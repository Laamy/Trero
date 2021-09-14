#region

using System;
using System.Threading;
using Microsoft.VisualBasic;
using Trero.ClientBase;

#endregion

namespace Trero.Modules
{
    internal class Friends : Module
    {
        public Friends() : base("Friends", (char)0x07)
        {
        } // Not defined

        public override void OnEnable()
        {
            new Thread(() =>
            {
                var treroAction = Interaction.InputBox("Please enter action (remove/add/list)", "Trero (Friends)")
                    .ToLower();

                if (treroAction == "list")
                {
                    foreach (var str in Game.CustomDefines.friends)
                        Console.WriteLine(str);
                    return;
                }

                var username = Interaction.InputBox("Please enter player username", "Trero (Friends)").ToLower();

                if (treroAction != "remove")
                {
                    if (treroAction != "add") return;
                    Game.CustomDefines.friends.Add(username);
                    return;
                }

                for (var index = 0; index < Game.CustomDefines.friends.Count; index++)
                {
                    var str = Game.CustomDefines.friends[index];
                    if (username == str)
                        Game.CustomDefines.friends.Remove(str);
                }
            }).Start();
        }
    }
}