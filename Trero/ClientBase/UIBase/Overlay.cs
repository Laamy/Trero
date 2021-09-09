using System;
using System.Drawing;
using System.Windows.Forms;
using Trero.ClientBase.KeyBase;

namespace Trero.ClientBase.UIBase
{
    public partial class Overlay : Form
    {
        public Overlay()
        {
            InitializeComponent();
            handle = this;

            TopMost = true;
            Focus();
        }

        public static Overlay handle;

        private void timer1_Tick(object sender, EventArgs e) // didnt wanna use this because its slow but... i have to unless i use some fancy degl methods for cross thread editing :(
        {
            MCM.RECT rect = MCM.getMinecraftRect();

            Location = new Point(rect.Left, rect.Top);
            Size = new Size(rect.Right - rect.Left, rect.Bottom - rect.Top - 6);

            if (!MCM.isMinecraftFocused() && Opacity != 0)
                Opacity = 0;
            if (MCM.isMinecraftFocused() && Opacity != 100)
                Opacity = 100;
        }

        private void Overlay_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawString("Trero Template", new Font(FontFamily.GenericSansSerif, 12f), Brushes.Orange, new PointF(10, 36 + (0 * 14)));
            e.Graphics.DrawString("ClientInstance: " + Game.clientInstance.ToString("X"), new Font(FontFamily.GenericSansSerif, 12f), Brushes.Orange, new PointF(10, Size.Height - 6 - (4 * 14)));
            e.Graphics.DrawString("Pos: " + Game.position, new Font(FontFamily.GenericSansSerif, 12f), Brushes.Orange, new PointF(10, Size.Height - 6 - (3 * 14)));
            e.Graphics.DrawString("Players: " + Game.getPlayers().Count, new Font(FontFamily.GenericSansSerif, 12f), Brushes.Orange, new PointF(10, Size.Height - 6 - (2 * 14)));
            e.Graphics.DrawString("Entities: " + Game.getEntites().Count, new Font(FontFamily.GenericSansSerif, 12f), Brushes.Orange, new PointF(10, Size.Height - 6 - (1 * 14)));
        }
    }
}
