#region

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

#endregion

namespace Trero.ClientBase
{
    public class HexHandler
    {
        public static string ToHex(int value)
        {
            var outp = value.ToString("X");
            return outp;
        }

        public static int ToInt(string value)
        {
            var hexString = int.Parse(value, NumberStyles.HexNumber);
            return hexString;
        }

        public static long ToLong(string value)
        {
            var outp = Convert.ToInt64(value, 16);
            return outp;
        }

        public static ulong ToULong(object value)
        {
            var outp = Convert.ToUInt64(value);
            return outp;
        }

        public static string AddBytes(string hexString, int bytes)
        {
            var f = ToInt(hexString) + bytes; // Convert hexString to int then plus x amount of bytes to it

            var outp = ToHex(f); // Convert back to hexString

            return outp;
        }

        public static Vector2 CompassClamp(float[] array, Vector2 c)
        {
            var vec = c;

            vec.y = array.OrderBy(v => Math.Abs((long)v - (vec.y + 180f) / (360f / array.Length))).First();
            vec.x = array.OrderBy(v => Math.Abs((long)v - (vec.x + 90f) / (180f / array.Length))).First();

            return vec;
        }
    }

    internal static class Base
    {
        public static Vector3 Vec3(float _ = 0, float v = 0, float c = 0)
        {
            return new Vector3(_, v, c);
        }

        public static Vector3 Vec3(string v)
        {
            return new Vector3(v);
        }

        public static Vector3 Vec3(string x, string y, string z)
        {
            return new Vector3($"{x},{y},{z}");
        }

        public static iVector3 IVec3(int _ = 0, int v = 0, int c = 0)
        {
            return new iVector3(_, v, c);
        }

        public static Vector2 Vec2(float _ = 0f, float v = 0f)
        {
            return new Vector2(_, v);
        }

        public static Vector2 Vec2(string v)
        {
            return new Vector2(v);
        }

        public static ulong ToAddr(object v)
        {
            return (ulong)v;
        }
    }

    public static class Ext
    {
        public static int ClosestTo(this IEnumerable<int> collection, int target)
        {
            var closest = int.MaxValue;
            var minDifference = int.MaxValue;
            foreach (var element in collection)
            {
                var difference = Math.Abs((long)element - target);
                if (minDifference <= difference) continue;
                minDifference = (int)difference;
                closest = element;
            }

            return closest;
        }

        public static T Clone<T>(this T controlToClone)
            where T : Control
        {
            var controlProperties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var instance = Activator.CreateInstance<T>();

            foreach (var propInfo in controlProperties)
                if (propInfo.CanWrite)
                    if (propInfo.Name != "WindowTarget")
                        propInfo.SetValue(instance, propInfo.GetValue(controlToClone, null), null);

            return instance;
        }

        public static IEnumerable<Control> GetAllChildren(this Control root)
        {
            var stack = new Stack<Control>();
            stack.Push(root);

            while (stack.Any())
            {
                var next = stack.Pop();
                foreach (Control child in next.Controls)
                    stack.Push(child);
                yield return next;
            }
        }
    }
}