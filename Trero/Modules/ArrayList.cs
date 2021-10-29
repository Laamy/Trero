#region

using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trero.ClientBase.UIBase;
using Trero.Modules.vModuleExtra;

#endregion

namespace Trero.Modules
{
    internal class ArrayList : Module
    {
        public ArrayList() : base("ArrayList", (char)0x07, "Visual")
        {
            addBypass(new BypassBox(new string[] { "Theme: Trero" }));
            addBypass(new BypassBox(new string[] { "Size: 24", "Size: 32", "Size: 12" }));
        }

        public override void OnEnable()
        {
            base.OnEnable();

            Overlay.handle.Paint += renderArrayList;
        }

        public override void OnDisable()
        {
            base.OnDisable();

            Overlay.handle.Paint -= renderArrayList;
        }

        Font df = new Font("Arial", 24, FontStyle.Bold);

        Brush brush1 = new SolidBrush(Color.FromArgb(44, 44, 44));
        Brush brush2 = new SolidBrush(Color.FromArgb(33, 33, 33));
        Brush stringColour = new SolidBrush(Color.FromArgb(200, 200, 200));

        private void renderArrayList(object sender, PaintEventArgs e)
        {
            switch (bypasses[1].curIndex)
            {
                case 0:
                    if (df.Size != 24)
                        df = new Font("Arial", 24, FontStyle.Bold);
                    break;
                case 1:
                    if (df.Size != 32)
                        df = new Font("Arial", 32, FontStyle.Bold);
                    break;
                case 2:
                    if (df.Size != 12)
                        df = new Font("Arial", 12, FontStyle.Bold);
                    break;
            }
            if (enabled)
            {
                int loop = 0;
                foreach (Module mod in Program.Modules) // get all modules
                {
                    switch (mod.enabled)
                    {
                        case true:
                            switch (bypasses[0].curIndex)
                            {
                                case 0:
                                    string name = mod.name;
                                    if (mod.keybind != 0x07)
                                        name = mod.name + $" [{mod.keybind}]";
                                    var c = e.Graphics.MeasureString(name, df);
                                    e.Graphics.FillRectangle(brush1, Overlay.handle.Width - c.Width - 5 - (df.Size / 4), c.Height * loop, 5 + (df.Size / 4), df.Size * 1.66f);
                                    e.Graphics.FillRectangle(brush2, Overlay.handle.Width - c.Width, c.Height * loop, c.Width, df.Size * 1.66f);
                                    e.Graphics.DrawString(name, df, stringColour, Overlay.handle.Width - c.Width, c.Height * loop);
                                    break;
                            }
                            loop++;
                            break;
                    }
                }
            } // ArrayList render code!
        }
    }
}