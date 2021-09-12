using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trero.ClientBase;
using Trero.ClientBase.EntityBase;
using Trero.ClientBase.UIBase;

namespace Trero.Modules
{
    class Reach : Module
    {
        public Reach() : base("Reach", (char)0x07, "Combat") { } // Not defined
        public override void onEnable()
        {
            MCM.writeBaseFloat(0x1CAEB90, 7);
            base.onEnable();
        }
        public override void onDisable()
        {
            MCM.writeBaseFloat(0x1CAEB90, 3);
            base.onDisable();
        }
    }
}
