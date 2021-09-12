using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trero.ClientBase;
using Trero.ClientBase.EntityBase;
using Trero.ClientBase.KeyBase;
using Trero.ClientBase.VersionBase;

namespace Trero.Modules
{
    class ClickTP : Module
    {
        public ClickTP() : base("ClickTP", (char)0x07, "Exploits") => Keymap.keyEvent += keyPress;

        private void keyPress(object sender, KeyEvent e)
        {
            if (e.vkey == vKeyCodes.KeyDown)
            {
                if (enabled)
                {
                    if (e.key.ToString() == "MButton")
                    {
                        iVector3 ivec = Game.SelectedBlock;

                        Vector3 newPos = Base.Vec3(ivec.x, ivec.y, ivec.z);

                        Game.position = newPos;
                    }
                }
            }
        }
    }
}
