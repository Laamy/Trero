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
    class CreativeFly : Module
    {
        public CreativeFly() : base("CreativeFly", (char)0x07, "Player") { } // Not defined

        public override void onTick()
        {
            // if (Game.isNull) return;

            Game.isFlying = true; // need to define 
        }
    }
}
