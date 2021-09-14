#region

using Trero.ClientBase;
using Trero.ClientBase.VersionBase;

#endregion

namespace Trero.Modules
{
    internal class Glide : Module
    {
        public Glide() : base("Glide", (char)0x07, "Player")
        {
        } // Not defined

        public override void OnTick()
        {
            if (Game.isNull) return;

            MCM.writeFloat(Game.localPlayer + VersionClass.GetData("velocity") + 4, -0.01f);
        }
    }
}