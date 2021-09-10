using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trero.ClientBase;
using Trero.ClientBase.EntityBase;
using Trero.ClientBase.UIBase;

namespace Trero.Modules
{
    class PlayerDisplay : Module
    {
        public PlayerDisplay() : base("PlayerDisplay", (char)0x07) { } // Not defined
        public override void onEnable()
        {
            base.onEnable();
            foreach (Control ct in Overlay.handle.Controls)
            {
                if (ct.Name == "panel1")
                    ct.Visible = true;
            }
        }
        public override void onDisable()
        {
            base.onDisable();
            foreach (Control ct in Overlay.handle.Controls)
            {
                if (ct.Name == "panel1")
                    ct.Visible = false;
            }
        }
    }
}
