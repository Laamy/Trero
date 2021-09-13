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
    class Limiter : Module
    {
        public Limiter() : base("Limiter", (char)0x07, "Other") { } 

        public override void onEnable()
        {
            Program.Limiter = true;
            base.onEnable();
        }

        public override void onDisable()
        {
            Program.Limiter = false;
            base.onDisable();
        }
    }
}
