using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trero.ClientBase.FaketernalBase;
using Trero.ClientBase.KeyBase;
using Trero.ClientBase.VersionBase;

namespace Trero.ClientBase
{
    class Faketernal
    {
        public static class ClientObj// most of this has been removed because i need to update the pointers and im to tired rn so
        {
            public static ulong chatInstance
            {
                get // 0x04120400, 0x8, 0x48, 0xA0, 0x128
                {
                    return MCM.baseEvaluatePointer(HexHandler.toULong(VersionClass.getData("chatBase")), new ulong[] {
                    VersionClass.getData("chatBase+1"),
                    VersionClass.getData("chatBase+2"),
                    VersionClass.getData("chatBase+3"),
                    VersionClass.getData("chatBase+4")
                });
                }
            }

            public static List<MessageObj> chatMessages
            {
                get
                {
                    return null;
                }
            }

            public static MessageObj getMessageAt(ulong index) // Get the minecraft message at index
            {
                MessageObj msg = null;

                msg = new MessageObj(MCM.evaluatePointer(chatInstance, new ulong[] {
                    (index + 1 * 0x28) + (index + 1 * 0xD8),
                    0x0
                })); //MCM.readInt64(chatInstance + ((index + 1 * 0x28) + (index + 1 * 0xD8))));

                return msg;
            }

            public static void createClientObj() // create a fake client sided message inside of mc
            {
                Game.isLookingAtBlock = 0;
                Game.SideSelect = 1;

                Mouse.MouseEvent(Mouse.MouseEventFlags.MOUSEEVENTF_RIGHTDOWN);
            }
            public static void setClientObj(MessageObj v) // edit last fake message inside of mc
            {
                // getMessageAt
            }
        }
    }
}
