#region

using Trero.ClientBase;
using Trero.Modules.vModuleExtra;

#endregion

namespace Trero.Modules
{
    internal class Jump : Module // for those who dont know this is a reference to a famous movie ;p
    {
        public Jump() : base("Jump", (char)0x07, "Movement", "Broken jumper movie reference")
        {
            addBypass(new BypassBox(new string[] { "JumpDistance: 1", "JumpDistance: 2", "JumpDistance: 3", "JumpDistance: 4", "JumpDistance: 5", "JumpDistance: 6", "JumpDistance: 7" }));
        }

        public override void OnEnable()
        {
            base.OnEnable();

            Game.jumpForwards(bypasses[0].curIndex + 1);

            base.OnDisable();
        }
    }
}