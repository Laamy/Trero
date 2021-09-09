using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Trero.ClientBase
{
    public class HexHandler
    {
        public static string toHex(int value)
        {
            string outp = value.ToString("X");
            return outp;
        }

        public static int toInt(string value)
        {
            int hexString = int.Parse(value, NumberStyles.HexNumber);
            return hexString;
        }

        public static long toLong(string value)
        {
            long outp = Convert.ToInt64(value, 16);
            return outp;
        }

        public static ulong toULong(object value)
        {
            ulong outp = Convert.ToUInt64(value);
            return outp;
        }

        public static string addBytes(string hexString, int bytes)
        {
            var f = toInt(hexString) + bytes; // Convert hexString to int then plus x amount of bytes to it

            string outp = toHex(f); // Convert back to hexString

            return outp;
        }

        public static Vector2 compassClamp(float[] array, Vector2 c)
        {
            Vector2 vec = c;

            vec.y = array.OrderBy(v => Math.Abs((long)v - ((vec.y + 180f) / (360f / array.Length)))).First();
            vec.x = array.OrderBy(v => Math.Abs((long)v - ((vec.x + 90f) / (180f / array.Length)))).First();

            return vec;
        }
    }

    class Base
    {
        public static Vector3 Vec3(float _ = 0, float v = 0, float c = 0)
        {
            Vector3 tempVec = new Vector3(_, v, c);
            return tempVec;
        }
        public static Vector3 Vec3(string v)
        {
            Vector3 tempVec = new Vector3(v);
            return tempVec;
        }

        public static iVector3 iVec3(int _ = 0, int v = 0, int c = 0)
        {
            iVector3 tempVec = new iVector3(_, v, c);
            return tempVec;
        }

        public static Vector2 Vec2(float _ = 0f, float v = 0f)
        {
            Vector2 tempVec = new Vector2(_, v);
            return tempVec;
        }
        public static Vector2 Vec2(string v)
        {
            Vector2 tempVec = new Vector2(v);
            return tempVec;
        }

        public static ulong ToAddr(object v)
        {
            ulong addr = 0x0;
            addr = (ulong)v;
            return addr;
        }
    }

    static class Ext
    {
        public static int ClosestTo(this IEnumerable<int> collection, int target)
        {
            var closest = int.MaxValue;
            var minDifference = int.MaxValue;
            foreach (var element in collection)
            {
                var difference = Math.Abs((long)element - target);
                if (minDifference > difference)
                {
                    minDifference = (int)difference;
                    closest = element;
                }
            }

            return closest;
        }
    }
}
