#region

using System.Collections.Generic;
using System.Threading;
using Trero.ClientBase;
using Trero.Modules.vModuleExtra;

#endregion

namespace Trero.Modules
{
    internal class Blink : Module
    {
        List<Vector3> posList = new List<Vector3>();
        Vector3 firstPos = Base.Vec3();

        public Blink() : base("Blink", (char)0x07, "Exploits", "Stops sending packets then sends them all at once")
        {
        }

        public override void OnEnable()
        {
            base.OnEnable();

            OverrideBase.CanSendPackets = false;
            firstPos = Game.position;
            posList.Clear();
        }

        public override void OnDisable()
        {
            base.OnDisable();

            Game.teleport(firstPos);

            OverrideBase.CanSendPackets = true;

            Game.timer = 300;
            foreach (var c in posList)
            {
                Game.teleport(c);
                Thread.Sleep(1);
            }
            Game.timer = 20;
        }
    }
}