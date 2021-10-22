#region

using Trero.ClientBase;
using Trero.ClientBase.VersionBase;

#endregion

namespace Trero.Modules
{
    internal class Jesus : Module
    {
        public Jesus() : base("Jesus", (char)0x07, "Player")
        {
        } // Not defined

        public override void OnTick()
        {
            if (Game.isNull) return;

            if (Game.isInWater || Game.isInLava)
            {
                MCM.writeFloat(Game.localPlayer + VersionClass.GetData("velocity") + 4, 0.01f);
                Game.onGround = true;
            }
        }
    }
}