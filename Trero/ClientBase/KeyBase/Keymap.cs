using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trero.ClientBase.KeyBase
{
    class Keymap
    {
        [DllImport("user32.dll")] public static extern bool GetAsyncKeyState(char v);
        [DllImport("user32.dll")] public static extern bool GetAsyncKeyState(Keys v);
        [DllImport("user32.dll")] public static extern bool GetAsyncKeyState(int v);
        [DllImport("user32.dll")] static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);
        [DllImport("user32.dll")] static extern IntPtr GetForegroundWindow();

        public static int e = 0;
        public static bool isMinecraftFocused()
        {
            StringBuilder sb = new StringBuilder("Minecraft".Length + 1);
            GetWindowText(GetForegroundWindow(), sb, "Minecraft".Length + 1);
            return sb.ToString().CompareTo("Minecraft") == 0;
        }

        public static Keymap handle;
        public static EventHandler<KeyEvent> keyEvent;

        Dictionary<char, uint> dBuff = new Dictionary<char, uint>();
        Dictionary<char, bool> noKey = new Dictionary<char, bool>();

        Dictionary<char, uint> rBuff = new Dictionary<char, uint>();
        Dictionary<char, bool> yesKey = new Dictionary<char, bool>();

        public Keymap()
        {
            // ++e;
            handle = this;
            // ++e;
            for (char c = (char)0; c < 0xFF; c++)
            {
                rBuff.Add(c, 0);
                noKey.Add(c, true);
            }
            // ++e;
            for (char c = (char)0; c < 0xFF; c++)
            {
                dBuff.Add(c, 0);
                yesKey.Add(c, true);
            }
            // ++e;

            new Thread(() => {
                while (!Program.quit)
                {
                    // ++e;
                    try
                    {
                        if (isMinecraftFocused())
                        {
                            // ++e;
                            for (char c = (char)0; c < 0xFF; c++)
                            {
                                noKey[c] = true;
                                yesKey[c] = false;
                                if (GetAsyncKeyState(c))
                                {
                                    if (keyEvent != null)
                                        keyEvent.Invoke(this, new KeyEvent(c, vKeyCodes.KeyHeld));
                                    // ++e;
                                    noKey[c] = false;
                                    if (dBuff[c] > 0)
                                        continue;
                                    dBuff[c]++;
                                    try
                                    {
                                        if (keyEvent != null)
                                            keyEvent.Invoke(this, new KeyEvent(c, vKeyCodes.KeyDown));
                                        // ++e;
                                    }
                                    catch { }
                                }
                                else
                                {
                                    yesKey[c] = true;
                                    if (rBuff[c] > 0)
                                        continue;
                                    rBuff[c]++;
                                    try
                                    {
                                        if (keyEvent != null)
                                            keyEvent.Invoke(this, new KeyEvent(c, vKeyCodes.KeyUp));
                                        // ++e;
                                    }
                                    catch { }
                                }
                                if (noKey[c])
                                    dBuff[c] = 0;
                                if (!yesKey[c])
                                    rBuff[c] = 0;
                            }
                        }
                    }
                    catch { }
                }
            }).Start();
        }
    }
    public class KeyEvent : EventArgs // flare's key events
    {
        public Keys key;
        public vKeyCodes vkey;
        public KeyEvent(char v, vKeyCodes c)
        {
            key = (Keys)v;
            vkey = c;
        }
        public KeyEvent(Keys v, vKeyCodes c)
        {
            key = v;
            vkey = c;
        }
    }
    public enum vKeyCodes : int
    {
        KeyDown = 0,
        KeyHeld = 1,
        KeyUp = 2
    }
}
