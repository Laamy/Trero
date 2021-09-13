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
    class Unlimiter : Module
    {
        public Unlimiter() : base("Unlimiter", (char)0x07, "Other") { } 

        public override void onEnable()
        {
            Program.Unlimiter = true;
            base.onEnable();
        }

        public override void onDisable()
        {
            Program.Unlimiter = false;
            base.onDisable();
        }
    }
}
