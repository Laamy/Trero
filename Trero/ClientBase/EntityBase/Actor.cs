using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trero.ClientBase.VersionBase;

namespace Trero.ClientBase.EntityBase
{
    class Actor
    {
        public ulong addr;
        public Actor(ulong addr) => this.addr = addr;

        public bool isValid
        {
            get => MCM.readInt(addr) != 0;
        } // isValid

        public void teleport(AABB advancedAxis)
        {
            MCM.writeFloat(addr + (ulong)VersionClass.getData("positionX"), advancedAxis.x.x);
            MCM.writeFloat(addr + (ulong)VersionClass.getData("positionX") + 4, advancedAxis.x.y);
            MCM.writeFloat(addr + (ulong)VersionClass.getData("positionX") + 8, advancedAxis.x.z);

            MCM.writeFloat(addr + (ulong)VersionClass.getData("positionX") + 12, advancedAxis.y.x);
            MCM.writeFloat(addr + (ulong)VersionClass.getData("positionX") + 16, advancedAxis.y.y);
            MCM.writeFloat(addr + (ulong)VersionClass.getData("positionX") + 20, advancedAxis.y.z);
        }// Teleportation
        public void teleport(float x, float y, float z)
        {
            teleport(new AABB(Base.Vec3(x, y, z), Base.Vec3(x + .6f, y + 1.8f, z + .6f)));
        } // Teleportation
        public void teleport(Vector3 _Vec3)
        {
            teleport(_Vec3.x, _Vec3.y, _Vec3.z);
        }// Teleportation

        public Vector3 position
        {
            get
            {
                Vector3 vec = Base.Vec3();

                vec.x = MCM.readFloat(addr + VersionClass.getData("positionX"));
                vec.y = MCM.readFloat(addr + VersionClass.getData("positionX") + 4);
                vec.z = MCM.readFloat(addr + VersionClass.getData("positionX") + 8);

                return vec;
            }
            set => teleport(value);
        } // Position
        public int gamemode
        {
            get
            {
                int gamemode = -1;
                gamemode = (int)(MCM.readInt64(addr + VersionClass.getData("positionX")) / 4294967296);
                return gamemode;
            }
            set => MCM.writeInt64(addr + VersionClass.getData("positionX"), (ulong)(value * 4294967296));
        } // Gamemode
        public int isFalling
        {
            get
            {
                int value = -1;
                value = (int)(MCM.readInt64(addr + VersionClass.getData("JumpForBHop")));
                return value;
            }
            set => MCM.writeInt64(addr + VersionClass.getData("JumpForBHop"), (ulong)value);
        } // Is currently falling?
        public Vector3 velocity
        {
            get
            {
                Vector3 vec = Base.Vec3();

                vec.x = MCM.readFloat(addr + VersionClass.getData("velocity"));
                vec.y = MCM.readFloat(addr + VersionClass.getData("velocity") + 4);
                vec.z = MCM.readFloat(addr + VersionClass.getData("velocity") + 8);

                return vec;
            }
            set
            {
                MCM.writeFloat(addr + VersionClass.getData("velocity"), value.x);
                MCM.writeFloat(addr + VersionClass.getData("velocity") + 4, value.y);
                MCM.writeFloat(addr + VersionClass.getData("velocity") + 8, value.z);
            }
        } // Velocity
        public Vector2 rotation
        {
            get
            {
                Vector2 vec = Base.Vec2();

                vec.x = MCM.readFloat(addr + VersionClass.getData("bodyRots"));
                vec.y = MCM.readFloat(addr + VersionClass.getData("bodyRots") + 4);

                return vec;
            }
        } // Rotations
        public Vector2 compassRotations
        {
            get
            {
                Vector2 vec = rotation;

                vec.y = (new float[] { 1, 2, 3, 4 }).OrderBy(v => Math.Abs((long)v - ((vec.y + 180f) / 90f))).First();
                vec.x = (new float[] { 1, 2, 3, 4 }).OrderBy(v => Math.Abs((long)v - ((vec.x + 90f) / 180f))).First();

                return vec;
            }
        } // Compass Rotations
        public bool onGround
        {
            get => MCM.readInt(addr + VersionClass.getData("onGround")) != 0;
            set
            {
                if (value) MCM.writeInt(addr + VersionClass.getData("onGround"), 16777473);
                else MCM.writeInt(addr + VersionClass.getData("onGround"), 0);
            }
        } // onGround
        public bool onGround2
        {
            get => MCM.readInt(addr + VersionClass.getData("onGround2")) != 0;
        } // onGround2
        public string username
        {
            get => MCM.readString(addr + VersionClass.getData("username"), 32);
        } // Username
        public string type
        {
            get => MCM.readString(addr + VersionClass.getData("entityType"), 32);
        } // Type
        public Vector2 hitbox
        {
            get
            {
                Vector2 vec = Base.Vec2();

                vec.x = MCM.readFloat(addr + VersionClass.getData("hitbox"));
                vec.y = MCM.readFloat(addr + VersionClass.getData("hitbox") + 4);

                return vec;
            }
            set
            {
                MCM.writeFloat(addr + VersionClass.getData("hitbox"), value.x);
                MCM.writeFloat(addr + VersionClass.getData("hitbox") + 4, value.y);
            }
        } // Hitbox
        public int heldItemCount
        {
            get
            {
                int value = -1;
                value = (int)(MCM.readInt(addr + VersionClass.getData("helditemCount")));
                return value;
            }
        } // Held item count
        public int holdItem
        {
            get
            {
                int value = -1;
                value = (int)(MCM.readInt(addr + VersionClass.getData("holdItem")));
                return value;
            }
        } // Are you holding an item?
        public int holdItemId
        {
            get
            {
                int value = -1;
                value = (int)(MCM.readInt(addr + VersionClass.getData("holdItemId")));
                return value;
            }
        } // Whats the id of the item your holding?
        public int swingAn
        {
            get
            {
                int value = -1;
                value = (int)(MCM.readInt(addr + VersionClass.getData("swingAn")));
                return value;
            }
            set => MCM.writeInt(addr + VersionClass.getData("swingAn"), value);
        } // Current swing animation point (0-4?)
    }
}
