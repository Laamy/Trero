#region

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Trero.ClientBase.KeyBase;
using Trero.ClientBase.UIBase.TreroUILibrary;
using Trero.ClientBase.VersionBase;
using Trero.Modules;
using Trero.Modules.vModuleExtra;

#endregion

namespace Trero.ClientBase.UIBase
{
    public partial class Overlay : Form
    {
        [DllImport("User32.dll")]
        public static extern int GetWindowLong(IntPtr hwnd, int nIndex);

        [DllImport("User32.dll")]
        public static extern int SetWindowLong(IntPtr hwnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        public static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc,
            WinEventDelegate lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);

        [DllImport("user32.dll")]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, IntPtr voidProcessId);

        public delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);

        public static Overlay handle;

        private Font _df = new Font(FontFamily.GenericSansSerif, 12f);

        private Point _mouseDownLocation;

        public Button vMod;
        public Label cMod;

        public Overlay()
        {
            handle = this;

            Console.WriteLine("Initializing components...");
            InitializeComponent();
            Console.WriteLine("Initialized components!");

            Focus();

            int initialStyle = GetWindowLong(this.Handle, -20);
            SetWindowLong(this.Handle, -20, initialStyle | 0x80000 | 0x20);

            overDel = new WinEventDelegate(adjust);

            Console.WriteLine("Initializing hooks...");
            SetWinEventHook((uint)SWEH_Events.EVENT_OBJECT_LOCATIONCHANGE, (uint)SWEH_Events.EVENT_OBJECT_LOCATIONCHANGE, IntPtr.Zero, overDel, MCM.mcWinProcId, GetWindowThreadProcessId(MCM.mcWinHandle, IntPtr.Zero), (uint)SWEH_dwFlags.WINEVENT_OUTOFCONTEXT | (uint)SWEH_dwFlags.WINEVENT_SKIPOWNPROCESS | (uint)SWEH_dwFlags.WINEVENT_SKIPOWNTHREAD);
            SetWinEventHook((uint)SWEH_Events.EVENT_SYSTEM_FOREGROUND, (uint)SWEH_Events.EVENT_SYSTEM_FOREGROUND, IntPtr.Zero, overDel, 0, 0, (uint)SWEH_dwFlags.WINEVENT_OUTOFCONTEXT | (uint)SWEH_dwFlags.WINEVENT_SKIPOWNPROCESS | (uint)SWEH_dwFlags.WINEVENT_SKIPOWNTHREAD);
            Console.WriteLine("Initialized window hooks!");
            Program.moduleToggled += redraw;
            Console.WriteLine("Initialized arraylist hooks!");

            TopMost = true;
        }

        private void redraw(object sender, EventArgs e) => Invalidate();

        private void adjust(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
        {
            var rect = MCM.getMinecraftRect();

            var cvE = new Placement();
            GetWindowPlacement(MCM.mcWinHandle,
                ref cvE); // Change window size if fullscreen to match extra offsets
            var vE = 0;
            var vA = 0;
            if (cvE.showCmd == 3) // Perfect window offsets
            {
                vE = 8;
                vA = 2;
            }

            int x = rect.Left + 9 + vA;
            int y = rect.Top + 35 + vE;
            int width = rect.Right - rect.Left - 18 - vA;
            int height = rect.Bottom - rect.Top - 44 - vE;
            SetWindowPos(Handle, MCM.isMinecraftFocusedInsert(), x, y, width, height, 0x0040);

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
            Invalidate();
        }

        WinEventDelegate overDel;
        private Font df = new Font("Arial", 24, FontStyle.Bold);

        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy,
            uint uFlags);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowPlacement(IntPtr hWnd, ref Placement lpwndpl);

        private void timer1_Tick(object sender, EventArgs e)
        {
            /*var list = "";

            try
            {
                var vList = Game.getEntites(); // getPlayers
                list = "Players : " + vList.Count + "\r\n";
                list = vList.Aggregate(list,
                    (current, plr) =>
                        current + (int)Game.position.Distance(plr.position) + "b " + plr.username + "\r\n");
            }
            catch
            {
            }

            var calclist = TextRenderer.MeasureText(list, playerList.Font);

            var size1 = new Size(calclist.Width + 20, calclist.Height);
            var size2 = new Size(calclist.Width + 20, calclist.Height + 24);

            if (panel5.Size != size1)
            {
                panel1.Size = size2;
                panel5.Size = size1;
            }

            playerList.Text = list;*/
        }

        private void updateModule(Module mod, Panel c) // fixed a second time ;p
        {
            foreach (var obj in c.Controls)
            {
                if (obj.GetType() == typeof(Button))
                {
                    Button btn = (Button)obj;
                    if (mod.name != btn.Name) return;
                    switch (mod.enabled)
                    {
                        case true when btn.BackColor != Color.FromArgb(255, 0, 103, 255):
                            btn.BackColor = Color.FromArgb(255, 0, 103, 255);
                            break;
                        case false when btn.BackColor == Color.FromArgb(255, 0, 103, 255):
                            btn.BackColor = Color.FromArgb(255, 54, 71, 96);
                            break;
                    }
                }
            }
        }

        private void Overlay_Paint(object sender, PaintEventArgs e)
        {
            //e.Graphics.DrawString("Trero Template", df, Brushes.Orange, new PointF(0, 0));
        }

        private void panel2_MouseDown_1(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            _mouseDownLocation = e.Location;
            panel2.BringToFront();
        }

        private void panel2_MouseMove_1(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:

                    panel2.Left = e.X + panel2.Left - _mouseDownLocation.X;
                    panel2.Top = e.Y + panel2.Top - _mouseDownLocation.Y;
                    break;
            }
        }

        private void Overlay_Load(object sender, EventArgs e)
        {
            foreach (var mod in Program.Modules)
            {
                var moduleButton = ClonablePanel.Clone();
                Button btn = ClonableButton.Clone();

                Label cLab = label13.Clone();

                List<Label> vModules = new List<Label>();

                for (int vI = 0; vI < mod.bypasses.Count; ++vI)
                {
                    Label tempTab = label13.Clone();

                    tempTab.Text = mod.bypasses[vI].list[mod.bypasses[vI].curIndex];
                    tempTab.Visible = true;
                    tempTab.Dock = DockStyle.Top;
                    tempTab.Name = mod.name + ";";

                    var tag = new KeyTags();
                    tag.Add("bypass", mod.bypasses[vI]);
                    tag.Add("button", tempTab);
                    tag.Add("index", vI);

                    tempTab.Tag = tag;
                    tempTab.MouseClick += actorPress;

                    vModules.Add(tempTab);
                }

                moduleButton.Visible = true;
                btn.Visible = true;
                btn.Name = mod.name;
                btn.Text = mod.name;
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatAppearance.BorderColor = TestCategory.BackColor;
                btn.MouseDown += keybindActivated;

                var tagc = new KeyTags();
                tagc.Add("desc", mod.desc);

                btn.Tag = tagc;

                btn.MouseEnter += ActivateTooltip;
                btn.MouseLeave += DeactivateTooltip;

                cLab.Text = "Keybind: None";
                if (mod.keybind != 0x07)
                    cLab.Text = "Keybind: " + (Keys)mod.keybind;
                cLab.Visible = true;
                cLab.Dock = DockStyle.Top;
                cLab.Name = mod.name + ";"; // so their backcolors aren't updated by timer
                cLab.MouseClick += actorBind;

                foreach (var vMod in vModules)
                    moduleButton.Controls.Add(vMod);

                moduleButton.Controls.Add(cLab);

                moduleButton.Controls.Add(btn);

                switch (mod.category)
                {
                    case "Flies":
                        panel7.Controls.Add(moduleButton);
                        break;
                    case "Visual":
                        panel15.Controls.Add(moduleButton);
                        break;
                    case "Exploits":
                        panel13.Controls.Add(moduleButton);
                        break;
                    case "World":
                        panel9.Controls.Add(moduleButton);
                        break;
                    case "Combat":
                        panel11.Controls.Add(moduleButton);
                        break;
                    case "Player":
                        panel17.Controls.Add(moduleButton);
                        break;
                    case "Other":
                        TestCategory.Controls.Add(moduleButton);
                        break;
                }
            }

            InvalidateCategories();

            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();
        }

        private void actorPress(object sender, MouseEventArgs e) // did this while upset please ignore
        {
            var lab = sender as Label;
            KeyTags boxy = lab.Tag as KeyTags;

            BypassBox bypassPressed = (BypassBox)boxy.Get("bypass");

            if (bypassPressed.curIndex < (bypassPressed.list.Count() - 1))
                bypassPressed.curIndex++;
            else if (bypassPressed.curIndex >= (bypassPressed.list.Count() - 1))
                bypassPressed.curIndex = 0;
            lab.Text = bypassPressed.list[bypassPressed.curIndex];

            Program.moduleToggled.Invoke(null, new EventArgs());
        }

        private void actorBind(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:

                    var btn = (Label)sender;
                    if (btn == null) return;

                    btn.Text = @"Keybind: ...";

                    cMod = btn;

                    btn.KeyDown += vCatchKeybind;
                    btn.Select();

                    break;
            }

            Program.moduleToggled.Invoke(null, new EventArgs());
        }

        void InvalidateCategories() // update category sizes depending on moduleList size
        {
            SuspendLayout();

            cValidate(panel7, panel6);
            cValidate(panel15, panel14);
            cValidate(panel13, panel12);
            cValidate(panel9, panel8);
            cValidate(panel11, panel10);
            cValidate(panel17, panel16);
            cValidate(TestCategory, panel2);
            cValidate(panel22, panel21);

            ResumeLayout();
        }

        void cValidate(Panel miniPanel, Panel titlePanel)
        {
            int categoryHeight = 0;
            foreach (Control c in miniPanel.Controls)
                if (c.Visible)
                    categoryHeight += c.Height;
            if (miniPanel.Height != categoryHeight)
            {
                miniPanel.Size = new Size(0, categoryHeight);
                titlePanel.Size = new Size(titlePanel.Size.Width, categoryHeight + 24);
            }
        }

        private void keybindActivated(object sender, MouseEventArgs e) // remove atani like keybind system
        {
            /*if (e.Button == MouseButtons.Middle)
            {
                var btn = (Button)sender;
                if (btn == null) return;

                btn.Text = btn.Name + @" (...)";

                vMod = btn;

                btn.KeyDown += catchKeybind;
                btn.Select();
            }*/

            if (e.Button == MouseButtons.Left)
            {
                var btn = (Button)sender;
                if (btn == null) return;

                foreach (var mod in Program.Modules.Where(mod => mod.name == btn.Name))
                    if (mod.enabled)
                    {
                        mod.OnDisable();
                        btn.BackColor = Color.FromArgb(255, 0, 103, 255);
                    }
                    else
                    {
                        mod.OnEnable();
                        btn.BackColor = Color.FromArgb(255, 54, 71, 96);
                    }
            }

            if (e.Button == MouseButtons.Right) // i want to change the category size depending on its contentSize so
            {
                var btn = (Button)sender;
                if (btn == null) return;

                if (btn.Parent.Height > 30)
                    btn.Parent.Size = ClonableButton.Size;
                else
                {
                    //btn.Parent.Size = new Size(1, 48);
                    int categoryHeight = 0;
                    foreach (Control c in btn.Parent.Controls)
                        if (c.Visible)
                            categoryHeight += c.Height;
                    btn.Parent.Size = new Size(0, categoryHeight);
                }
                InvalidateCategories();
            }

            Program.moduleToggled.Invoke(null, new EventArgs());
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

            Program.moduleToggled.Invoke(null, new EventArgs());
        }

        private void vCatchKeybind(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Delete)
            {
                cMod.KeyDown -= vCatchKeybind;
                cMod.Text = "Keybind: None";
                foreach (var mod in Program.Modules)
                    if (mod.name + ";" == cMod.Name)
                        mod.keybind = (char)0x07;
                return;
            }

            foreach (var mod in Program.Modules)
                if (mod.name + ";" == cMod.Name)
                    mod.keybind = (char)(int)e.KeyCode;

            cMod.Text = @"Keybind: " + e.KeyCode;

            cMod.KeyDown -= vCatchKeybind;

            Program.moduleToggled.Invoke(null, new EventArgs());
        }

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    _mouseDownLocation = e.Location;
                    panel3.BringToFront();
                    break;
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
            switch (e.Button)
            {
                case MouseButtons.Left:
                    _mouseDownLocation = e.Location;
                    panel1.BringToFront();
                    break;
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            panel1.Left = e.X + panel1.Left - _mouseDownLocation.X;
            panel1.Top = e.Y + panel1.Top - _mouseDownLocation.Y;
        }

        private void panel6_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    _mouseDownLocation = e.Location;
                    panel6.BringToFront();
                    break;
            }
        }

        private void panel6_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            panel6.Left = e.X + panel6.Left - _mouseDownLocation.X;
            panel6.Top = e.Y + panel6.Top - _mouseDownLocation.Y;
        }

        private void panel14_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    _mouseDownLocation = e.Location;
                    panel14.BringToFront();
                    break;
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
            switch (e.Button)
            {
                case MouseButtons.Left:
                    _mouseDownLocation = e.Location;
                    panel8.BringToFront();
                    break;
            }
        }

        private void panel8_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            panel8.Left = e.X + panel8.Left - _mouseDownLocation.X;
            panel8.Top = e.Y + panel8.Top - _mouseDownLocation.Y;
        }

        private void panel10_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    _mouseDownLocation = e.Location;
                    panel10.BringToFront();
                    break;
            }
        }

        private void panel10_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            panel10.Left = e.X + panel10.Left - _mouseDownLocation.X;
            panel10.Top = e.Y + panel10.Top - _mouseDownLocation.Y;
        }

        private void panel12_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    _mouseDownLocation = e.Location;
                    panel12.BringToFront();
                    break;
            }
        }

        private void panel12_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            panel12.Left = e.X + panel12.Left - _mouseDownLocation.X;
            panel12.Top = e.Y + panel12.Top - _mouseDownLocation.Y;
        }

        private void panel16_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    _mouseDownLocation = e.Location;
                    panel16.BringToFront();
                    break;
            }
        }

        private void panel16_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            panel16.Left = e.X + panel16.Left - _mouseDownLocation.X;
            panel16.Top = e.Y + panel16.Top - _mouseDownLocation.Y;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            try // OH I KNOW WHATS WRONG LMAO THIS BROKE BECAUSE IM USING PANELS INSTEAD OF BUTTONS!
            {
                foreach (var mod in Program.Modules)
                {
                    foreach (var btn in TestCategory.Controls) updateModule(mod, (Panel)btn);
                    foreach (var btn in panel7.Controls) updateModule(mod, (Panel)btn);
                    foreach (var btn in panel17.Controls) updateModule(mod, (Panel)btn);
                    foreach (var btn in panel15.Controls) updateModule(mod, (Panel)btn);
                    foreach (var btn in panel9.Controls) updateModule(mod, (Panel)btn);
                    foreach (var btn in panel11.Controls) updateModule(mod, (Panel)btn);
                    foreach (var btn in panel13.Controls) updateModule(mod, (Panel)btn);
                }
            }
            catch
            {
                // ignored
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (!Game.isNull)
                UpdateLabel.Text = Game.username + "   |   " + VersionClass.currentVersion.name + "   |   " + Game.position.x + ", " + Game.position.y + ", " + Game.position.z;
            else UpdateLabel.Text = "Not InGame";

        }

        private void label12_Click_1(object sender, EventArgs e)
        {

        }

        private void Overlay_ResizeBegin(object sender, EventArgs e) => SuspendLayout();
        private void Overlay_ResizeEnd(object sender, EventArgs e) => ResumeLayout();

        private void button2_Click(object sender, EventArgs e)
        {
            panel18.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Game.teleport((int)numericUpDown1.Value, (int)numericUpDown2.Value, (int)numericUpDown3.Value);
        }

        private void panel18_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    _mouseDownLocation = e.Location;
                    panel18.BringToFront();
                    break;
            }
        }

        private void panel18_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            panel18.Left = e.X + panel18.Left - _mouseDownLocation.X;
            panel18.Top = e.Y + panel18.Top - _mouseDownLocation.Y;
        }

        private void label4_MouseDown(object sender, MouseEventArgs e) => panel8_MouseDown(sender, e); // worked
        private void label4_MouseMove(object sender, MouseEventArgs e) => panel8_MouseMove(sender, e);

        private void label10_MouseDown(object sender, MouseEventArgs e) => panel16_MouseDown(sender, e);
        private void label10_MouseMove(object sender, MouseEventArgs e) => panel16_MouseMove(sender, e);

        private void label8_MouseDown(object sender, MouseEventArgs e) => panel12_MouseDown(sender, e);
        private void label8_MouseMove(object sender, MouseEventArgs e) => panel12_MouseMove(sender, e);

        private void label6_MouseDown(object sender, MouseEventArgs e) => panel2_MouseDown_1(sender, e);
        private void label6_MouseMove(object sender, MouseEventArgs e) => panel2_MouseMove_1(sender, e);

        private void label5_MouseDown(object sender, MouseEventArgs e) => panel6_MouseDown(sender, e);
        private void label5_MouseMove(object sender, MouseEventArgs e) => panel6_MouseMove(sender, e);

        private void label9_MouseDown(object sender, MouseEventArgs e) => panel14_MouseDown(sender, e);
        private void label9_MouseMove(object sender, MouseEventArgs e) => panel14_MouseMove(sender, e);

        private void label7_MouseDown(object sender, MouseEventArgs e) => panel10_MouseDown(sender, e);
        private void label7_MouseMove(object sender, MouseEventArgs e) => panel10_MouseMove(sender, e);

        private void button3_Click(object sender, EventArgs e)
        {
            Faketernal.Potions.CreateAction((i, c) => { // give slowness effect

                if (Game.onGround == true) // Oof...
                {
                    var plrYaw = Game.bodyRots.y; // yaw

                    if (Keymap.GetAsyncKeyState(Keys.W))
                    {
                        if (!Keymap.GetAsyncKeyState(Keys.A) && !Keymap.GetAsyncKeyState(Keys.D))
                            plrYaw += 90f;
                        if (Keymap.GetAsyncKeyState(Keys.A))
                            plrYaw += 45f;
                        else if (Keymap.GetAsyncKeyState(Keys.D))
                            plrYaw += 135f;
                    }
                    else if (Keymap.GetAsyncKeyState(Keys.S))
                    {
                        if (!Keymap.GetAsyncKeyState(Keys.A) && !Keymap.GetAsyncKeyState(Keys.D))
                            plrYaw -= 90f;
                        if (Keymap.GetAsyncKeyState(Keys.A))
                            plrYaw -= 45f;
                        else if (Keymap.GetAsyncKeyState(Keys.D))
                            plrYaw -= 135f;
                    }
                    else if (!Keymap.GetAsyncKeyState(Keys.W) && !Keymap.GetAsyncKeyState(Keys.S))
                    {
                        if (!Keymap.GetAsyncKeyState(Keys.A) && Keymap.GetAsyncKeyState(Keys.D))
                            plrYaw += 180f;
                    }

                    if (!(Keymap.GetAsyncKeyState(Keys.W) | Keymap.GetAsyncKeyState(Keys.A) | Keymap.GetAsyncKeyState(Keys.S) |
                          Keymap.GetAsyncKeyState(Keys.D))) return;
                    var calYaw = plrYaw * ((float)Math.PI / 180f);

                    MCM.writeFloat(Game.localPlayer + VersionClass.GetData("velocity"), (float)Math.Cos(calYaw) * (0.05f / c));
                    MCM.writeFloat(Game.localPlayer + VersionClass.GetData("velocity") + 8, (float)Math.Sin(calYaw) * (0.05f / c));
                }

            }, new iRGB(129, 108, 90), (int)PotionDiritation.Value, (int)PotionAmplifier.Value, 0.85f);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Faketernal.Potions.ClearActions();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Faketernal.Potions.CreateAction((i, c) => { // give slowfalling effect

                float speed = -(0.10f / c);

                if (Game.velocity.y < speed)
                {
                    MCM.writeFloat(Game.localPlayer + VersionClass.GetData("velocity") + 4, speed);
                }

            }, new iRGB(209, 239, 255), (int)PotionDiritation.Value, (int)PotionAmplifier.Value);
        }

        private void panel21_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    _mouseDownLocation = e.Location;
                    panel21.BringToFront();
                    break;
            }
        }

        private void panel21_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            panel21.Left = e.X + panel21.Left - _mouseDownLocation.X;
            panel21.Top = e.Y + panel21.Top - _mouseDownLocation.Y;
        }

        private void label11_MouseDown(object sender, MouseEventArgs e) => panel21_MouseDown(sender, e);
        private void label11_MouseMove(object sender, MouseEventArgs e) => panel21_MouseMove(sender, e);

        private void button6_Click(object sender, EventArgs e)
        {
            Faketernal.Potions.CreateAction((i, c) => { // give speed effect

                Game.speed = (c * 0.0200000009f) + 0.1f; // Thanks javajar for this equation ;p

            }, new iRGB(198, 175, 124), (int)PotionDiritation.Value, (int)PotionAmplifier.Value, 2f, () => { Game.speed = 0.1000000015f; });
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Faketernal.Potions.CreateAction((i, c) => { // give jumpboost effect

                if (Game.onGround == true) // Oof...
                {
                    if (Keymap.GetAsyncKeyState(Keys.Space))
                    {
                        MCM.writeFloat(Game.localPlayer + VersionClass.GetData("velocity") + 4, 0.5f * (0.50f * c ));
                    }
                }

            }, new iRGB(76, 255, 34), (int)PotionDiritation.Value, (int)PotionAmplifier.Value);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Faketernal.Potions.CreateAction((i, c) => { // give levitation effect

                float speed = (0.10f * c);

                MCM.writeFloat(Game.localPlayer + VersionClass.GetData("velocity") + 4, speed);

            }, new iRGB(255, 255, 206), (int)PotionDiritation.Value, (int)PotionAmplifier.Value);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Faketernal.Potions.CreateAction((i, c) => { // give custom effect

                float amp = (0.5f * (c + 1));

                Game.stepHeight = amp;

            }, new iRGB(220, 178, 238), (int)PotionDiritation.Value, (int)PotionAmplifier.Value, 1f, () => { Game.stepHeight = 0.5f; });
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Faketernal.Potions.CreateAction((i, c) => { // give custom effect

                if (Game.onGround == true)
                {
                    if (Keymap.GetAsyncKeyState(Keys.Space))
                    {
                        MCM.writeFloat(Game.localPlayer + VersionClass.GetData("velocity") + 4, 0.25f / c);
                    }
                }

            }, new iRGB(118, 141, 124), (int)PotionDiritation.Value, (int)PotionAmplifier.Value);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var pos = Game.position;

            float skimmedBodyParts = .5f / (int)PotionAmplifier.Value;

            //if (skimmedBodyParts < 0)
                //skimmedBodyParts = 0.05f;

            Game.teleport(new AABB(pos, new Vector3(pos.x + skimmedBodyParts, pos.y + 1.8f, pos.z + skimmedBodyParts)));

            Faketernal.Potions.CreateAction((i, c) => {}, new iRGB(169, 202, 207), (int)PotionDiritation.Value, 0, 1f, () => { Game.teleport(Game.position); });
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Faketernal.Potions.CreateAction((i, c) => { // give custom effect

                Game.stepHeight = (0.5f * (c + 1));
                Game.onGround = true;
                if (Game.isInWater)
                    MCM.writeFloat(Game.localPlayer + VersionClass.GetData("velocity") + 4, 0.01f * c);

            }, new iRGB(128, 76, 146), (int)PotionDiritation.Value, (int)PotionAmplifier.Value, 1f, () => { Game.stepHeight = 0.5f; });
        }

        private void panel21_MouseClick(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                    foreach (Control c in panel22.Controls)
                        c.Visible = !c.Visible;
                    InvalidateCategories();
                    break;
            }
        }
        private void label11_MouseClick(object sender, MouseEventArgs e) => panel21_MouseClick(sender, e);

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void panel23_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel21_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void ActivateTooltip(object sender, EventArgs e) => label16.Text = ((KeyTags)((Button)sender).Tag).Get("desc").ToString();

        private void DeactivateTooltip(object sender, EventArgs e) => label16.Text = "";

        private void timer4_Tick(object sender, EventArgs e)
        {

            /*try
            {
                var ent = Game.getClosestPlayer();

                if (ent == null)
                {
                    label2.Text = "None";
                    label2.Text = "";
                    label3.Text = "";
                }
                else
                {
                    var vec = Base.Vec3((int)ent.position.x, (int)ent.position.y, (int)ent.position.z);

                    label1.Text = ent.username;
                    label2.Text = vec.ToString();
                    label3.Text = Game.position.Distance(vec) + "b";
                }
            }
            catch
            {
            }*/
        }
    }
}

/*

Slowness - 129,108,90
SlowFalling - 209,239,255
Speed - 198,175,124
NightVision - 161,31,31
JumpBoost - 76,255,34
Levitation - 255,255,206

TurtleMaster - 98,91,117
Wither - 39,42,53
Weakness - 72,77,72
Posion - 49,147,78
Strength - 35,36,147
WaterBreathing - 153,82,46
Regeneration - 171,92,205
Invisibility - 146,131,127
FireResistance - 58,154,228

*/
