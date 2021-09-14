#region

using System;
using System.Runtime.InteropServices;

#endregion

namespace Trero.ClientBase.KeyBase
{
    public class Mouse
    {
        [Flags]
        public enum MouseEventFlags
        {
            MOUSEEVENTF_LEFTDOWN = 0x02,
            MOUSEEVENTF_RIGHTDOWN = 0x08,
            MOUSEEVENTF_MIDDLEDOWN = 0x20
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(long dwFlags, long dx, long dy, long cButtons, long dwExtraInfo);

        public static void MouseEvent(MouseEventFlags keyFlag)
        {
            if (keyFlag == MouseEventFlags.MOUSEEVENTF_LEFTDOWN)
            {
                mouse_event(0x02, 0, 0, 0, 0);
                mouse_event(0x04, 0, 0, 0, 0);
            }
            else if (keyFlag == MouseEventFlags.MOUSEEVENTF_RIGHTDOWN)
            {
                mouse_event(0x08, 0, 0, 0, 0);
                mouse_event(0x10, 0, 0, 0, 0);
            }
            else if (keyFlag == MouseEventFlags.MOUSEEVENTF_MIDDLEDOWN)
            {
                mouse_event(0x20, 0, 0, 0, 0);
                mouse_event(0x40, 0, 0, 0, 0);
            }
        }
    }
}