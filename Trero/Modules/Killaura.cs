using System;
using System.Windows.Forms;
using Trero.ClientBase;
using Trero.ClientBase.FaketernalBase;
using Trero.ClientBase.KeyBase;
using Trero.ClientBase.VersionBase;

namespace Trero.Modules
{
    class Killaura : Module
    {
        public Killaura() : base("Killaura", (char)0x07, "Comabat")
        {
            Keymap.keyEvent += keyPress;
        }// Not defined

        private void keyPress(object sender, KeyEvent e)
        {
            if (e.vkey == vKeyCodes.KeyDown)
            {

            }
        }

        public override void onTick()
        {
            var ent = Game.getClosestPlayer();
            if (ent == null) return;

            if (ent == null) return;

            Vector3 pos = ent.position;

            if (Game.position.Distance(pos) < 6)
            {
                Mouse.MouseEvent(Mouse.MouseEventFlags.MOUSEEVENTF_LEFTDOWN);
            }
        }
    }
}
