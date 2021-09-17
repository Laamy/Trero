#region

using Trero.ClientBase;
using Trero.ClientBase.KeyBase;

#endregion

namespace Trero.Modules
{
    internal class Zoom : Module
    {
        public Zoom() : base("Zoom", 'C', "Visual")
        {
            Keymap.keyEvent += KeyvE;
        } // Not defined

        private void KeyvE(object sender, KeyEvent e)
        {
            if (e.vkey != VKeyCodes.KeyUp) return;
            if ((char)(int)e.key == keybind) OnDisable(); // Disable module when keybind has been let go
        }

        public override void OnEnable()
        {
            Game.setFieldOfView(0.2f);

            base.OnEnable();
        }

        public override void OnDisable()
        {
            Game.resetFieldOfView();

            base.OnDisable();
        }
    }
}