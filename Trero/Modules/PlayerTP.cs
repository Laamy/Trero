using System.Diagnostics;
using System.Windows.Forms;
using Trero.ClientBase.UIBase;
using Microsoft.VisualBasic;
using Trero.ClientBase;
using System.Threading;
using Trero.ClientBase.EntityBase;

namespace Trero.Modules
{
    class PlayerTP : Module
    {
        public PlayerTP() : base("PlayerTP", (char)0x07, "Exploits") { } // Not defined
        public override void onEnable()
        {
            new Thread(() => {
                string username = Interaction.InputBox("Please enter player username", "Trero (PlayerTP)").ToLower();

                foreach (Actor entity in Game.getPlayers())
                {
                    if (entity.username.ToLower().Contains(username))
                    {
                        Game.position = entity.position;
                        break;
                    }
                }

            }).Start();
        }
    }
}
