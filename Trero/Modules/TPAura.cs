﻿using System;
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
        public TPAura() : base("TPAura", (char)0x07, "Exploits") { } // Not defined
        Random ran = new Random();

        public override void onTick()
        {
            if (Game.isNull) return;

            var ent = Game.getClosestPlayer();
            if (ent == null) return; // Returns if entity doesnt exist

            Vector3 pos = ent.position;

            if (Game.position.Distance(pos) < 6)
            {
                pos.x += (float)(ran.NextDouble() * (0.6 - -0.6) + -0.6);
                pos.z += (float)(ran.NextDouble() * (0.6 - -0.6) + -0.6);

                Game.position = pos;
                Game.velocity = Base.Vec3(0, -0.01f, 0);
            }
        }
    }
}
