using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trero.ClientBase;
using Trero.ClientBase.EntityBase;

namespace Trero.Modules
{
    class TPAura : Module
    {
        int flicker = 0;
        public TPAura() : base("TPAura", (char)0x07, "Exploits") { } // Not defined
        Random ran = new Random();

        public override void onTick()
        {
            if (Game.isNull) return;
            flicker++;

            if (flicker == 4)
            {
                flicker = 0;
                if (Game.isNull) return;
                flicker++;

                if (flicker == 300)
                {
                    flicker = 0;
                    Game.onGround = false;
                }
            }
        }
    }
}
