using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trero.ClientBase;

namespace Trero.Modules
{
    class Debug : Module
    {
        [DllImport("kernel32.dll")] static extern IntPtr GetConsoleWindow();
        [DllImport("user32.dll")] static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        public Debug() : base("Debug", (char)0x07, "Other", true) { } // 0x07 = no keybind

        public override void onEnable()
        {
            ShowWindow(GetConsoleWindow(), 5);
            base.onEnable();
        }

        public override void onDisable()
        {
            ShowWindow(GetConsoleWindow(), 0);
            base.onDisable();
        }
    }
}
