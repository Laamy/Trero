#region

using Trero.ClientBase;

#endregion

namespace Trero.Modules
{
    internal class NoLagBack : Module
    {
        public NoLagBack() : base("NoLagBack", (char)0x07, "Exploits", "half done")
        {
        } // Not defined

        public override void OnEnable()
        {
            base.OnEnable();

            OverrideBase.ServerCanTeleportClient = false;
        }

        public override void OnDisable()
        {
            base.OnDisable();

            OverrideBase.ServerCanTeleportClient = true;
        }
    }
}