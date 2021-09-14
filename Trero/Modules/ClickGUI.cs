#region

using System.Windows.Forms;
using Trero.ClientBase.UIBase;

#endregion

namespace Trero.Modules
{
    internal class ClickGUI : Module
    {
        public ClickGUI() : base("ClickGUI", (char)(int)Keys.Insert, "Visual", true)
        {
        } // Not defined

        public override void OnEnable()
        {
            try
            {
                Overlay.handle.Invoke((MethodInvoker)delegate
                {
                    foreach (Control ct in Overlay.handle.Controls)
                        if (Overlay.handle != null && (string)ct.Tag == "Category")
                            ct.Visible = true;
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
                    foreach (Control ct in Overlay.handle.Controls)
                        if (Overlay.handle != null && (string)ct.Tag == "Category")
                            ct.Visible = false;
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