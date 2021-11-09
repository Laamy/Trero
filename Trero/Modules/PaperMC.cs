#region

using Trero.ClientBase;

#endregion

namespace Trero.Modules
{
    internal class PaperMC : Module
    {
        public PaperMC() : base("PaperMC", (char)0x07, "Exploits", "Turn everyone into... Paper..?")
        {
        } // Not defined

        public override void OnEnable()
        {
            base.OnEnable();

            OverrideBase.entityModel.allowModelScalingX = false;

            OverrideBase.entityModel.
        }

        public override void OnDisable()
        {
            base.OnDisable();

            OverrideBase.entityModel.allowModelScalingX = true;
        }
    }
}