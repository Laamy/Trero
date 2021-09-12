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
    class AntiImmoblie : Module
    {
        public AntiImmoblie() : base("AntiImmoblie", (char)(int)Keys.Insert, "Exploits", true) { } // Not defined
        public override void onEnable()
        {
            MCM.writeBaseBytes(0x1CAEB90, MCM.ceByte2Bytes("90 90")); // nop anti immobile address
            base.onEnable();
        }
        public override void onDisable()
        {
            MCM.writeBaseBytes(0x1CAEB90, MCM.ceByte2Bytes("75 16")); // restore anti immobile address
            base.onDisable();
        }
    }
}
