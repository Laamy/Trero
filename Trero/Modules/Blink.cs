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

        public override void OnTick()
        {
            posList.Add(Game.position);
        }

        public override void OnDisable()
        {
            base.OnDisable();

            Game.teleport(firstPos);

            OverrideBase.CanSendPackets = true;

            Game.timer = 60;
            //posList.Reverse();
            for (int _timer = 0; _timer < 64; ++_timer)
                foreach (var c in posList)
                    Game.teleport(posList[posList.Count - 1]);
            Game.timer = 20;
        }
    }
}