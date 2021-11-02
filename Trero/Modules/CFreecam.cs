#region

using System;
using System.Windows.Forms;
using Trero.ClientBase;
using Trero.ClientBase.KeyBase;

#endregion

namespace Trero.Modules
{
    internal class CFreecam : Module
    {
        Vector3 savedCoords = Base.Vec3();
        Vector3 savedVel = Base.Vec3();
        bool flying = false;
        public CFreecam() : base("Freecam", (char)0x07, "Others", "Freely move around client sidedly") { }

        public override void OnEnable() // just need nopacket lmao
        {
            base.OnEnable();

            OverrideBase.CanSendPackets = false;

            savedCoords = Game.position;
            savedVel = Game.velocity;
            flying = Game.isFlying;

        }

        public override void OnDisable()
        {
            base.OnDisable();

            Game.teleport(savedCoords);
            Game.velocity = savedVel;
            Game.isFlying = !flying;

            OverrideBase.CanSendPackets = true;
        }

        public override void OnTick()
        {
            base.OnTick();

            var pos = Game.position;
            var pos2 = Game.position;

            pos2.x += 0.6f;
            pos2.z += 0.6f;

            Game.teleport(new AABB(pos, pos2));

            Game.isFlying = false;
        }
    }
}