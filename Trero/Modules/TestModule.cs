using System;
using Trero.ClientBase;
using Trero.ClientBase.KeyBase;

namespace Trero.Modules
{
    class TestModule : Module
    {
        public TestModule() : base("TestModule", (char)0x07, "Other", "Developer test module") { }

        public override void OnEnable()
        {
            base.OnEnable();

            MCM.freezeBytes(Game.localPlayer + 0x138, MCM.float2Bytes(Game.bodyRots.x));
        }

        public override void OnDisable()
        {
            base.OnDisable();

            MCM.unfreezeBytes(Game.localPlayer + 0x0);
        }
    }
}
