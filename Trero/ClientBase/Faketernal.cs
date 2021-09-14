#region

using System;
using System.Collections.Generic;
using Trero.ClientBase.FaketernalBase;
using Trero.ClientBase.KeyBase;
using Trero.ClientBase.VersionBase;

#endregion

namespace Trero.ClientBase
{
    internal class Faketernal
    {
        public static class
            ClientObj // most of this has been removed because i need to update the pointers and im to tired rn so
        {
            public static ulong chatInstance
            {
                get // 0x04120400, 0x8, 0x48, 0xA0, 0x128
                {
                    return MCM.baseEvaluatePointer(HexHandler.ToULong(VersionClass.GetData("chatBase")), new[]
                    {
                        VersionClass.GetData("chatBase+1"),
                        VersionClass.GetData("chatBase+2"),
                        VersionClass.GetData("chatBase+3"),
                        VersionClass.GetData("chatBase+4")
                    });
                }
            }

            public static List<MessageObj> chatMessages => null;

            public static MessageObj getMessageAt(ulong index) // Get the minecraft message at index
            {
                MessageObj msg = null;

                msg = new MessageObj(MCM.evaluatePointer(chatInstance, new ulong[]
                {
                    index + 1 * 0x28 + index + 1 * 0xD8,
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

        public static class Utils
        {
            public static float NextFloat(float min, float max)
            {
                var random = new Random();
                var val = random.NextDouble() * (max - min) + min;
                return (float)val;
            }
        }
    }
}