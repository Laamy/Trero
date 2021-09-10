using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trero.ClientBase.KeyBase;

namespace Trero.Modules
{
    class Module
    {
        public bool enabled = false;
        public string name;
        public string category;
        public char keybind;
        public Module(string name, char keybind, string category = "TestCategory")
        {
            this.name = name;
            this.keybind = keybind;
            this.category = category;

            Keymap.keyEvent += onKeypress;
        }

        private void onKeypress(object sender, KeyEvent e) // Cant be overridden 
        {
            if (e.vkey == vKeyCodes.KeyDown && (int)e.key == keybind)
            {
                enabled = !enabled;
                if (enabled) onEnable();
                if (!enabled) onDisable();
            }
        } // Enable/Disable Handler

        public virtual void onEnable() => enabled = true;
        public virtual void onDisable() => enabled = false;

        public virtual void onTick() { }
        public virtual void onLoop()
        {
            if (enabled)
                onTick();
        }
    }
}
