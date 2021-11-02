#region

using System;
using System.Windows.Forms;
using Trero.ClientBase;
using Trero.ClientBase.KeyBase;
using Trero.Modules.vModuleExtra;

#endregion

namespace Trero.Modules
{
    internal class Fly : Module
    {
        private int count = 0;//lifeboatfly blink counter

        public Fly() : base("Fly", (char)0x07, "Flies", "Basic fly that supports minevile's disabler when on vannila")
        {
            addBypass(new BypassBox(new string[] { "Mode: Vannila", "Mode: LifeBoat"}));
        } // 0x07 = no keybind

        public override void OnEnable()
        {
            base.OnEnable();
            if(bypasses[0].curIndex == 1)
            {
                //tp up
                Game.position = new Vector3(Game.position.x, Game.position.y + 1f, Game.position.z);
            }
            foreach (var mod in Program.Modules)
                if (mod.name == "Disabler" && mod.enabled && bypasses[0].curIndex == 0)
                    Game.vclip(0.5f);
        }

        public override void OnTick()
        {
            if (Game.isNull) return;

            if (bypasses[0].curIndex == 0)
            {
                Game.onGround = true;

                var newVel = Base.Vec3();

                var cy = (Game.bodyRots.y + 89.9f) * ((float)Math.PI / 180F);

                if (Keymap.GetAsyncKeyState(Keys.W))
                {
                    newVel.z = (float)Math.Sin(cy) * (12 / 16f);
                    newVel.x = (float)Math.Cos(cy) * (12 / 16f);
                }

                if (Keymap.GetAsyncKeyState((char)Keys.Space))
                {
                    foreach (var mod in Program.Modules)
                        if (mod.name == "Disabler" && !mod.enabled)
                            newVel.y += 0.6f;
                }
                if (Keymap.GetAsyncKeyState((char)Keys.LShiftKey))
                {
                    newVel.y -= 0.6f;
                }

                Game.velocity = newVel;
            }else if (bypasses[0].curIndex == 1)
            {
                Game.onGround = true;

                var newVel = Base.Vec3();

                var cy = (Game.bodyRots.y + 89.9f) * ((float)Math.PI / 180F);
                float speed = 1.0f;

                if (Keymap.GetAsyncKeyState(Keys.W))
                {
                    newVel.z = (float)Math.Sin(cy) * speed;
                    newVel.x = (float)Math.Cos(cy) * speed;
                }
                newVel.y = -0.001f;

                if(count > 7)
                {
                    OverrideBase.CanSendPackets = false;
                    count = 0;
                }
                else
                {
                    count++;
                    OverrideBase.CanSendPackets = true;
                }
                Game.velocity = newVel;
            }
        }

        public override void OnDisable()
        {
            base.OnDisable();
            count = 0;
            OverrideBase.CanSendPackets = true;
        }
    }
}