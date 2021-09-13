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
        public ClickGUI() : base("ClickGUI", (char)(int)Keys.Insert, "Visual", true) { } // Not defined
        public override void onEnable()
        {
            try
            {
                Overlay.handle.Invoke((MethodInvoker)delegate {
                    foreach (Control ct in Overlay.handle.Controls)
                    {
                        if (Overlay.handle != null && ct.Tag == "Category")
                            ct.Visible = true;
                    }
                });
            }
            catch { }
            base.onEnable();
        }
        public override void onDisable()
        {
            try
            {
                Overlay.handle.Invoke((MethodInvoker)delegate {
                    foreach (Control ct in Overlay.handle.Controls)
                    {
                        if (Overlay.handle != null && ct.Tag == "Category")
                            ct.Visible = false;
                    }
                });
            }
            catch { }
            base.onDisable();
        }
    }
}
