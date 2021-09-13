using System.Diagnostics;
using System.Windows.Forms;
using Trero.ClientBase.UIBase;
using Microsoft.VisualBasic;
using Trero.ClientBase;
using System.Threading;
using Trero.ClientBase.EntityBase;
using System;

namespace Trero.Modules
{
    class Friends : Module
    {
        public Friends() : base("Friends", (char)0x07, "Other") { } // Not defined
        public override void onEnable()
        {
            new Thread(() => {
                string treroAction = Interaction.InputBox("Please enter action (remove/add/list)", "Trero (Friends)").ToLower();

                if (treroAction == "list")
                {
                    foreach (string str in Game.CustomDefines.friends)
                        Console.WriteLine(str);
                    return;
                }

                string username = Interaction.InputBox("Please enter player username", "Trero (Friends)").ToLower();

                if (treroAction == "remove")
                {
                    foreach (string str in Game.CustomDefines.friends)
                        if (username == str)
                            Game.CustomDefines.friends.Remove(str);
                    return;
                }
                if (treroAction == "add")
                {
                    Game.CustomDefines.friends.Add(username);
                    return;
                }
            }).Start();
        }
    }
}
