#region

using Trero.ClientBase;
using Trero.ClientBase.KeyBase;
using Trero.Modules.vModuleExtra;

#endregion

namespace Trero.Modules
{
    internal class Zoom : Module
    {
        public Zoom() : base("Zoom", 'C', "Visual", "Zoom in when pressing C just like optifine!")
        {
            addBypass(new BypassBox(new string[] { "Minus 0.8", "Minus 1" }));
            Keymap.keyEvent += KeyvE;
        } // Not defined

        private void KeyvE(object sender, KeyEvent e)
        {
            if (e.vkey != VKeyCodes.KeyUp) return;
            if ((char)(int)e.key == keybind) OnDisable(); // Disable module when keybind has been let go
        }

        public override void OnEnable()
        {
            if (bypasses[0].curIndex == 0)
                Game.setFieldOfView(0.2f);
            if (bypasses[0].curIndex == 1)
                Game.setFieldOfView(0f);

            base.OnEnable();
        }

        public override void OnDisable()
        {
            Game.resetFieldOfView();

            base.OnDisable();
        }
    }
}