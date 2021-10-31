using System;
using Trero.ClientBase;
using Trero.ClientBase.KeyBase;

namespace Trero.Modules
{
    class Tower : Module
    {
        public Tower() : base("Tower", (char)0x07, "World", "Hold right click to automatically build upwards") {
            Keymap.keyEvent += keyPress;
        }

        private void keyPress(object sender, KeyEvent e)
        {
            if (e.vkey == VKeyCodes.KeyHeld && enabled)
            {
                if ((char)e.key == (char)0x02)
                {
                    Game.Placing = 0;

                    OverrideBase.Pitch = false;

                    Vector2 rots = Game.bodyRots;
                    rots.x = 90f;
                    Game.bodyRots = rots;

                    Game.velocity = Base.Vec3(0, 0.25f);
                }
            }
            if (e.vkey == VKeyCodes.KeyUp)
            {
                OverrideBase.Pitch = true;
            }
        }
    }
}
