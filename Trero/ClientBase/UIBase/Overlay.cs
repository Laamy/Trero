using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
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

            new Thread(() => {
                while (!Program.quit)
                {
                    try
                    {
                        Invoke((MethodInvoker)delegate {
                            MCM.RECT rect = MCM.getMinecraftRect();

                            Placement e = new Placement();
                            GetWindowPlacement(MCM.mcWinHandle, ref e); // Change window size if fullscreen to match extra offsets
                            int vE = 0;
                            int vA = 0;
                            int vB = 0;
                            int vC = 0;
                            if (e.showCmd == 3) // Perfect window offsets
                            {
                                vE = 8;
                                vA = 2;

                                vB = 9; // these have extra because of the windows shadow effect
                                vC = 3;
                            }

                            Location = new Point(rect.Left + 9 + vA, rect.Top + 35 + vE); // Title bar is 32 pixels high
                            Size = new Size(rect.Right - rect.Left - 18 - vC, rect.Bottom - rect.Top - 44 - vB);

                            if (MCM.isMinecraftFocused() && !TopMost)
                                TopMost = true;
                            if (!MCM.isMinecraftFocused() && TopMost)
                            {
                                TopMost = false;
                                SetWindowPos(Handle, new IntPtr(1), 0, 0, 0, 0, 2 | 1 | 10);
                            }
                        });
                    }
                    catch { }
                }
            }).Start();
            
            new Thread(() => { // Auto focus
                Thread.Sleep(10);
                Invoke((MethodInvoker)delegate {
                    Focus();
                });
            }).Start();
        }

        [DllImport("user32.dll")] static extern IntPtr GetForegroundWindow();
        [DllImportAttribute("User32.dll")]  static extern IntPtr SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")] static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        [DllImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] static extern bool GetWindowPlacement(IntPtr hWnd, ref Placement lpwndpl);

        private struct Placement
        {
            public int length;
            public int flags;
            public int showCmd;
            public Point ptMinPosition;
            public Point ptMaxPosition;
            public Rectangle rcNormalPosition;
        }

        public static Overlay handle;

        private void timer1_Tick(object sender, EventArgs e)
        {
        }

        Font df = new Font(FontFamily.GenericSansSerif, 12f);

        private void Overlay_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 22, 22, 44)), new Rectangle(0, Size.Height - 2 - (4 * 16), (int)e.Graphics.MeasureString("ClientInstance: " + Game.clientInstance.ToString("X"), new Font(FontFamily.GenericSansSerif, 12f)).Width + 4, Size.Height - (4 * 16)));

            e.Graphics.DrawString("Trero Template", df, Brushes.Orange, new PointF(0, 0));

            e.Graphics.DrawString("ClientInstance: " + Game.clientInstance.ToString("X"), df, Brushes.Orange, new PointF(0, Size.Height - 6 - (4 * 14)));
            e.Graphics.DrawString("Pos: " + Game.position, df, Brushes.Orange, new PointF(0, Size.Height - 6 - (3 * 14)));
            e.Graphics.DrawString("Players: " + Game.getPlayers().Count, df, Brushes.Orange, new PointF(0, Size.Height - 6 - (2 * 14)));
            e.Graphics.DrawString("Entities: " + Game.getEntites().Count, df, Brushes.Orange, new PointF(0, Size.Height - 6 - (1 * 14)));

            //e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 22, 22, 44)), new Rectangle(0, 0, Size.Width, Size.Height));
        }
    }
}
