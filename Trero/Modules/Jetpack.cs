using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trero.ClientBase;
using Trero.ClientBase.EntityBase;
using Trero.ClientBase.KeyBase;

namespace Trero.Modules
{
    class Jetpack : Module
    {
        public Jetpack() : base("Jetpack", (char)0x07, "Flies") {
            Keymap.keyEvent += keyvE;
        } // Not defined

        private void keyvE(object sender, KeyEvent e)
        {
            if (e.vkey == vKeyCodes.KeyUp)
            {
                if ((char)(int)e.key == keybind)
                {
                    onDisable(); // Disable module when keybind has been let go
                }
            }
        }

        public override void onTick()
        {
            if (Game.isNull) return;

            var vel = Base.Vec3();
            var dirVec = Game.lVector;

            vel.x = 0.6f * dirVec.x;
            vel.y = 0.6f * -dirVec.y;
            vel.z = 0.6f * dirVec.z;

            Game.velocity = vel;
        }
    }
}
