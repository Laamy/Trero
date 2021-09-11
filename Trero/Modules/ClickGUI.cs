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
    class ClickGUI : Module
    {
        public ClickGUI() : base("ClickGUI", (char)(int)Keys.Insert, "Visual") { } // Not defined
        public override void onEnable()
        {
            base.onEnable();
            foreach (Control ct in Overlay.handle.Controls)
            {
                if (ct.Tag == "Category")
                    ct.Visible = true;
            }
        }
        public override void onDisable()
        {
            base.onDisable();
            foreach (Control ct in Overlay.handle.Controls)
            {
                if (ct.Tag == "Category")
                    ct.Visible = false;
            }
        }
    }
}
