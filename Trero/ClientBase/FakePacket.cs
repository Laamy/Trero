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
        public static void createClientObj() // create a fake client sided message inside of mc
        {
            Game.isLookingAtBlock = 0;
            Game.SideSelect = 1;

            Mouse.MouseEvent(Mouse.MouseEventFlags.MOUSEEVENTF_LEFTDOWN);
        }
    }
}
