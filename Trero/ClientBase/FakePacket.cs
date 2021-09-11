using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trero.ClientBase.KeyBase;

namespace Trero.ClientBase
{
    class FakePacket
    {

        public static class ClientObj// most of this has been removed because i need to update the pointers and im to tired rn so
        {
            public static void createClientObj() // create a fake client sided message inside of mc
            {
                Game.isLookingAtBlock = 0;
                Game.SideSelect = 1;

                Mouse.MouseEvent(Mouse.MouseEventFlags.MOUSEEVENTF_RIGHTDOWN);
            }
            public static void setClientObj(string v) // edit last fake message inside of mc
            {
                // chat pointer is defined in version i think you know what to do from here
            }
        }
    }
}
