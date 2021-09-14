#region

using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Trero.Modules;

#endregion

namespace Trero.ClientBase.UIBase
{
    public partial class Overlay : Form
    {
        public static Overlay handle;

        private Font _df = new Font(FontFamily.GenericSansSerif, 12f);

        private Point _mouseDownLocation;

        public Button vMod;

        public Overlay()
        {
            InitializeComponent();
            handle = this;

            new Thread(() =>
            {
                Thread.Sleep(100);
                Invoke((MethodInvoker)delegate { Focus(); });
                while (!Program.quit)
                {
                    Thread.Sleep(1);
                    // Thread.Sleep(1);
                    try
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            var rect = MCM.getMinecraftRect();

                            var e = new Placement();
                            GetWindowPlacement(MCM.mcWinHandle,
                                ref e); // Change window size if fullscreen to match extra offsets
                            var vE = 0;
                            var vA = 0;
                            var vB = 0;
                            var vC = 0;
                            if (e.showCmd == 3) // Perfect window offsets
                            {
                                vE = 8;
                                vA = 2;

                                vB = 9; // these have extra because of the windows shadow effect (Not exactly required but oh well)
                                vC = 3;
                            }

                            Location = new Point(rect.Left + 9 + vA, rect.Top + 35 + vE); // Title bar is 32 pixels high
                            Size = new Size(rect.Right - rect.Left - 18 - vC, rect.Bottom - rect.Top - 44 - vB);
                        });
                    }
                    catch
                    {
                    }
                }

                Application.Exit();
            }).Start();
            TopMost = true;
        }

        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy,
            uint uFlags);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowPlacement(IntPtr hWnd, ref Placement lpwndpl);

        private void timer1_Tick(object sender, EventArgs e)
        {
            var list = "";

            try
            {
                var vList = Game.getPlayers();
                list = "Players : " + vList.Count + "\r\n";
                list = vList.Aggregate(list,
                    (current, plr) =>
                        current + (int)Game.position.Distance(plr.position) + "b " + plr.username + "\r\n");
            }
            catch
            {
                // ignored
            }

            if (list == "")
                list = "No players";

            var calclist = TextRenderer.MeasureText(list, playerList.Font);

            if (panel5.Size != new Size(calclist.Width, calclist.Height + 20))
            {
                panel1.Size = new Size(calclist.Width + 20, calclist.Height + 24);
                panel5.Size = new Size(calclist.Width + 20, calclist.Height);
            }

            playerList.Text = list;

            try
            {
                var ent = Game.getClosestPlayer();

                if (ent == null)
                {
                    label2.Text = "None";
                    label2.Text = "";
                    label3.Text = "";
                    return;
                }

                var vec = Base.Vec3((int)ent.position.x, (int)ent.position.y, (int)ent.position.z);

                label1.Text = ent.username;
                label2.Text = vec.ToString();
                label3.Text = Game.position.Distance(vec) + "b";
            }
            catch
            {
            }
        }

        private void updateModule(Module mod, Button btn)
        {
            if (mod.name != btn.Name) return;
            switch (mod.enabled)
            {
                case true when btn.BackColor != Color.FromArgb(255, 39, 39, 39):
                    btn.BackColor = Color.FromArgb(255, 39, 39, 39);
                    break;
                case false when btn.BackColor == Color.FromArgb(255, 39, 39, 39):
                    btn.BackColor = Color.FromArgb(255, 44, 44, 44);
                    break;
            }
        }

        private void Overlay_Paint(object sender, PaintEventArgs e)
        {
            /*
            
            // e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 22, 22, 44)), new Rectangle(0, Size.Height - 2 - (6 * (int)df.Size + 4 * DrawingUtils.screenSize), (int)e.Graphics.MeasureString("ClientInstance: " + Game.clientInstance.ToString("X"), df).Width + 4, Size.Height - (4 * (int)df.Size + 4 * DrawingUtils.screenSize)));

            //e.Graphics.DrawString("Trero Template", df, Brushes.Orange, new PointF(0, 0));

            if (Game.screenData.StartsWith("start_screen"))
            {
                e.Graphics.DrawString("Tretard Edition", new Font(FontFamily.GenericSansSerif, 10f * DrawingUtils.screenSize), Brushes.Orange, // Size.Width / 24f
                    new PointF(DrawingUtils.screenCenter.x - (int)(e.Graphics.MeasureString("Tretard Edition", new Font(FontFamily.GenericSansSerif, 10f * DrawingUtils.screenSize)).Width / 2), DrawingUtils.LogoVCenter.y));
            }

            e.Graphics.DrawString("screenCenter: " + DrawingUtils.screenCenter, df, Brushes.Orange, new PointF(0, Size.Height - 6 - (6 * 14 * DrawingUtils.screenSize)));
            e.Graphics.DrawString("screenSize: " + DrawingUtils.screenSize, df, Brushes.Orange, new PointF(0, Size.Height - 6 - (5 * 14 * DrawingUtils.screenSize)));
            e.Graphics.DrawString("ClientInstance: " + Game.clientInstance.ToString("X"), df, Brushes.Orange, new PointF(0, Size.Height - 6 - (4 * 14 * DrawingUtils.screenSize)));
            e.Graphics.DrawString("Pos: " + Game.position, df, Brushes.Orange, new PointF(0, Size.Height - 6 - (3 * 14 * DrawingUtils.screenSize)));
            e.Graphics.DrawString("Players: " + Game.getPlayers().Count, df, Brushes.Orange, new PointF(0, Size.Height - 6 - (2 * 14 * DrawingUtils.screenSize)));
            e.Graphics.DrawString("Entities: " + Game.getEntites().Count, df, Brushes.Orange, new PointF(0, Size.Height - 6 - (1 * 14 * DrawingUtils.screenSize)));

            */
        }

        private void panel2_MouseDown_1(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            _mouseDownLocation = e.Location;
            panel2.BringToFront();
        }

        private void panel2_MouseMove_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                panel2.Left = e.X + panel2.Left - _mouseDownLocation.X;
                panel2.Top = e.Y + panel2.Top - _mouseDownLocation.Y;
            }
        }

        private void Overlay_Load(object sender, EventArgs e)
        {
            foreach (var mod in Program.Modules)
            {
                var moduleButton = ClonableButton.Clone();
                moduleButton.Visible = true;
                moduleButton.Name = mod.name;
                moduleButton.Text = mod.name;
                if (mod.keybind != 0x07)
                    moduleButton.Text += @" (" + (Keys)mod.keybind + @")";
                moduleButton.Click += moduleActivated;
                moduleButton.MouseDown += keybindActivated;
                moduleButton.FlatAppearance.BorderSize = 0;
                moduleButton.FlatAppearance.BorderColor = TestCategory.BackColor;
                switch (mod.category)
                {
                    case "Flies":
                        panel7.Controls.Add(moduleButton);
                        panel7.Size = new Size(0, panel7.Controls.Count * ClonableButton.Size.Height);
                        panel6.Size = new Size(panel6.Size.Width,
                            panel7.Controls.Count * ClonableButton.Size.Height + 24);
                        break;
                    case "Visual":
                        panel15.Controls.Add(moduleButton);
                        panel15.Size = new Size(0, panel15.Controls.Count * ClonableButton.Size.Height);
                        panel14.Size = new Size(panel14.Size.Width,
                            panel15.Controls.Count * ClonableButton.Size.Height + 24);
                        break;
                    case "Exploits":
                        panel13.Controls.Add(moduleButton);
                        panel13.Size = new Size(0, panel13.Controls.Count * ClonableButton.Size.Height);
                        panel12.Size = new Size(panel12.Size.Width,
                            panel13.Controls.Count * ClonableButton.Size.Height + 24);
                        break;
                    case "World":
                        panel9.Controls.Add(moduleButton);
                        panel9.Size = new Size(0, panel9.Controls.Count * ClonableButton.Size.Height);
                        panel8.Size = new Size(panel8.Size.Width,
                            panel9.Controls.Count * ClonableButton.Size.Height + 24);
                        break;
                    case "Combat":
                        panel11.Controls.Add(moduleButton);
                        panel11.Size = new Size(0, panel11.Controls.Count * ClonableButton.Size.Height);
                        panel10.Size = new Size(panel10.Size.Width,
                            panel11.Controls.Count * ClonableButton.Size.Height + 24);
                        break;
                    case "Player":
                        panel17.Controls.Add(moduleButton);
                        panel17.Size = new Size(0, panel17.Controls.Count * ClonableButton.Size.Height);
                        panel16.Size = new Size(panel16.Size.Width,
                            panel17.Controls.Count * ClonableButton.Size.Height + 24);
                        break;
                    case "Other":
                        TestCategory.Controls.Add(moduleButton);
                        TestCategory.Size = new Size(0, (TestCategory.Controls.Count - 1) * ClonableButton.Size.Height);
                        panel2.Size = new Size(panel2.Size.Width,
                            (TestCategory.Controls.Count - 1) * ClonableButton.Size.Height + 24);
                        break;
                }
            }

            foreach (var mod in Program.Modules)
                if (mod.name == "Antibot" || mod.name == "ClickGUI")
                    mod.OnEnable();
        }

        private void keybindActivated(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Middle) return;
            var btn = (Button)sender;
            if (btn == null) return;

            btn.Text = btn.Name + @" (...)";

            vMod = btn;

            btn.KeyDown += catchKeybind;
            btn.Select();
        }

        private void catchKeybind(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Delete)
            {
                vMod.KeyDown -= catchKeybind;
                vMod.Text = vMod.Name;
                foreach (var mod in Program.Modules)
                    if (mod.name == vMod.Name)
                        mod.keybind = (char)0x07;
                return;
            }

            foreach (var mod in Program.Modules)
                if (mod.name == vMod.Name)
                    mod.keybind = (char)(int)e.KeyCode;

            vMod.Text = vMod.Name + @" (" + e.KeyCode + @")";

            vMod.KeyDown -= catchKeybind;
        }

        private void moduleActivated(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            if (btn == null) return;

            foreach (var mod in Program.Modules.Where(mod => mod.name == btn.Name))
                if (mod.enabled)
                {
                    mod.OnDisable();
                    btn.BackColor = Color.FromArgb(255, 44, 44, 44);
                }
                else
                {
                    mod.OnEnable();
                    btn.BackColor = Color.FromArgb(255, 39, 39, 39);
                }
        }

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _mouseDownLocation = e.Location;
                panel3.BringToFront();
            }
        }

        private void panel3_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            panel3.Left = e.X + panel3.Left - _mouseDownLocation.X;
            panel3.Top = e.Y + panel3.Top - _mouseDownLocation.Y;
        }

        private void ClonableButton_Click(object sender, EventArgs e)
        {
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            _mouseDownLocation = e.Location;
            panel1.BringToFront();
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            panel1.Left = e.X + panel1.Left - _mouseDownLocation.X;
            panel1.Top = e.Y + panel1.Top - _mouseDownLocation.Y;
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void panel6_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            _mouseDownLocation = e.Location;
            panel6.BringToFront();
        }

        private void panel6_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            panel6.Left = e.X + panel6.Left - _mouseDownLocation.X;
            panel6.Top = e.Y + panel6.Top - _mouseDownLocation.Y;
        }

        private void panel14_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _mouseDownLocation = e.Location;
                panel14.BringToFront();
            }
        }

        private void panel14_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            panel14.Left = e.X + panel14.Left - _mouseDownLocation.X;
            panel14.Top = e.Y + panel14.Top - _mouseDownLocation.Y;
        }

        private void panel8_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            _mouseDownLocation = e.Location;
            panel8.BringToFront();
        }

        private void panel8_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            panel8.Left = e.X + panel8.Left - _mouseDownLocation.X;
            panel8.Top = e.Y + panel8.Top - _mouseDownLocation.Y;
        }

        private void panel10_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            _mouseDownLocation = e.Location;
            panel10.BringToFront();
        }

        private void panel10_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            panel10.Left = e.X + panel10.Left - _mouseDownLocation.X;
            panel10.Top = e.Y + panel10.Top - _mouseDownLocation.Y;
        }

        private void panel12_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            _mouseDownLocation = e.Location;
            panel12.BringToFront();
        }

        private void panel12_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            panel12.Left = e.X + panel12.Left - _mouseDownLocation.X;
            panel12.Top = e.Y + panel12.Top - _mouseDownLocation.Y;
        }

        private void panel16_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            _mouseDownLocation = e.Location;
            panel16.BringToFront();
        }

        private void panel16_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            panel16.Left = e.X + panel16.Left - _mouseDownLocation.X;
            panel16.Top = e.Y + panel16.Top - _mouseDownLocation.Y;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            try // fixed
            {
                foreach (var mod in Program.Modules)
                {
                    foreach (Button btn in TestCategory.Controls) updateModule(mod, btn);
                    foreach (Button btn in panel7.Controls) updateModule(mod, btn);
                    foreach (Button btn in panel17.Controls) updateModule(mod, btn);
                    foreach (Button btn in panel15.Controls) updateModule(mod, btn);
                    foreach (Button btn in panel9.Controls) updateModule(mod, btn);
                    foreach (Button btn in panel11.Controls) updateModule(mod, btn);
                    foreach (Button btn in panel13.Controls) updateModule(mod, btn);
                }
            }
            catch
            {
                // ignored
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            try // fixed
            {
                if (MCM.isMinecraftFocused() && TopMost == false)
                    TopMost = true;
                if (MCM.isMinecraftFocused() || !TopMost) return;
                if (ActiveForm == this) return;
                Opacity = 1;
                TopMost = false;
                SetWindowPos(Handle, new IntPtr(1), 0, 0, 0, 0, 2 | 1 | 10);
            }
            catch
            {
                // ignored
            }
        }

        private struct Placement
        {
            public int length;
            public int flags;
            public int showCmd;
            public Point ptMinPosition;
            public Point ptMaxPosition;
            public Rectangle rcNormalPosition;
        }
    }
}