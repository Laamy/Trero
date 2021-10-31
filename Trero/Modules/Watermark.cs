#region

using System;
using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Trero.ClientBase;
using Trero.ClientBase.UIBase;

#endregion

namespace Trero.Modules
{
    internal class Watermark : Module
    {
        public Watermark() : base("Watermark", (char)0x07, "Visual", "Toggle trero's annoying watermark >:c", true)
        {
        } // Not defined

        public override void OnEnable()
        {
            base.OnEnable();

            try
            {
                Overlay.handle.Invoke((MethodInvoker)delegate
                {
                    foreach (Control ct in Overlay.handle.Controls)
                        if (Overlay.handle != null && (string)ct.Tag == "watermark")
                            ct.Visible = true;
                });
            }
            catch
            {
            }
        }

        public override void OnDisable()
        {
            base.OnDisable();

            try
            {
                Overlay.handle.Invoke((MethodInvoker)delegate
                {
                    foreach (Control ct in Overlay.handle.Controls)
                        if (Overlay.handle != null && (string)ct.Tag == "watermark")
                            ct.Visible = false;
                });
            }
            catch
            {
            }
        }
    }
}