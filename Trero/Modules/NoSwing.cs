#region

using Trero.ClientBase;

#endregion

namespace Trero.Modules
{
    internal class NoSwing : Module
    {
        public NoSwing() : base("NoSwing", (char)0x07, "Visual", "Remove the swinging animation")
        {
        } // Not defined

        public override void OnTick()
        {
            if (Game.isNull) return;

            Game.swingAn = 0;
        }
    }
}