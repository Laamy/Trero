using System;
using System.Windows.Forms;
using Trero.ClientBase;
using Trero.ClientBase.FaketernalBase;
using Trero.ClientBase.KeyBase;
using Trero.ClientBase.VersionBase;

namespace Trero.Modules
{
    class TestModule : Module
    {
        public TestModule() : base("TestModule", (char)0x07, "Other") { } // Not defined
        public override void onTick()
        {
            //Faketernal.ClientObj.createClientObj(); // tellraw externally
            //Faketernal.ClientObj.setClientObj(Faketernal.ClientObj.getMessageAt(0));

            MessageObj obj = Faketernal.ClientObj.getMessageAt(0);
            //obj.message = "yes";
            Console.WriteLine(obj.addr.ToString("X"));

            //Console.WriteLine(Faketernal.ClientObj.chatInstance.ToString("X"));
        }
    }
}
