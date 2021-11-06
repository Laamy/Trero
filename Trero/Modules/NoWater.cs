using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trero.ClientBase;

namespace Trero.Modules
{
    internal class NoWater : Module
    {
        public NoWater() : base("NoWater",(char) 0x07, "Exploits", "This Module will make it so you don't slow down in water - Gamerclient28921!")
        {
        }

        public override void OnEnable()
        {
            base.OnEnable();

            MCM.writeBaseBytes(0x1DF9850, MCM.ceByte2Bytes("90 90 90 90 90 90"));//nop the water velocity modifier code
        }

        public override void OnDisable()
        {
            base.OnDisable();
            MCM.writeBaseBytes(0x1DF9850, MCM.ceByte2Bytes("88 86 5D 02 00 00")); // restore original bytes
        }
    }
}
