#region

using System.Windows.Forms;
using Trero.ClientBase;
using Trero.ClientBase.KeyBase;
using Trero.ClientBase.VersionBase;
using Trero.Modules.vModuleExtra;

#endregion

namespace Trero.Modules
{
    internal class SlimeWall : Module
    {
        public SlimeWall() : base("SlimeWall", (char)0x07, "Player")
        {
            addBypass(new BypassBox(new string[] { "Hive", "Fast", "Faster", "Slow" }));
        } // Not defined

        public override void OnTick()
        {
            float speed = 0.1f;

            if (bypasses[0].curIndex == 0)
                speed = 0.1f;
            if (bypasses[0].curIndex == 1)
                speed = 0.2f;
            if (bypasses[0].curIndex == 2)
                speed = 0.4f;
            if (bypasses[0].curIndex == 3)
                speed = 0.05f;

            if (Game.touchingObject != 1) return;
            if (Keymap.GetAsyncKeyState(Keys.Space))
                MCM.writeFloat(Game.localPlayer + VersionClass.GetData("velocity") + 4, speed);
            else MCM.writeFloat(Game.localPlayer + VersionClass.GetData("velocity") + 4, -speed);
        }
    }
}