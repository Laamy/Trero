using System.Diagnostics;
using System.Windows.Forms;
using Trero.ClientBase.UIBase;

namespace Trero.Modules
{
    class KillGame : Module
    {
        public KillGame() : base("KillGame", (char)0x07, "Other") { } // Not defined
        public override void onEnable()
        {
            Process.GetProcessesByName("ApplicationFrameHost")[0].Kill();
            Process.GetProcessesByName("Minecraft.Windows")[0].Kill();
        }
    }
}
