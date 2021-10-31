#region

using System.Threading;
using System.Windows.Forms;
using Trero.ClientBase.UIBase;

#endregion

namespace Trero.Modules
{
    internal class ClickGUI : Module
    {
        public ClickGUI() : base("ClickGUI", (char)(int)Keys.Insert, "Visual", "List all categories and their modules in their own little windows", true)
        {
        } // Not defined

        public override void OnEnable()
        {
            try
            {
                Overlay.handle.Invoke((MethodInvoker)delegate
                {

                    Overlay.handle.SuspendLayout();

                    foreach (Control ct in Overlay.handle.Controls)
                        if (Overlay.handle != null && (string)ct.Tag == "Category")
                            ct.Visible = true;

                    Overlay.handle.ResumeLayout();
                });
            }
            catch
            {
                // ignored
            }

            base.OnEnable();
        }

        public override void OnDisable()
        {
            try
            {
                Overlay.handle.Invoke((MethodInvoker)delegate
                {
                    Overlay.handle.SuspendLayout();

                    foreach (Control ct in Overlay.handle.Controls)
                        if (Overlay.handle != null && (string)ct.Tag == "Category")
                            ct.Visible = false;

                    Overlay.handle.ResumeLayout();
                });
            }
            catch
            {
                // ignored
            }

            base.OnDisable();
        }
    }
}