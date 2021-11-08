using System;
using System.Linq;
using Trero.ClientBase;
using Trero.ClientBase.KeyBase;
using Trero.Modules.vModuleExtra;

namespace Trero.Modules
{
    class CScaffold : Module
    {

        float savedY = 0f;
        public CScaffold() : base("CScaffold", (char)0x07, "World", "Place blocks under you automatically (C)") {
        }

        public override void OnEnable()
        {
            base.OnEnable();
        }

        public override void OnDisable()
        {
            base.OnDisable();
        }

        public override void OnTick()
        {
            base.OnTick();
        }
    }
}
