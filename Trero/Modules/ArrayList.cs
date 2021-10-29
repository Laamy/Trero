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
    internal class ArrayList : Module // "@yaami<3 can you code me a dad" - Mew_.IsSpecial 2021
    {
        public ArrayList() : base("ArrayList", (char)0x07, "Visual")
        {
            addBypass(new BypassBox(new string[] { "Theme: Trero", "Theme: FontOnly" }));
            addBypass(new BypassBox(new string[] { "Size: 24", "Size: 32", "Size: 12" }));
            addBypass(new BypassBox(new string[] { "ShowKeybind: True", "ShowKeybind: False" }));
            addBypass(new BypassBox(new string[] { "Font: Arial", "Font: Impact" }));
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

        Font df = new Font("Arial", 24, FontStyle.Regular);

        Brush brush1 = new SolidBrush(Color.FromArgb(44, 44, 44));
        Brush brush2 = new SolidBrush(Color.FromArgb(33, 33, 33));
        Brush stringColour = new SolidBrush(Color.FromArgb(200, 200, 200));

        private void renderArrayList(object sender, PaintEventArgs e)
        {
            string font = "Arial";
            if (bypasses[3].curIndex == 0)
                font = "Arial";
            if (bypasses[3].curIndex == 1)
                font = "Impact";

            switch (bypasses[1].curIndex)
            {
                case 0:
                    if (df.Size != 24)
                        df = new Font(font, 24, FontStyle.Regular);
                    break;
                case 1:
                    if (df.Size != 32)
                        df = new Font(font, 32, FontStyle.Regular);
                    break;
                case 2:
                    if (df.Size != 12)
                        df = new Font(font, 12, FontStyle.Regular);
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
                                    if (mod.keybind != 0x07 && bypasses[2].curIndex == 0)
                                        name = mod.name + $" [{mod.keybind}]";
                                    var c = e.Graphics.MeasureString(name, df);
                                    e.Graphics.FillRectangle(brush1, Overlay.handle.Width - c.Width - 5 - (df.Size / 4), c.Height * loop, 5 + (df.Size / 4), df.Size * 1.66f);
                                    e.Graphics.FillRectangle(brush2, Overlay.handle.Width - c.Width, c.Height * loop, c.Width, df.Size * 1.66f);
                                    e.Graphics.DrawString(name, df, stringColour, Overlay.handle.Width - c.Width, c.Height * loop);
                                    break;
                                case 1:
                                    string namec = mod.name;
                                    if (mod.keybind != 0x07 && bypasses[2].curIndex == 0)
                                        namec = mod.name + $" [{mod.keybind}]";
                                    var cc = e.Graphics.MeasureString(namec, df);
                                    e.Graphics.DrawString(namec, df, stringColour, Overlay.handle.Width - cc.Width, cc.Height * loop);
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