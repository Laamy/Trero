#region

using Trero.ClientBase;

#endregion

namespace Trero.Modules
{
    internal class TPAura : Module
    {
        private int _flicker;

        public TPAura() : base("TPAura", (char)0x07, "Exploits", "Teleport around the closest player")
        {
        } // Not defined

        public override void OnTick()
        {
            if (Game.isNull) return;
            _flicker++;

            if (_flicker != 4) return;
            _flicker = 0;
            if (Game.isNull) return;
            _flicker++;

            if (_flicker != 300) return;
            _flicker = 0;
            Game.onGround = false;
        }
    }
}