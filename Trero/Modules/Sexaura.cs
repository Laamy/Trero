using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trero.ClientBase;
using Trero.ClientBase.EntityBase;

namespace Trero.Modules
{
    class Sexaura : Module
    {
        public Sexaura() : base("Sexaura", (char)0x07, "World") { } // Not defined

        public override void onTick()
        {
            if (Game.isNull) return;

            var ent = Game.getClosestPlayer();
            if (ent == null) return; // Returns if entity doesnt exist

            Vector3 pos = ent.position;

            if (Game.position.Distance(pos) < 6)
            {
                pos.x += 0.5f;

                Game.position = pos;
                Game.velocity = Base.Vec3();
            }
        }
    }
}
