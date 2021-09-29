#region

using System;
using System.Linq;
using Trero.ClientBase.VersionBase;

#endregion

namespace Trero.ClientBase.EntityBase
{
    internal class Actor
    {
        public ulong addr;

        public Actor(ulong addr)
        {
            this.addr = addr;
        }

        public bool isValid => MCM.readInt(addr) != 0; // isValid

        public Vector3 position
        {
            get
            {
                var vec = Base.Vec3();

                vec.x = MCM.readFloat(addr + VersionClass.GetData("positionX"));
                vec.y = MCM.readFloat(addr + VersionClass.GetData("positionX") + 4);
                vec.z = MCM.readFloat(addr + VersionClass.GetData("positionX") + 8);

                return vec;
            }
            set => Teleport(value);
        } // Position

        public int gamemode
        {
            get => (int)(MCM.readInt64(addr + VersionClass.GetData("positionX")) / 4294967296);
            set => MCM.writeInt64(addr + VersionClass.GetData("positionX"), (ulong)(value * 4294967296));
        } // Gamemode

        public int isFalling
        {
            get
            {
                var value = -1;
                value = (int)MCM.readInt64(addr + VersionClass.GetData("JumpForBHop"));
                return value;
            }
            set => MCM.writeInt64(addr + VersionClass.GetData("JumpForBHop"), (ulong)value);
        } // Is currently falling?

        public Vector3 velocity
        {
            get
            {
                var vec = Base.Vec3();

                vec.x = MCM.readFloat(addr + VersionClass.GetData("velocity"));
                vec.y = MCM.readFloat(addr + VersionClass.GetData("velocity") + 4);
                vec.z = MCM.readFloat(addr + VersionClass.GetData("velocity") + 8);

                return vec;
            }
            set
            {
                MCM.writeFloat(addr + VersionClass.GetData("velocity"), value.x);
                MCM.writeFloat(addr + VersionClass.GetData("velocity") + 4, value.y);
                MCM.writeFloat(addr + VersionClass.GetData("velocity") + 8, value.z);
            }
        } // Velocity

        public Vector2 rotation
        {
            get
            {
                var vec = Base.Vec2();

                vec.x = MCM.readFloat(addr + VersionClass.GetData("bodyRots"));
                vec.y = MCM.readFloat(addr + VersionClass.GetData("bodyRots") + 4);

                return vec;
            }
        } // Rotations

        public Vector2 compassRotations
        {
            get
            {
                var vec = rotation;

                vec.y = new float[] { 1, 2, 3, 4 }.OrderBy(v => Math.Abs((long)v - (vec.y + 180f) / 90f)).First();
                vec.x = new float[] { 1, 2, 3, 4 }.OrderBy(v => Math.Abs((long)v - (vec.x + 90f) / 180f)).First();

                return vec;
            }
        } // Compass Rotations

        public bool onGround
        {
            get => MCM.readInt(addr + VersionClass.GetData("onGround")) != 0;
            set
            {
                if (value) MCM.writeInt(addr + VersionClass.GetData("onGround"), 16777473);
                else MCM.writeInt(addr + VersionClass.GetData("onGround"), 0);
            }
        } // onGround

        public bool onGround2 => MCM.readInt(addr + VersionClass.GetData("onGround2")) != 0; // onGround2

        public string username => MCM.readString(addr + VersionClass.GetData("username"), 32); // Username

        public string type => MCM.readString(addr + VersionClass.GetData("entityType"), 32); // Type

        public Vector2 hitbox
        {
            get
            {
                var vec = Base.Vec2();

                vec.x = MCM.readFloat(addr + VersionClass.GetData("hitbox"));
                vec.y = MCM.readFloat(addr + VersionClass.GetData("hitbox") + 4);

                return vec;
            }
            set
            {
                MCM.writeFloat(addr + VersionClass.GetData("hitbox"), value.x);
                MCM.writeFloat(addr + VersionClass.GetData("hitbox") + 4, value.y);
            }
        } // Hitbox

        public int heldItemCount
        {
            get
            {
                var value = -1;
                value = MCM.readInt(addr + VersionClass.GetData("helditemCount"));
                return value;
            }
        } // Held item count

        public int holdItem
        {
            get
            {
                var value = -1;
                value = MCM.readInt(addr + VersionClass.GetData("holdItem"));
                return value;
            }
        } // Are you holding an item?

        public int holdItemId
        {
            get
            {
                var value = -1;
                value = MCM.readInt(addr + VersionClass.GetData("holdItemId"));
                return value;
            }
        } // Whats the id of the item your holding?

        public int swingAn
        {
            get => MCM.readInt(addr + VersionClass.GetData("swingAn"));
            set => MCM.writeInt(addr + VersionClass.GetData("swingAn"), value);
        } // Current swing animation point (0-4?)

        public void Teleport(AABB advancedAxis)
        {
            MCM.writeFloat(addr + VersionClass.GetData("positionX"), advancedAxis.lower.x);
            MCM.writeFloat(addr + VersionClass.GetData("positionX") + 4, advancedAxis.lower.y);
            MCM.writeFloat(addr + VersionClass.GetData("positionX") + 8, advancedAxis.lower.z);

            MCM.writeFloat(addr + VersionClass.GetData("positionX") + 12, advancedAxis.upper.x);
            MCM.writeFloat(addr + VersionClass.GetData("positionX") + 16, advancedAxis.upper.y);
            MCM.writeFloat(addr + VersionClass.GetData("positionX") + 20, advancedAxis.upper.z);
        } // Teleportation

        public void Teleport(float x, float y, float z)
        {
            Teleport(new AABB(Base.Vec3(x, y, z), Base.Vec3(x + .6f, y + 1.8f, z + .6f)));
        } // Teleportation

        public void Teleport(Vector3 _Vec3)
        {
            Teleport(_Vec3.x, _Vec3.y, _Vec3.z);
        } // Teleportation
    }
}