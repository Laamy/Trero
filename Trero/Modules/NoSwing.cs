using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trero.ClientBase;
using Trero.ClientBase.EntityBase;
using Trero.ClientBase.KeyBase;
using Trero.ClientBase.VersionBase;

namespace Trero.Modules
{
    class NoSwing : Module
    {
        public NoSwing() : base("NoSwing", (char)0x07, "Visual") { } // Not defined

        public override void onTick()
        {
            if (Game.isNull) return;

            Game.swingAn = 0;
        }
    }
}
