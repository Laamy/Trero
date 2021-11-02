#region

using Trero.ClientBase;

#endregion

namespace Trero.Modules
{
    internal class NoLagBack : Module
    {
        public NoLagBack() : base("NoLagBack", (char)0x07, "Exploits", "Useless")
        {
        } // Not defined
        public override void OnTick()
        {
            base.OnTick();

            var pos = Game.position;

            pos.y += 0.001f;
            pos.x += 0.001f;
            pos.z += 0.001f;

            Game.teleport(pos);
        }
    }
}