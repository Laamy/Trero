using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trero.ClientBase;

namespace Trero.Modules
{
    class Hitbox : Module
    {
        public Hitbox() : base("Hitbox", (char)0x07, "Combat") { } // 0x07 = no keybind

        public override void onTick()
        {
            if (Game.isNull) return;
            foreach (var entity in Game.getPlayers())
            {
                entity.hitbox = Base.Vec2(7, 7);
            }
        }
        public override void onDisable()
        {
            base.onDisable();

            foreach (var entity in Game.getPlayers())
            {
                entity.hitbox = Base.Vec2(0.6f, 1.8f);
            }
        }
    }
}
