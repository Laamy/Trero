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

            Actor plr = Game.getClosestPlayer();
            if (Game.position.Distance(plr.position) < 6f)
                Game.SexActor(plr);
        }
    }
}
