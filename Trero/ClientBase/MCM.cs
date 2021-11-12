#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace Trero.ClientBase
{
    public class MCM
    {
        private static string mainProcName = "";

        public static uint gameProcId;
        public static Process gameProc;
        public static IntPtr gameHandle;
        public static IntPtr gameBaseAddr;
        public static ProcessModule gameMainModule;

        public static ProcessModule selectedModule;
        public static IntPtr selectedHandle;
        public static IntPtr selectedModuleAddr;

        public static uint procHandleId;
        public static IntPtr procHandle;

        public static List<AddressBox> frozenBytes = new List<AddressBox>();

        #region Flags
        [Flags]
        public enum AllocationType
        {
            Commit = 0x1000,
            Reserve = 0x2000,
            Decommit = 0x4000,
            Release = 0x8000,
            Reset = 0x80000,
            Physical = 0x400000,
            TopDown = 0x100000,
            WriteWatch = 0x200000,
            LargePages = 0x20000000
        }

        [Flags]
        public enum MemoryProtection
        {
            Execute = 0x10,
            ExecuteRead = 0x20,
            ExecuteReadWrite = 0x40,
            ExecuteWriteCopy = 0x80,
            NoAccess = 0x01,
            ReadOnly = 0x02,
            ReadWrite = 0x04,
            WriteCopy = 0x08,
            GuardModifierflag = 0x100,
            NoCacheModifierflag = 0x200,
            WriteCombineModifierflag = 0x400
        }
        #endregion

        #region Imports
        [DllImport("user32.dll")]
        public static extern bool GetAsyncKeyState(char vKey);

        [DllImport("kernel32", SetLastError = true)]
        public static extern int ReadProcessMemory(IntPtr hProcess, ulong lpBase, ref ulong lpBuffer, int nSize,
            int lpNumberOfBytesRead);

        [DllImport("kernel32", SetLastError = true)]
        public static extern int WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, ref IntPtr lpBuffer,
            int nSize, int lpNumberOfBytesWritten);

        [DllImport("kernel32", SetLastError = true)]
        public static extern int WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, ref byte lpBuffer, int nSize,
            int lpNumberOfBytesWritten);

        [DllImport("kernel32", SetLastError = true)]
        public static extern int VirtualProtectEx(IntPtr hProcess, IntPtr lpAddress, int dwSize, long flNewProtect,
            ref long lpflOldProtect);

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool GetWindowRect(IntPtr hWnd, out ProcessRectangle lpRect);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool GetWindowRect(IntPtr hWnd, out Rectangle lpRect);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool GetWindow(IntPtr hWnd, uint uCmd);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize,
            AllocationType flAllocationType, MemoryProtection flProtect);

        [DllImport("kernel32")]
        public static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize,
            IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, out uint lpThreadId);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        public static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, UIntPtr dwSize, uint dwFreeType);
        #endregion

        #region InitUtils
        public static void openGame(string procByName)
        {
            var procs = Process.GetProcessesByName(procByName); // Minecraft.Windows
            var w10 = procs[0];
            var proc = OpenProcess(0x1F0FFF, false, w10.Id);
            gameProcId = (uint)w10.Id;
            gameHandle = proc;
            gameMainModule = w10.MainModule;
            gameBaseAddr = gameMainModule.BaseAddress;
            gameProc = w10;

            selectModule(gameMainModule);
        }

        public static void selectModule(ProcessModule module)
        {
            selectedModule = module;
            selectedHandle = gameHandle;
            selectedModuleAddr = module.BaseAddress;
        }

        public static void selectModule(string moduleName, bool noLower = false)
        {
            foreach (ProcessModule module in gameProc.Modules)
            {
                switch (noLower)
                {
                    case true:
                        if (module.ModuleName == moduleName)
                            selectModule(module);
                        break;
                    case false:
                        if (module.ModuleName.ToLower() == moduleName.ToLower())
                            selectModule(module);
                        break;
                }
            }
        }

        public static void selectBaseModule() => selectModule(gameMainModule);

        public static void openWindowHost(string processWindowHost, string processWindowHostName)
        {
            var procs = Process.GetProcessesByName(processWindowHost); // ApplicationFrameHost
            procHandle = procs[0].MainWindowHandle;
            procHandleId = (uint)procs[0].Id;
            mainProcName = processWindowHostName;
        }
        #endregion

        #region WindowInformation
        public static ProcessRectangle getGameRect()
        {
            var winRect = new ProcessRectangle();
            GetWindowRect(procHandle, out winRect);
            return winRect;
        }

        public static Rectangle getGameDrawingRect()
        {
            var winRect = new Rectangle();
            GetWindowRect(procHandle, out winRect);
            return winRect;
        }

        public static void focusedStr(string winName) => mainProcName = winName;
        public static string getFocusedStr() => mainProcName;

        public static bool isGameFocused()
        {
            var sb = new StringBuilder(mainProcName.Length + 1);
            GetWindowText(GetForegroundWindow(), sb, mainProcName.Length + 1);
            return sb.ToString().CompareTo(mainProcName) == 0;
        }

        public static IntPtr isGameFocusedInsert()
        {
            var sb = new StringBuilder(mainProcName.Length + 1);
            GetWindowText(GetForegroundWindow(), sb, mainProcName.Length + 1);
            if (sb.ToString() == mainProcName)
                return (IntPtr)(-1);
            return (IntPtr)(-2);
        }
        #endregion

        #region Memory Protection
        public static void unprotectMemory(IntPtr address, int bytesToUnprotect)
        {
            long receiver = 0;
            VirtualProtectEx(selectedHandle, address, bytesToUnprotect, 0x40, ref receiver);
        }

        public static void protectMemory(IntPtr address, int bytesToUnprotect)
        {
            long receiver = 0;
            VirtualProtectEx(selectedHandle, address, bytesToUnprotect, 0x2, ref receiver);
        }
        #endregion

        #region CEObj 2 Obj
        public static byte[] ceByte2Bytes(string byteString)
        {
            var byteStr = byteString.Split(' ');
            var bytes = new byte[byteStr.Length];
            var c = 0;
            foreach (var b in byteStr)
            {
                bytes[c] = Convert.ToByte(b, 16);
                c++;
            }

            return bytes;
        }

        public static int[] ceByte2Ints(string byteString)
        {
            var intStr = byteString.Split(' ');
            var ints = new int[intStr.Length];
            var c = 0;
            foreach (var b in intStr)
            {
                ints[c] = int.Parse(b, NumberStyles.HexNumber);
                c++;
            }

            return ints;
        }

        public static ulong[] ceByte2uLong(string byteString)
        {
            var intStr = byteString.Split(' ');
            var longs = new ulong[intStr.Length];
            var c = 0;
            foreach (var b in intStr)
            {
                longs[c] = ulong.Parse(b, NumberStyles.HexNumber);
                c++;
            }

            return longs;
        }
        #endregion

        #region MultiLevelPointers
        public static ulong baseEvaluatePointer(ulong offset, ulong[] offsets)
        {
            ulong buffer = 0;
            ReadProcessMemory(selectedHandle, (ulong)selectedModuleAddr + offset, ref buffer, sizeof(ulong), 0);
            for (var i = 0; i < offsets.Length - 1; i++)
                ReadProcessMemory(selectedHandle, buffer + offsets[i], ref buffer, sizeof(ulong), 0);
            return buffer + offsets[offsets.Length - 1];
        }

        public static ulong evaluatePointer(ulong addr, ulong[] offsets)
        {
            ulong buffer = 0;
            ReadProcessMemory(selectedHandle, addr, ref buffer, sizeof(ulong), 0);
            for (var i = 0; i < offsets.Length - 1; i++)
                ReadProcessMemory(selectedHandle, buffer + offsets[i], ref buffer, sizeof(ulong), 0);
            return buffer + offsets[offsets.Length - 1];
        }
        #endregion

        #region BaseRead
        public static int readBaseByte(int offset)
        {
            ulong buffer = 0;
            ReadProcessMemory(selectedHandle, (ulong)(selectedModuleAddr + offset), ref buffer, sizeof(byte), 0);
            return (byte)buffer;
        }

        public static int readBaseInt(int offset)
        {
            ulong buffer = 0;
            ReadProcessMemory(selectedHandle, (ulong)(selectedModuleAddr + offset), ref buffer, sizeof(int), 0);
            return (int)buffer;
        }

        public static float readBaseFloat(int offset)
        {
            ulong buffer = 0;
            ReadProcessMemory(selectedHandle, (ulong)(selectedModuleAddr + offset), ref buffer, sizeof(float), 0);
            return (float)buffer;
        }

        public static ulong readBaseInt64(int offset)
        {
            ulong buffer = 0;
            ReadProcessMemory(selectedHandle, (ulong)(selectedModuleAddr + offset), ref buffer, sizeof(long), 0);
            return buffer;
        }
        #endregion

        #region BaseWrite
        public static void writeBaseByte(int offset, byte value)
        {
            unprotectMemory(selectedModuleAddr + offset, 1);
            WriteProcessMemory(selectedHandle, selectedModuleAddr + offset, ref value, sizeof(byte), 0);
        }

        public static void writeBaseInt(int offset, int value)
        {
            var intByte = BitConverter.GetBytes(value);
            var inc = 0;
            unprotectMemory(selectedModuleAddr + offset, intByte.Length);
            foreach (var b in intByte)
            {
                writeBaseByte(offset + inc, b);
                inc++;
            }
        }

        public static void writeBaseBytes(int offset, byte[] value)
        {
            var inc = 0;
            unprotectMemory(selectedModuleAddr + offset, value.Length);
            foreach (var b in value)
            {
                writeBaseByte(offset + inc, b);
                inc++;
            }
        }

        public static void writeBaseFloat(int offset, float value)
        {
            var intByte = BitConverter.GetBytes(value);
            var inc = 0;
            unprotectMemory(selectedModuleAddr + offset, intByte.Length);
            foreach (var b in intByte)
            {
                writeBaseByte(offset + inc, b);
                inc++;
            }
        }

        public static void writeBaseInt64(int offset, ulong value)
        {
            var intByte = BitConverter.GetBytes(value);
            var inc = 0;
            unprotectMemory(selectedModuleAddr + offset, sizeof(long));
            foreach (var b in intByte)
            {
                writeBaseByte(offset + inc, b);
                inc++;
            }
        }
        #endregion

        #region DirectRead
        public static byte readByte(ulong address)
        {
            ulong buffer = 0;
            ReadProcessMemory(selectedHandle, address, ref buffer, sizeof(byte), 0);
            return (byte)buffer;
        }

        public static int readInt(ulong address)
        {
            ulong buffer = 0;
            ReadProcessMemory(selectedHandle, address, ref buffer, sizeof(int), 0);
            return (int)buffer;
        }

        public static short readInt16(ulong address)
        {
            ulong buffer = 0;
            ReadProcessMemory(selectedHandle, address, ref buffer, sizeof(short), 0);
            return (short)buffer;
        }

        public static float readFloat(ulong address)
        {
            ulong buffer = 0;
            ReadProcessMemory(selectedHandle, address, ref buffer, sizeof(float), 0);
            var raw = BitConverter.GetBytes(buffer);
            return BitConverter.ToSingle(raw, 0);
        }

        public static ulong readInt64(ulong address)
        {
            ulong buffer = 0;
            ReadProcessMemory(selectedHandle, address, ref buffer, sizeof(ulong), 0);
            return buffer;
        }

        public static string readString(ulong address, ulong length)
        {
            var strByte = new byte[length];
            var inc = 0;
            foreach (var b in strByte)
            {
                var next = readByte(address + (ulong)inc);
                if (next == 0)
                    break;
                strByte[inc] = next;
                inc++;
            }

            return new string(Encoding.Default.GetString(strByte).Take(inc).ToArray());
        }
        #endregion

        #region DirectWrite
        public static void writeByte(ulong address, byte value)
        {
            WriteProcessMemory(selectedHandle, (IntPtr)address, ref value, sizeof(byte), 0);
        }

        public static void writeBytes(ulong address, byte[] value)
        {
            var inc = 0;
            foreach (var b in value)
            {
                writeByte(address + (ulong)inc, b);
                inc++;
            }
        }

        public static void writeInt(ulong address, int value)
        {
            var intByte = BitConverter.GetBytes(value);
            var inc = 0;
            foreach (var b in intByte)
            {
                writeByte(address + (ulong)inc, b);
                inc++;
            }
        }

        public static void writeInt16(ulong address, short value)
        {
            var intByte = BitConverter.GetBytes(value);
            var inc = 0;
            foreach (var b in intByte)
            {
                writeByte(address + (ulong)inc, b);
                inc++;
            }
        }

        public static void writeFloat(ulong address, float value)
        {
            var intByte = BitConverter.GetBytes(value);
            var inc = 0;
            foreach (var b in intByte)
            {
                writeByte(address + (ulong)inc, b);
                inc++;
            }
        }

        public static void writeInt64(ulong address, ulong value)
        {
            var intByte = BitConverter.GetBytes(value);
            var inc = 0;
            foreach (var b in intByte)
            {
                writeByte(address + (ulong)inc, b);
                inc++;
            }
        }

        public static void writeString(ulong address, string str)
        {
            var intByte = Encoding.ASCII.GetBytes(str);
            var inc = 0;
            foreach (var b in intByte)
            {
                writeByte(address + (ulong)inc, b);
                inc++;
            }
        }
        #endregion

        #region Freeze
        /// <summary>
        /// Constantly overwrite an address
        /// </summary>
        /// <param name="addr">Address</param>
        /// <param name="value">Bytes to write</param>
        public static void freezeBytes(ulong addr, byte[] value)
        {
            unfreezeBytes(addr);

            var drci = new AddressBox(addr, value);
            frozenBytes.Add(drci);

            Task.Factory.StartNew((() =>
            {
                while (frozenBytes.Contains(drci))
                {
                    if (readByte(addr) != value[0])
                        writeBytes(addr, value);
                    Thread.Sleep(1);
                    //protectMemory((IntPtr)addr, value.Length); // crashes
                }
            }));
        }

        /// <summary>
        /// Like MCM.freezeBytes(...) but no speed limitations
        /// </summary>
        /// <param name="addr">Address</param>
        /// <param name="value">Bytes to write</param>
        public static void factoryFreezeBytes(ulong addr, byte[] value)
        {
            unfreezeBytes(addr);

            var drci = new AddressBox(addr, value);
            frozenBytes.Add(drci);

            Task.Factory.StartNew((() =>
            {
                while (frozenBytes.Contains(drci))
                {
                    if (readByte(addr) != value[0])
                        writeBytes(addr, value);
                }
            }));
        }

        /// <summary>
        /// Unfreeze area of memory
        /// </summary>
        /// <param name="addr">Address</param>
        public static void unfreezeBytes(ulong addr)
        {
            foreach (AddressBox addrBox in frozenBytes)
            {
                if (addrBox.addr == addr)
                {
                    frozenBytes.Remove(addrBox);
                    //unprotectMemory((IntPtr)addrBox.addr, addrBox.newBytes.Length);
                }
            }
        }
        #endregion

        #region Covert
        /// <summary>
        /// Covert float to bytes array
        /// </summary>
        /// <param name="value">float to bytes input</param>
        /// <returns>Bytes array depending on {value}</returns>
        public static byte[] float2Bytes(float value)
        {
            var intByte = BitConverter.GetBytes(value);
            return intByte;
        }

        /// <summary>
        /// Covert int to bytes array
        /// </summary>
        /// <param name="value">int to bytes input</param>
        /// <returns>Bytes array depending on {value}</returns>
        public static byte[] int2Bytes(int value)
        {
            var intByte = BitConverter.GetBytes(value);
            return intByte;
        }

        /// <summary>
        /// Covert bool to bytes array
        /// </summary>
        /// <param name="value">bool to bytes input</param>
        /// <returns>Bytes array depending on {value}</returns>
        public static byte[] bool2Bytes(bool value)
        {
            var intByte = BitConverter.GetBytes(value);
            return intByte;
        }

        /// <summary>
        /// Covert short to bytes array
        /// </summary>
        /// <param name="value">short to bytes input</param>
        /// <returns>Bytes array depending on {value}</returns>
        public static byte[] short2Bytes(short value)
        {
            var intByte = BitConverter.GetBytes(value);
            return intByte;
        }
        #endregion

        #region Structs
        /// <summary>
        /// Window Rectangle
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct ProcessRectangle
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
            public ProcessRectangle(Point position, Point size) // this is most likely wrong
            {
                this.Left = position.X;
                this.Top = position.X + size.X;
                this.Right = position.Y;
                this.Bottom = position.Y + size.Y;

                // Left, Top, Right, Bottom
                // X, X - X, Y, Y - Y

                // Left, Top,
                // Right, Bottom
                // X, X - X,
                // Y, Y - Y
            }
        }

        /// <summary>
        /// Structor used for freezing and unfreezing addresses
        /// </summary>
        public struct AddressBox
        {
            public ulong addr;
            public byte[] newBytes;
            public AddressBox(ulong addr, byte[] newBytes)
            {
                this.addr = addr;
                this.newBytes = newBytes;
            }
        }
        #endregion
    }
}