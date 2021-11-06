#region

using System.Windows.Forms;
using Trero.ClientBase;
using Trero.ClientBase.KeyBase;

#endregion

namespace Trero.Modules
{
    internal class ClickTP : Module
    {
        public ClickTP() : base("ClickTP", (char)0x07, "Exploits", "Right click to teleport to the position your looking at")
        {
            Keymap.keyEvent += KeyPress;
        }

        private void KeyPress(object sender, KeyEvent e)
        {
            if (e.vkey != VKeyCodes.KeyDown || !enabled || e.key != (Keys)0x02) return;

            var ivec = Game.SelectedBlock;

            if (ivec.x == 0 || ivec.y == 0 || ivec.z == 0) return;

            var newPos = Base.Vec3(ivec.x, ivec.y + 1, ivec.z);

            Game.position = newPos;
        }
    }
}