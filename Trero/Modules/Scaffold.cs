using System;
using System.Linq;
using Trero.ClientBase;
using Trero.ClientBase.KeyBase;
using Trero.ClientBase.VersionBase;
using Trero.Modules.vModuleExtra;

namespace Trero.Modules
{
    class Scaffold : Module
    {

        float savedY = 0f;
        public Scaffold() : base("Scaffold", (char)0x07, "World", "Place blocks under you automatically") {
            addBypass(new BypassBox(new string[] { "Speed: Default", "Speed: 1", "Speed: 2" }));
            addBypass(new BypassBox(new string[] { "Place Mode: Auto ", "Place Mode: Manual" }));
            addBypass(new BypassBox(new string[] { "AntiFall: true", "AntiFall: false" }));
            //addBypass(new BypassBox(new string[] { "No Blocks Stop: true", "No Blocks Stop: false" })); HeldItemCount is still broken
            //addBypass(new BypassBox(new string[] { "Mode: JumpBridge", "Mode: Beneath", "Mode: StairCase" }));
        }

        public override void OnEnable()
        {
            base.OnEnable();

            savedY = Game.position.y;
            MCM.freezeBytes(Game.level + VersionClass.GetData("SelectedBlock") + 4, MCM.int2Bytes((int)Game.position.y - 1));// this is just to stop annoying messages >:c
        }

        public override void OnDisable()
        {
            base.OnDisable();

            OverrideBase.lookingAtBlock = true;
            MCM.unfreezeBytes(Game.level + VersionClass.GetData("SelectedBlock") + 4);
        }

        public override void OnTick()
        {
            base.OnTick();

            if (MCM.isGameFocused())
            {
                Game.isLookingAtBlock = 0;

                var scaffoldPos = Game.exactPos;
                scaffoldPos.y = (int)savedY - 1;
                if (scaffoldPos.y != (int)savedY - 2)
                {
                    Game.SelectedBlock = scaffoldPos;
                }
                Game.SideSelect = 1;

                switch (bypasses[2].curIndex)
                {
                    case 0:
                        if(Game.position.y < (int)savedY)
                        {
                            if (Game.heldItemCount == 0)//HeldItemCount is still broken
                            {
                                Game.velocity = Base.Vec3();
                            }
                            else
                            {
                                Game.onGround = true;
                                Game.velocity = Base.Vec3();
                                Game.velocity = Base.Vec3(0, 0.3f);
                            }
                        }
                        break;
                }
                

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
                        if (MCM.isGameFocused())
                        {
                            Mouse.MouseEvent(Mouse.MouseEventFlags.MOUSEEVENTF_RIGHTDOWN);
                        }
                        break;
                }
            }
        }
    }
}
