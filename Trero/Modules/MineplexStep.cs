#region

using System;
using System.Windows.Forms;
using Trero.ClientBase;
using Trero.ClientBase.KeyBase;
using Trero.ClientBase.VersionBase;
using Trero.Modules.vModuleExtra;

#endregion

namespace Trero.Modules
{
    internal class MineplexStep : Module
    {
        public MineplexStep() : base("MineplexStep", (char)0x07, "Player", "Step made for mineplex and other servers that block it")
        {
            addBypass(new BypassBox(new string[] { "Default", "Fast", "Super Slow", "Slow" }));
        } // Not defined

        public override void OnTick()
        {
            float speed = 0.4f;

            if (Game.walkingIntoBlock != 1) return;

            switch (bypasses[0].curIndex)
            {
                case 0:
                    speed = 0.4f;
                    break;
                case 1:
                    speed = 0.6f;
                    break;
                case 2:
                    speed = 0.1f;
                    break;
                case 3:
                    speed = 0.2f;
                    break;
            }

            MCM.writeFloat(Game.localPlayer + VersionClass.GetData("velocity") + 4, speed);
        }
    }
}