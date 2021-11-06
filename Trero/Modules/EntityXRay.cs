#region

using Trero.ClientBase;

#endregion

namespace Trero.Modules
{
    internal class EntityXRay : Module
    {
        public EntityXRay() : base("EntityXRay", (char)0x07, "World", "Stop rendering most types of blocks so you can see all entities and chests/shulkers around you")
        {
        } // Not defined

        public override void OnEnable()
        {
            MCM.writeBaseBytes(0x2BEA094, MCM.ceByte2Bytes("90 90"));
            base.OnEnable();
        }

        public override void OnDisable()
        {
            MCM.writeBaseBytes(0x2BEA094, MCM.ceByte2Bytes("75 16"));
            base.OnDisable();
        }
    }
}