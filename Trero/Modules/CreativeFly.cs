#region

using Trero.ClientBase;

#endregion

namespace Trero.Modules
{
    internal class CreativeFly : Module
    {
        public CreativeFly() : base("CreativeFly", (char)0x07, "Flies")
        {
        } // Not defined

        public override void OnTick()
        {
            Game.isFlying = true;
        }

        public override void OnDisable()
        {
            base.OnDisable();

            Game.isFlying = false;
        }
    }
}