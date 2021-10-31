#region

using Trero.ClientBase;

#endregion

namespace Trero.Modules
{
    internal class FlickerExample : Module
    {
        private int _flicker;

        public FlickerExample() : base("FlickerExample", (char)0x07, "Other", "ExampleModule")
        {
        } // 0x07 = no keybind

        public override void OnTick()
        {
            if (Game.isNull) return;
            _flicker++;

            if (_flicker != 300) return;
            _flicker = 0;

        }
    }
}