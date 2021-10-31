#region

using System.Windows.Forms;
using Trero.ClientBase;
using Trero.ClientBase.KeyBase;
using Trero.ClientBase.VersionBase;
using Trero.Modules.vModuleExtra;

#endregion

namespace Trero.Modules
{
    internal class Spider : Module
    {
        public Spider() : base("Spider", (char)0x07, "Player", "Climb up walls, just like a spider!")
        {
        } // Not defined

        public override void OnTick()
        {
            float speed = 0.4f;

            if (Game.touchingObject != 1) return;
            if (Keymap.GetAsyncKeyState(Keys.Space))
                MCM.writeFloat(Game.localPlayer + VersionClass.GetData("velocity") + 4, speed);
            else MCM.writeFloat(Game.localPlayer + VersionClass.GetData("velocity") + 4, -speed);
        }
    }
}