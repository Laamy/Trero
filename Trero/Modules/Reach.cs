#region

using Trero.ClientBase;

#endregion

namespace Trero.Modules
{
    internal class Reach : Module
    {
        public Reach() : base("Reach", (char)0x07, "Combat")
        {
        } // Not defined

        public override void OnEnable()
        {
            MCM.writeBaseFloat(0x1CAEB90, 7);
            base.OnEnable();
        }

        public override void OnDisable()
        {
            MCM.writeBaseFloat(0x1CAEB90, 3);
            base.OnDisable();
        }
    }
}