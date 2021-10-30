#region

using System;
using System.Windows.Forms;
using Trero.ClientBase;
using Trero.ClientBase.KeyBase;
using Trero.Modules.vModuleExtra;

#endregion

namespace Trero.Modules
{
    internal class ElytraFlight : Module
    {
        Vector3 savedVel = Base.Vec3();
        public ElytraFlight() : base("ElytraFlight", (char)0x07, "Flies")
        {
            addBypass(new BypassBox(new string[] { "Default", "Fast", "Super fast", "Hyper speed" }));
            addBypass(new BypassBox(new string[] { "RestoreVel: False", "RestoreVel: True", }));
        } // Not defined

        public override void OnEnable()
        {
            base.OnEnable();

            if (bypasses[1].curIndex == 1)
                savedVel = Game.velocity;
        }

        public override void OnDisable()
        {
            base.OnDisable();

            if (bypasses[1].curIndex == 1)
                Game.velocity = savedVel;
        }

        public override void OnTick()
        {
            if (Game.isNull) return;

            Game.velocity = Base.Vec3(0, 0.05f);

            if (Keymap.GetAsyncKeyState(Keys.W))
                moveTick(0);
            if (Keymap.GetAsyncKeyState(Keys.A))
                moveTick(55);
            if (Keymap.GetAsyncKeyState(Keys.S))
                moveTick(110);
            if (Keymap.GetAsyncKeyState(Keys.D))
                moveTick(-55);
            if (Keymap.GetAsyncKeyState(Keys.LShiftKey) || Keymap.GetAsyncKeyState(Keys.RShiftKey))
                Game.velocity = Base.Vec3(0, 1);
        }

        private void moveTick(int offset)
        {
            var newVel = Game.velocity;

            var cy = (Game.bodyRots.y + 89.9f) * ((float)Math.PI / 180F);

            cy += offset;

            int speed = 4;

            switch (bypasses[0].curIndex)
            {
                case 0:
                    speed = 4;
                    break;
                case 1:
                    speed = 6;
                    break;
                case 2:
                    speed = 10;
                    break;
                case 3:
                    speed = 18;
                    break;
            }

            newVel.z = (float)Math.Sin(cy) * (speed / 9f);
            newVel.x = (float)Math.Cos(cy) * (speed / 9f);

            Game.velocity = newVel;
        }
    }
}