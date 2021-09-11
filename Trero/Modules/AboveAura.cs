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
    class AboveAura : Module
    {
        int flicker = 0;
        public AboveAura() : base("AboveAura", (char)0x07, "Exploits") { } // Not defined


        public override void onTick()
        {
            if (Game.isNull) return;


            flicker++;

            if (flicker == 4)
            {
                flicker = 0;
                var ent = Game.getClosestPlayer();
                if (ent == null) return;

                if (ent == null) return; // Returns if entity doesnt exist

                Vector3 pos = ent.position;

                if (Game.position.Distance(pos) < 6)
                {
                    pos.y += 2;

                    Game.position = pos;
                    Game.velocity = Base.Vec3(0, -0.01f, 0);
                }
            }
        }
    }
}
