using System.Diagnostics;
using System.Windows.Forms;
using Trero.ClientBase.UIBase;

namespace Trero.Modules
{
    class Eject : Module
    {
        public Eject() : base("Eject", (char)0x07, "Other") { } // Not defined
        public override void onEnable()
        {
            Program.quit = true;
        }
    }
}
