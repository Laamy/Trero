using System.Diagnostics;
using System.Windows.Forms;
using Trero.ClientBase.UIBase;
using Microsoft.VisualBasic;
using Trero.ClientBase;
using System.Threading;

namespace Trero.Modules
{
    class Teleport : Module
    {
        public Teleport() : base("Teleport", (char)0x07, "World") { } // Not defined
        public override void onEnable()
        {
            new Thread(() => Game.teleport(Base.Vec3(Interaction.InputBox("Please enter your new position", "Trero (Teleport)")))).Start();
        }
    }
}
