#region

using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Trero.ClientBase;
using Trero.ClientBase.UIBase;

#endregion

namespace Trero.Modules
{
    internal class Teleport : Module
    {
        public Teleport() : base("Teleport", (char)0x07, "World", "Bring up the trero teleportation menu")
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
                        if (Overlay.handle != null && (string)ct.Tag == "TeleportUI")
                            ct.Visible = true;

                    Overlay.handle.ResumeLayout();
                });
            }
            catch
            {
            }
        }

        public override void OnDisable()
        {
            try
            {
                Overlay.handle.Invoke((MethodInvoker)delegate
                {

                    Overlay.handle.SuspendLayout();

                    foreach (Control ct in Overlay.handle.Controls)
                        if (Overlay.handle != null && (string)ct.Tag == "TeleportUI")
                            ct.Visible = false;

                    Overlay.handle.ResumeLayout();
                });
            }
            catch
            {
            }
        }
    }
}