#region

using Trero.ClientBase;
using Trero.ClientBase.VersionBase;
using Trero.Modules.vModuleExtra;

#endregion

namespace Trero.Modules
{
    internal class Glide : Module
    {
        public Glide() : base("Glide", (char)0x07, "Player", "Glide down like a bird")
        {
            addBypass(new BypassBox(new string[] { "Trero", "Horion", "None" }));
        } // Not defined

        public override void OnTick()
        {
            if (Game.isNull) return;

            if (bypasses[0].curIndex == 0)
                MCM.writeFloat(Game.localPlayer + VersionClass.GetData("velocity") + 4, -0.01f);
            if (bypasses[0].curIndex == 1)
                MCM.writeFloat(Game.localPlayer + VersionClass.GetData("velocity") + 4, -0.1f);
            if (bypasses[0].curIndex == 2)
                MCM.writeFloat(Game.localPlayer + VersionClass.GetData("velocity") + 4, 0f);
        }
    }
}