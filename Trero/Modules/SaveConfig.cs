#region

using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;

using Trero.ClientBase;

#endregion

namespace Trero.Modules
{
    internal class SaveConfig : Module
    {
        public SaveConfig() : base("SaveConfig", (char)0x07, "Other", "Save your current client config as a config.json file!")
        {
        }

        public override void OnEnable()
        {
            ConfigIO config = new ConfigIO();

            foreach (var c in Program.Modules)
            {
                config.enableStates.Add(c.enabled);
                config.moduleKeybinds.Add(c.keybind);

                var list = new List<int>();
                foreach (var b in c.bypasses)
                    list.Add(b.curIndex);
                config.moduleBypasses.Add(list);

                config.moduleNames.Add(c.name);
            }

            string json = new JavaScriptSerializer().Serialize(config);
            File.WriteAllText("config.json", json);
        }
    }
}