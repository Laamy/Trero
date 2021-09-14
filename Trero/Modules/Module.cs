#region

using System.Collections.Generic;
using Trero.ClientBase.KeyBase;
using Trero.ClientBase.UIBase;
using Trero.Modules.vModuleExtra;

#endregion

namespace Trero.Modules
{
    internal class Module
    {
        public readonly string category;
        public readonly string name;
        public bool enabled;

        public char keybind;
        public List<vCheckBox> moduleCheckboxSettings = new List<vCheckBox>();

        public List<FloatSlider> moduleFloatSettings = new List<FloatSlider>();

        protected Module(string name, char keybind, string category = "Other", bool enabled = false)
        {
            this.name = name;
            this.keybind = keybind;
            this.category = category;
            this.enabled = enabled;

            Keymap.keyEvent += OnKeypress;
        }

        public void AddSetting()
        {
        }

        private void OnKeypress(object sender, KeyEvent e) // Cant be overridden 
        {
            if (Overlay.handle == null) return;

            if (e.vkey != VKeyCodes.KeyDown || (int)e.key != keybind) return;
            enabled = !enabled;
            //Slight performance improvement over two if statements
            switch (enabled)
            {
                case true:
                    OnEnable();
                    break;
                case false:
                    OnDisable();
                    break;
            }
        } // Enable/Disable Handler

        public virtual void OnEnable()
        {
            enabled = true;
        }

        public virtual void OnDisable()
        {
            enabled = false;
        }

        public virtual void OnTick()
        {
        }
    }
}