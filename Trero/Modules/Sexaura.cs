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
    class Sexaura : Module
    {
        public Sexaura() : base("Sexaura", 0x07) { } // Not defined

        public override void onTick()
        {
            Actor ent = Game.getClosestPlayer();

            Vector2 bodyRots = ent.rotation;
            Vector2 cumRots = ent.compassRotations;
            Vector3 dirv = Game.dirVect(bodyRots.x, bodyRots.y);
        }
    }
}
