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
        public NoWater() : base("NoWater",(char) 0x07, "Exploits", "This Module will make it so you don't slow down in water")
        {
        }
        public override void OnEnable()
        {
            base.OnEnable();

            MCM.writeBaseBytes(0x1DF9850, new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 });//nop the water velocity modifier code
        }

        public override void OnDisable()
        {
            base.OnDisable();

            MCM.writeBaseBytes(0x1DF9850, new byte[] { 0x88, 0x86, 0x5D, 0x02, 0x00, 0x00 }); // restore original bytes
        }
    }
}
