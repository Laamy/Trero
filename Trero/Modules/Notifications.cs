#region

using System;
using System.Windows.Forms;
using Trero.ClientBase;
using Trero.ClientBase.KeyBase;
using Trero.ClientBase.UIBase.Notifications;

#endregion

namespace Trero.Modules
{
    internal class Notifications : Module
    {
        public Notifications() : base("Notifications", (char)0x07, "Visual")
        {
        } // 0x07 = no keybind

        public override void OnEnable()
        {
            base.OnEnable();
        }

        public override void OnDisable()
        {
            base.OnDisable();
        }
    }
}
