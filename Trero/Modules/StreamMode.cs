#region

using Trero.ClientBase;
using Trero.Modules.vModuleExtra;

#endregion

namespace Trero.Modules
{
    internal class StreamMode : Module
    {
        public StreamMode() : base("StreamMode", (char)0x07, "World")
        {
            storedusr = Game.username;
        } // 0x07 = no keybind

        string storedusr = "null";

        public override void OnEnable()
        {
            base.OnEnable();
            storedusr = Game.username;
            Game.username = "TreroExploiter";
        }

        public override void OnDisable()
        {
            base.OnDisable();
            Game.username = storedusr;
        }
    }
}