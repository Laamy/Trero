#region

using Trero.ClientBase;

#endregion

namespace Trero.Modules
{
    internal class InPvPTower : Module
    {
        int teleportCount = 0;
        public InPvPTower() : base("InPvPTower", (char)0x07, "World") { }


        public override void OnTick()
        {
            switch (teleportCount)
            {
                case 0:
                    Game.teleport(-246, 52, 910);
                    break;
                case 1:
                    Game.teleport(-221, 51, 684);
                    break;
                case 2:
                    // Not here yet
                    break;
                case 3:
                    // Not here yet
                    break;
                default:
                    teleportCount = -1;
                    break;
            }

            teleportCount++;
        }
    }
}