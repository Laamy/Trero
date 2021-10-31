#region

using Trero.ClientBase;

#endregion

namespace Trero.Modules
{
    internal class Antibot : Module
    {
        public Antibot() : base("Antibot", (char)0x07, "Other", "Handles entity list filtering", true)
        {
        }

        public override void OnEnable()
        {
            Game.CustomDefines.antibot = true;
            Game.CustomDefines.antibotStates = new bool[] { true, false }; // default parser
            base.OnEnable();
        }

        public override void OnDisable()
        {
            Game.CustomDefines.antibot = false;
            base.OnDisable();
        }
    }
}