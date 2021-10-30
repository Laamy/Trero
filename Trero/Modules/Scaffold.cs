using System;
using System.Linq;
using Trero.ClientBase;
using Trero.ClientBase.KeyBase;
using Trero.Modules.vModuleExtra;

namespace Trero.Modules
{
    class Scaffold : Module
    {

        private bool keyDown;
        private bool autoPlace;
        public Scaffold() : base("Scaffold", (char)0x07, "World") {
            Keymap.keyEvent += keyPress;
            addBypass(new BypassBox(new string[] { "Speed: Default", "Speed: 1", "Speed: 2" }));
            addBypass(new BypassBox(new string[] { "Place Mode: Auto ", "Place Mode: Manual" }));
            //addBypass(new BypassBox(new string[] { "No Blocks Stop: true", "No Blocks Stop: false" })); HeldItemCount is broken
           // addBypass(new BypassBox(new string[] { "Mode: JumpBridge", "Mode: Beneath", "Mode: StairCase" }));
        }

        private void keyPress(object sender, KeyEvent e)
        {
            if (e.vkey == VKeyCodes.KeyHeld && enabled)
            {
                switch (bypasses[1].curIndex)
                {
                    case 0:
                        OverrideBase.Pitch = false;
                        OverrideBase.Yaw = false;
                        keyDown = true;
                        autoPlace = true;

                        Vector2 JumpBridgeRots = Game.bodyRots;
                        JumpBridgeRots.x = 85f;
                        JumpBridgeRots.y = Game.bodyRots.y;
                        Game.bodyRots = JumpBridgeRots;
                        break;

                }

                float _speed = 0.1000000015f;

                switch (bypasses[0].curIndex)
                {
                    case 0:
                        _speed = 0.1000000015f;
                        break;
                    case 1:
                        _speed = 0.13f;
                        break;
                    case 2:
                        _speed = 0.16f;
                        break;
                }

                Game.speed = _speed;
            }

            if (e.vkey == VKeyCodes.KeyUp)
            {
                Game.speed = 0.1000000015f;
                keyDown = false;
                autoPlace = false;
                OverrideBase.Pitch = true;
                OverrideBase.Yaw = true;
            }
        }

        public override void OnTick()
        {
            if (keyDown == true)
            {
                Game.Placing = 0;
                switch (bypasses[1].curIndex)
                {
                    case 0:
                        if (MCM.isMinecraftFocused())
                        {
                            Mouse.MouseEvent(Mouse.MouseEventFlags.MOUSEEVENTF_RIGHTDOWN);
                        }
                        break;
                }

                switch (bypasses[2].curIndex)
                {
                    case 0: //helditemcount is broken
                        break;
                }
                /*switch (bypasses[0].curIndex)
                {
                    case 0:
                        if (Game.onGround)
                        {
                            /*Vector3 Velocity = Game.velocity;
                            Velocity.x = Game.lVector.x;
                            Velocity.y = 0.5f;
                            Velocity.z = Game.bodyRots.y;
                            Game.velocity = Velocity; //HMMMMMMMMMMMMMMMMMMMMMMMMMMM, jump bridge ???
                            
                        }
                        break;
                }*/
            }
        }
    }
}
