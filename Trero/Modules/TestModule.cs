using System;
using System.Windows.Forms;
using Trero.ClientBase;
using Trero.ClientBase.KeyBase;
using Trero.ClientBase.VersionBase;

namespace Trero.Modules
{
    class TestModule : Module
    {
        public TestModule() : base("TestModule", (char)0x07, "Other") { } // Not defined
        public override void onTick()
        {
            FakePacket.ClientObj.createClientObj(); // tellraw externally
            FakePacket.ClientObj.setClientObj("[Atani1.17]: sex");
        }
    }
}
