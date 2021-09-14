#region

using System.Linq;

#endregion

namespace Trero.ClientBase.VersionBase
{
    internal class IVersion // version template
    {
        public string name;
        public object[] sdk;

        //This is retarded ngl
        public IVersion(object[] list)
        {
            name = list[0].ToString();
            sdk = list.Skip(1).ToArray();
        }
    }
}