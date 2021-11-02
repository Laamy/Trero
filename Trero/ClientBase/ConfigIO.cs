using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trero.Modules.vModuleExtra;

namespace Trero.ClientBase
{
    class ConfigIO
    {
        public List<List<int>> moduleBypasses = new List<List<int>>();
        public List<bool> enableStates = new List<bool>();
        public List<char> moduleKeybinds = new List<char>();
        public List<string> moduleNames = new List<string>();
    }
}
