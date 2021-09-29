#region

using System;
using Trero.ClientBase;

#endregion

namespace Trero.Modules
{
    internal class TestModule : Module
    {
        public TestModule() : base("TestModule", (char)0x07)
        {
        } // Not defined

        public override void OnTick()
        {
            //Faketernal.ClientObj.createClientObj(); // tellraw externally
            //Faketernal.ClientObj.setClientObj(Faketernal.ClientObj.getMessageAt(0));

            //var obj = Faketernal.ClientObj.getMessageAt(0);
            //obj.message = "yes";
            //Console.WriteLine(obj.addr.ToString("X"));

            //Console.WriteLine(Faketernal.ClientObj.chatInstance.ToString("X"));
        }
    }
}