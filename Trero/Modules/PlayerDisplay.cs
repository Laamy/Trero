#region

using System.Windows.Forms;
using Trero.ClientBase.UIBase;

#endregion

namespace Trero.Modules
{
    internal class PlayerDisplay : Module
    {
        public PlayerDisplay() : base("PlayerDisplay", (char)0x07, "Visual", "Display a list of players around you with their positions")
        {
        } // Not defined

        public override void OnEnable()
        {
            base.OnEnable();
            foreach (Control ct in Overlay.handle.Controls)
                if (ct.Name == "panel1")
                    ct.Visible = true;
        }

        public override void OnDisable()
        {
            base.OnDisable();
            foreach (Control ct in Overlay.handle.Controls)
                if (ct.Name == "panel1")
                    ct.Visible = false;
        }
    }
}