using System;
using System.Linq;
using Trero.ClientBase;
using Trero.ClientBase.KeyBase;
using Trero.Modules.vModuleExtra;

namespace Trero.Modules
{
    class Scaffold : Module
    {
        public Scaffold() : base("Scaffold", (char)0x07, "World") {
            addBypass(new BypassBox(new string[] { "Speed: Default", "Speed: 1", "Speed: 2" }));
            addBypass(new BypassBox(new string[] { "Place Mode: Auto ", "Place Mode: Manual" }));
            //addBypass(new BypassBox(new string[] { "No Blocks Stop: true", "No Blocks Stop: false" })); HeldItemCount is broken
            //addBypass(new BypassBox(new string[] { "Mode: JumpBridge", "Mode: Beneath", "Mode: StairCase" }));
        }

        public override void OnDisable()
        {
            base.OnDisable();

            OverrideBase.lookingAtBlock = true;
        }

        public override void OnTick()
        {
            base.OnTick();

            if (MCM.isMinecraftFocused())
            {
                OverrideBase.lookingAtBlock = false;
                Game.isLookingAtBlock = 0;

                var scaffoldPos = Game.exactPos;
                scaffoldPos.y -= 2;
                Game.SelectedBlock = scaffoldPos;

                Game.SideSelect = 1;

                float _speed = 0;

                switch (bypasses[0].curIndex)
                {
                    case 0:
                        _speed = 0;
                        break;
                    case 1:
                        _speed = 0.13f;
                        break;
                    case 2:
                        _speed = 0.16f;
                        break;
                }

                if (_speed != 0)
                    Game.speed = _speed;


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
            }
        }
    }
}
