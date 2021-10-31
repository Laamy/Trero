#region

using System.Linq;
using System.Threading;
using Microsoft.VisualBasic;
using Trero.ClientBase;

#endregion

namespace Trero.Modules
{
    internal class PlayerTP : Module
    {
        public PlayerTP() : base("PlayerTP", (char)0x07, "Exploits", "Teleport to a player of your choice")
        {
        } // Not defined

        public override void OnEnable()
        {
            new Thread(() =>
            {
                var username = Interaction.InputBox("Please enter player username", "Trero (PlayerTP)").ToLower();

                foreach (var entity in Game.getPlayers().Where(entity => entity.username.ToLower().Contains(username)))
                {
                    Game.position = entity.position;
                    break;
                }
            }).Start();
        }
    }
}