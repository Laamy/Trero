#region

using System;
using System.Windows.Forms;
using Trero.ClientBase;
using Trero.ClientBase.KeyBase;
using Trero.ClientBase.UIBase;
using Trero.ClientBase.VersionBase;

#endregion

namespace Trero.Modules
{
    internal class MineplexFlyv2 : Module
    {
        public MineplexFlyv2() : base("BhopFlight", (char)0x07, "Flies", "MineplexFlight version 2")
        {
        } // Not defined

        public override void OnEnable()
        {
            foreach (Module mod in Program.Modules)
                if (mod.name == "MineplexFly" || mod.name == "Bhop")
                    mod.OnEnable();
            base.OnEnable();
        }

        public override void OnDisable()
        {
            foreach (Module mod in Program.Modules)
                if (mod.name == "MineplexFly" || mod.name == "Bhop")
                    mod.OnDisable();
            base.OnDisable();
        }
    }
}