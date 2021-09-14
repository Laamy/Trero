#region

using System;
using System.Runtime.InteropServices;

#endregion

namespace Trero.Modules
{
    internal class Debug : Module
    {
        public Debug() : base("Debug", (char)0x07, "Other", true)
        {
        } // 0x07 = no keybind

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public override void OnEnable()
        {
            ShowWindow(GetConsoleWindow(), 5);
            base.OnEnable();
        }

        public override void OnDisable()
        {
            ShowWindow(GetConsoleWindow(), 0);
            base.OnDisable();
        }
    }
}