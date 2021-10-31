#region

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Trero.ClientBase.KeyBase;
using Trero.ClientBase.UIBase;
using Trero.Modules.vModuleExtra;

#endregion

namespace Trero.ClientBase.UIBase.Notifications
{
    internal class Notification
    {
        public readonly string content;
        public readonly string title;
        public readonly string notificationType;
        public int timer;
        public bool ticking;
        private bool enabled;



        public Notification(string title, string content, string notificationType = "ENABLED")
        {
            this.title = title;
            this.content = content;
            this.notificationType = notificationType;
            this.ticking = true;
            this.OnEnable();
            Program.notifications.Add(this);
            Overlay.handle.Paint += renderNotification;
        }

        public virtual void OnEnable()
        {
            enabled = true;
        }

        private void renderNotification(object sender, PaintEventArgs e)
        {
            if (!enabled) return;
            int loop = 0;
            Font df = new Font("Arial", 24, FontStyle.Regular);
            Font df1 = new Font("Arial", 14, FontStyle.Regular);

            Brush brush1 = new SolidBrush(Color.FromArgb(44, 44, 44));
            Brush brush2 = new SolidBrush(Color.FromArgb(33, 33, 33));

            Brush white = new SolidBrush(Color.FromArgb(255, 255, 255));
            Brush red = new SolidBrush(Color.FromArgb(224, 13, 31));
            Brush green = new SolidBrush(Color.FromArgb(49, 209, 0));

            Brush stringColour = new SolidBrush(Color.FromArgb(200, 200, 200));
            e.Graphics.FillRectangle(brush2, Overlay.handle.Width - 250, Overlay.handle.Height - 100, Overlay.handle.Height - 100, 100);
            e.Graphics.DrawString("Module Toggled", df, white, Overlay.handle.Width - 250, Overlay.handle.Height - 100);
            if (this.notificationType == "DISABLED") {
                e.Graphics.DrawString(this.title, df1, white, Overlay.handle.Width - 170, Overlay.handle.Height - 60);
                e.Graphics.DrawString("Disabled  ", df1, red, Overlay.handle.Width - 250, Overlay.handle.Height - 60);
            }
            else if(this.notificationType == "ENABLED")
            {
                e.Graphics.DrawString(this.title, df1, white, Overlay.handle.Width - 170, Overlay.handle.Height - 60);
                e.Graphics.DrawString("Enabled  ", df1, green, Overlay.handle.Width - 250, Overlay.handle.Height - 60);
            }
        }

        public virtual void OnDisable()
        {
            enabled = false;
            this.ticking = false;
            Program.notifications.Remove(this);
        }

        public virtual void OnTick()
        {

            timer++;
            if (timer > 0 && timer < 51)
            {
                timer = 0;
                Overlay.handle.Paint -= renderNotification;
                this.OnDisable();
            }
        }
    }
}
