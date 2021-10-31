using System;
using System.Linq;
using Trero.ClientBase;
using Trero.ClientBase.KeyBase;
using Trero.Modules.vModuleExtra;

namespace Trero.Modules
{
    class SurroundTest : Module
    {

        int placeTicks = 0;
        public SurroundTest() : base("Surround", (char)0x07, "World")
        {
            addBypass(new BypassBox(new string[] { "SelfSurround: False", "SelfSurround: true" }));
           // addBypass(new BypassBox(new string[] { "Disable When Done: False", "Disable When Done: true" }));
            
        }

        public override void OnEnable()
        {
            base.OnEnable();
        }

        public override void OnDisable()
        {
            base.OnDisable();
        }

        public override void OnTick()
        {
            base.OnTick();

            if (MCM.isMinecraftFocused())
            {
                Mouse.MouseEvent(Mouse.MouseEventFlags.MOUSEEVENTF_RIGHTDOWN);
                Game.Placing = 0;
                placeTicks++;
                Game.teleport(Base.Vec3((int)Game.position.x, (int)Game.position.y, (int)Game.position.z));
                switch (bypasses[0].curIndex)
                {
                    case 1:
                        //OverrideBase.lookingAtBlock = false;
                        Game.isLookingAtBlock = 0;
                        

                        Game.velocity = Base.Vec3();
                        
                        //layer1
                        iVector3 block1 = Base.IVec3((int)Game.position.x, (int)Game.position.y + 2, (int)Game.position.z);
                        iVector3 block2 = Base.IVec3((int)Game.position.x, (int)Game.position.y, (int)Game.position.z + 1);
                        iVector3 block3 = Base.IVec3((int)Game.position.x, (int)Game.position.y, (int)Game.position.z - 1);
                        iVector3 block4 = Base.IVec3((int)Game.position.x + 1, (int)Game.position.y, (int)Game.position.z);
                        iVector3 block5 = Base.IVec3((int)Game.position.x - 1, (int)Game.position.y, (int)Game.position.z);
                        //

                        //layer1 corners
                        iVector3 block6 = Base.IVec3((int)Game.position.x, (int)Game.position.y - 1, (int)Game.position.z);
                        iVector3 block7 = Base.IVec3((int)Game.position.x , (int)Game.position.y + 1, (int)Game.position.z + 1);
                        iVector3 block8 = Base.IVec3((int)Game.position.x, (int)Game.position.y + 1, (int)Game.position.z - 1);
                        iVector3 block9 = Base.IVec3((int)Game.position.x + 1, (int)Game.position.y + 1, (int)Game.position.z);
                        iVector3 block10 = Base.IVec3((int)Game.position.x - 1, (int)Game.position.y + 1, (int)Game.position.z);
                        //



                        if (placeTicks == 1)
                        {
                            this.setBlock(block1);
                        }
                        else if (placeTicks == 2)
                        {
                            this.setBlock(block1);
                        }
                        else if (placeTicks == 3)
                        {
                            this.setBlock(block2);
                        }
                        else if (placeTicks == 4)
                        {
                            this.setBlock(block3);
                        }
                        else if (placeTicks == 5)
                        {
                            this.setBlock(block4);
                        } 
                        else if (placeTicks == 6) {
                            this.setBlock(block5);
                            
                        }
                        else if (placeTicks == 7)
                        {
                            this.setBlock(block6);

                        }
                        else if (placeTicks == 8)
                        {
                            this.setBlock(block7);

                        }
                        else if (placeTicks == 9)
                        {
                            this.setBlock(block8);

                        }
                        else if (placeTicks == 10)
                        {
                            this.setBlock(block9);

                        }
                        else if (placeTicks == 11)
                        {
                            this.setBlock(block10);
                            placeTicks = 0;
                            
                            this.OnDisable();
                        }
                        break;
                }
            }
        }

        public void setBlock(iVector3 position)
        {
            int x = position.x;
            int y = position.y;
            int z = position.z;

            var blockPos = Game.exactPos;
            blockPos.x = x;
            blockPos.y = y;
            blockPos.z = z;
            Game.SelectedBlock = blockPos;
            Game.SideSelect = 4;
        }
    }
}
