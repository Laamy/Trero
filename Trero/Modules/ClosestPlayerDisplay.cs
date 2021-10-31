#region

using System.Windows.Forms;
using Trero.ClientBase.UIBase;

#endregion

namespace Trero.Modules
{
    internal class ClosestPlayerDisplay : Module
    {
        public ClosestPlayerDisplay() : base("ClosestPlayerDisplay", (char)0x07, "Visual", "Display the closest players information")
        {
        } // Not defined

        public override void OnEnable()
        {
            base.OnEnable();
            foreach (Control ct in Overlay.handle.Controls)
                if (ct.Name == "panel3")
                    ct.Visible = true;
        }

        public override void OnDisable()
        {
            base.OnDisable();
            foreach (Control ct in Overlay.handle.Controls)
                if (ct.Name == "panel3")
                    ct.Visible = false;
        }
    }
}