#region

using System.Threading;
using Microsoft.VisualBasic;
using Trero.ClientBase;

#endregion

namespace Trero.Modules
{
    internal class Teleport : Module
    {
        public Teleport() : base("Teleport", (char)0x07, "World")
        {
        } // Not defined

        public override void OnEnable()
        {
            new Thread(() =>
                    Game.teleport(
                        Base.Vec3(Interaction.InputBox("Please enter your new position", "Trero (Teleport)"))))
                .Start();
        }
    }
}