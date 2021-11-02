#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;

using Trero.ClientBase;

#endregion

namespace Trero.Modules
{
    internal class LoadConfig : Module
    {
        public LoadConfig() : base("LoadConfig", (char)0x07, "Other", "Load the current config.json file if it exists")
        {
        }

        public override void OnEnable()
        {
            Console.WriteLine(@"Checking for config...");
            if (File.Exists("config.json"))
            {
                Console.WriteLine(@"Found config!");

                var config = new JavaScriptSerializer().Deserialize<ConfigIO>(File.ReadAllText("config.json"));

                foreach (var c in Program.Modules)
                {
                    int index = 0;
                    foreach (var a in config.moduleNames)
                    {
                        if (c.name == a)
                        {
                            c.enabled = config.enableStates[index];
                            int index2 = 0;
                            foreach (var b in c.bypasses)
                            {
                                c.bypasses[index2].curIndex = config.moduleBypasses[index][index2];
                                index2++;
                            }
                            c.keybind = config.moduleKeybinds[index];
                        }
                        index++;
                    }
                }
            }
            else Console.WriteLine(@"No config found, ignoring...");
        }
    }
}