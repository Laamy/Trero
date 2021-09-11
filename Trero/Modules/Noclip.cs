using System.Windows.Forms;
using Trero.ClientBase;
using Trero.ClientBase.UIBase;

namespace Trero.Modules
{
    class Noclip : Module
    {
        public Noclip() : base("Noclip", (char)0x07, "Exploits") { } // Not defined
        public override void onTick()
        {
            Vector3 pos = Game.position;
            Vector3 pos2 = Game.position;

            pos2.x += 0.6f;
            pos2.y -= 1.8f;
            pos2.z += 0.6f;

            Game.teleport(new AABB(pos, pos2));
        }
        public override void onDisable()
        {
            base.onDisable();
            Game.teleport(Game.position);
        }
    }
}
