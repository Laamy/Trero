#region

using System;
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

            // B8 00 00 00 00 90
            // B8 CD CC CC 3D 90
            // B8 (4 Byte vl) 90
            MCM.writeBaseBytes(OverrideBase.entityModel.playerModelAddr + (6 * 16), MCM.ceByte2Bytes("B8 CD CC CC 3D 90"));
            // If you rewrite a function you lose the ability to revert via OverrideBase
        }

        public override void OnDisable()
        {
            base.OnDisable();

            MCM.writeBaseBytes(OverrideBase.entityModel.playerModelAddr + (6 * 16), MCM.ceByte2Bytes("8B 81 30 01 00 00"));
            // OverrideBase.entityModel.allowModelScalingX = true;
            Console.WriteLine("c");
        }
    }
}