#region

using Trero.ClientBase;

#endregion

namespace Trero.Modules
{
    internal class CreativeFly : Module
    {
        public CreativeFly() : base("CreativeFly", (char)0x07, "Player")
        {
        } // Not defined

        public override void OnTick()
        {
            Game.isFlying = true;
        }
    }
}