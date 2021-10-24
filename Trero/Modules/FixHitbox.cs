#region

using System;
using System.Windows.Forms;
using Trero.ClientBase;
using Trero.ClientBase.KeyBase;

#endregion

namespace Trero.Modules
{
    internal class FixHitbox : Module
    {
        public FixHitbox() : base("FixHitbox", (char)0x07, "Others")
        {
        } // 0x07 = no keybind

        public override void OnEnable()
        {
            base.OnEnable();

            Game.teleport(Game.position);
        }
    }
}