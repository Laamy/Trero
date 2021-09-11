using System;
using System.Windows.Forms;
using Trero.ClientBase;
using Trero.ClientBase.KeyBase;
using Trero.ClientBase.VersionBase;

namespace Trero.Modules
{
    class LBSH : Module
    {
        public LBSH() : base("LBDF", (char)0x07, "Flies") { } // Not defined
        public override void onTick() { }
    }
}
