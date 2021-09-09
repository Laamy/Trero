using System.Linq;

namespace Trero.ClientBase.VersionBase
{
    class IVersion // version template
    {
        public string name;
        public object[] sdk;

        public IVersion(object[] list)
        {
            name = list[0].ToString();
            sdk = list.Skip(1).ToArray();
        }
    }
}
