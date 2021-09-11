using System;
using System.Windows.Forms;
using Trero.ClientBase;
using Trero.ClientBase.KeyBase;
using Trero.ClientBase.VersionBase;

namespace Trero.Modules
{
    class SlimeWall : Module
    {
        public SlimeWall() : base("SlimeWall", (char)0x07, "Player") { } // Not defined
        public override void onTick()
        {
            if (Game.touchingObject == 1)
            {
                if (Keymap.GetAsyncKeyState(Keys.Space))
                    MCM.writeFloat(Game.localPlayer + VersionClass.getData("velocity") + 4, 0.1f);
                else MCM.writeFloat(Game.localPlayer + VersionClass.getData("velocity") + 4, -0.1f);
            }
        }
    }
}
