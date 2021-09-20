#region

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

#endregion

namespace Trero.ClientBase.KeyBase
{
    internal class Keymap
    {
        public static int e = 0;

        public static Keymap handle;
        public static EventHandler<KeyEvent> keyEvent;
        public static EventHandler<KeyEvent> globalKeyEvent;

        private readonly Dictionary<char, uint> _dBuff = new Dictionary<char, uint>();
        private readonly Dictionary<char, bool> _noKey = new Dictionary<char, bool>();

        private readonly Dictionary<char, uint> _rBuff = new Dictionary<char, uint>();
        private readonly Dictionary<char, bool> _yesKey = new Dictionary<char, bool>();

        public Keymap()
        {
            handle = this;
            for (var c = (char)0; c < 0xFF; c++)
            {
                _rBuff.Add(c, 0);
                _dBuff.Add(c, 0);
                _noKey.Add(c, true);
                _yesKey.Add(c, true);
            }

            Program.mainThread += keyTick;
        }

        private void keyTick(object sender, EventArgs e)
        {
            try
            {
                // ++e;
                for (var c = (char)0; c < 0xFF; c++)
                {
                    _noKey[c] = true;
                    _yesKey[c] = false;
                    if (GetAsyncKeyState(c))
                    {
                        if (keyEvent != null)
                            if (MCM.isMinecraftFocused())
                                keyEvent.Invoke(this, new KeyEvent(c, VKeyCodes.KeyHeld));
                        //globalKeyEvent.Invoke(this, new KeyEvent(c, vKeyCodes.KeyHeld));
                        // ++e;
                        _noKey[c] = false;
                        if (_dBuff[c] > 0)
                            continue;
                        _dBuff[c]++;
                        try
                        {
                            if (keyEvent != null)
                                if (MCM.isMinecraftFocused())
                                    keyEvent.Invoke(this, new KeyEvent(c, VKeyCodes.KeyDown));
                            //globalKeyEvent.Invoke(this, new KeyEvent(c, vKeyCodes.KeyDown));
                            // ++e;
                        }
                        catch
                        {
                        }
                    }
                    else
                    {
                        _yesKey[c] = true;
                        if (_rBuff[c] > 0)
                            continue;
                        _rBuff[c]++;
                        try
                        {
                            if (keyEvent != null)
                                if (MCM.isMinecraftFocused())
                                    keyEvent.Invoke(this, new KeyEvent(c, VKeyCodes.KeyUp));
                            //globalKeyEvent.Invoke(this, new KeyEvent(c, vKeyCodes.KeyUp));
                            // ++e;
                        }
                        catch
                        {
                        }
                    }

                    if (_noKey[c])
                        _dBuff[c] = 0;
                    if (!_yesKey[c])
                        _rBuff[c] = 0;
                }
            }
            catch
            {
            }
        }

        [DllImport("user32.dll")]
        public static extern bool GetAsyncKeyState(char v);

        [DllImport("user32.dll")]
        public static extern bool GetAsyncKeyState(Keys v);

        [DllImport("user32.dll")]
        public static extern bool GetAsyncKeyState(int v);

        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        public static bool IsMinecraftFocused()
        {
            var sb = new StringBuilder("Minecraft".Length + 1);
            GetWindowText(GetForegroundWindow(), sb, "Minecraft".Length + 1);
            return string.Compare(sb.ToString(), "Minecraft", StringComparison.Ordinal) == 0;
        }
    }

    public class KeyEvent : EventArgs // flare's key events
    {
        public Keys key;
        public VKeyCodes vkey;

        public KeyEvent(char v, VKeyCodes c)
        {
            key = (Keys)v;
            vkey = c;
        }

        public KeyEvent(Keys v, VKeyCodes c)
        {
            key = v;
            vkey = c;
        }
    }

    public enum VKeyCodes
    {
        KeyDown = 0,
        KeyHeld = 1,
        KeyUp = 2
    }
}