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
    class Nofriends : Module
    {
        public Nofriends() : base("Nofriends", (char)0x07, "Other") { } 

        public override void onEnable()
        {
            Game.CustomDefines.nofriends = true;
            base.onEnable();
        }

        public override void onDisable()
        {
            Game.CustomDefines.nofriends = false;
            base.onDisable();
        }
    }
}
