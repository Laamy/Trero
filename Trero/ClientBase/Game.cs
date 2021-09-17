#region

using System;
using System.Collections.Generic;
using System.Linq;
using Trero.ClientBase.EntityBase;
using Trero.ClientBase.KeyBase;
using Trero.ClientBase.VersionBase;

#endregion

namespace Trero.ClientBase
{
    internal class Game
    {
        public static Random ran = new Random();

        // Actor Structor
        public static ulong clientInstance
        {
            get
            {
                return MCM.baseEvaluatePointer(HexHandler.ToULong(VersionClass.currentVersion.sdk[0]), new[]
                {
                    VersionClass.GetData("baseOffset+1"),
                    VersionClass.GetData("baseOffset+2")
                });
            }
        } // clientInstance

        public static ulong localPlayer
        {
            get
            {
                return MCM.evaluatePointer(clientInstance, new ulong[]
                {
                    VersionClass.GetData("baseOffset+3"),
                    0x0
                });
            }
        } // localPlayer

        public static ulong level => MCM.readInt64(localPlayer + VersionClass.GetData("level")); // level

        public static ulong EntityListStart =>
            MCM.readInt64(level + VersionClass.GetData("entitylist+1")); // entityliststart

        public static ulong EntityListEnd =>
            MCM.readInt64(level + VersionClass.GetData("entitylist+2")); // entitylistend

        public static bool isNull
        {
            get
            {
                if (screenData.StartsWith("toast_screen")) // screenData.StartsWith("toast_screen")
                    return false;
                return true;
            }
        } // isValid

        public static Vector3 position
        {
            get
            {
                var vec = Base.Vec3();

                vec.x = MCM.readFloat(localPlayer + VersionClass.GetData("positionX"));
                vec.y = MCM.readFloat(localPlayer + VersionClass.GetData("positionX") + 4);
                vec.z = MCM.readFloat(localPlayer + VersionClass.GetData("positionX") + 8);

                return vec;
            }
            set => teleport(value);
        } // Position

        public static Vector3 lookingAtPosition
        {
            get
            {
                var vec = Base.Vec3();

                vec.x = MCM.readFloat(localPlayer - VersionClass.GetData("lookingAtBlock"));
                vec.y = MCM.readFloat(localPlayer - VersionClass.GetData("lookingAtBlock") + 4);
                vec.z = MCM.readFloat(localPlayer - VersionClass.GetData("lookingAtBlock") + 8);

                return vec;
            }
        } // Position

        public static int gamemode
        {
            get
            {
                var gamemode = -1;
                gamemode = (int)(MCM.readInt64(localPlayer + VersionClass.GetData("gamemode")) / 4294967296);
                return gamemode;
            }
            set => MCM.writeInt64(localPlayer + VersionClass.GetData("gamemode"), (ulong)(value * 4294967296));
        } // Gamemode

        public static void setFieldOfView(float v)
        {
            MCM.writeFloat(localPlayer + VersionClass.GetData("fieldOfView"), v);
        }

        public static int isFalling
        {
            get
            {
                var value = -1;
                value = (int)MCM.readInt64(localPlayer + VersionClass.GetData("JumpForBHop"));
                return value;
            }
            set => MCM.writeInt(localPlayer + VersionClass.GetData("JumpForBHop"), value);
        }

        public static Vector3 velocity
        {
            get
            {
                var vec = Base.Vec3();

                vec.x = MCM.readFloat(localPlayer + VersionClass.GetData("velocity"));
                vec.y = MCM.readFloat(localPlayer + VersionClass.GetData("velocity") + 4);
                vec.z = MCM.readFloat(localPlayer + VersionClass.GetData("velocity") + 8);

                return vec;
            }
            set
            {
                MCM.writeFloat(localPlayer + VersionClass.GetData("velocity"), value.x);
                MCM.writeFloat(localPlayer + VersionClass.GetData("velocity") + 4, value.y);
                MCM.writeFloat(localPlayer + VersionClass.GetData("velocity") + 8, value.z);
            }
        } // Velocity

        public static Vector2 rotation
        {
            get
            {
                var vec = Base.Vec2();

                vec.x = MCM.readFloat(localPlayer + VersionClass.GetData("bodyRots"));
                vec.y = MCM.readFloat(localPlayer + VersionClass.GetData("bodyRots") + 4);

                return vec;
            }
        } // Rotations

        public static Vector2 compassRotations
        {
            get
            {
                var vec = rotation;

                vec.y = new float[] { 1, 2, 3, 4 }.OrderBy(v => Math.Abs((long)v - (vec.y + 180f) / 90f)).First();
                vec.x = new float[] { 1, 2, 3, 4 }.OrderBy(v => Math.Abs((long)v - (vec.x + 90f) / 180f)).First();

                return vec;
            }
        } // CompassRotations

        public static bool onGround
        {
            get => MCM.readInt(localPlayer + VersionClass.GetData("onGround")) != 0;
            set
            {
                if (value) MCM.writeInt(localPlayer + VersionClass.GetData("onGround"), 16777473);
                else MCM.writeInt(localPlayer + VersionClass.GetData("onGround"), 0);
            }
        } // onGround

        public static bool isFlying
        {
            get => MCM.readInt(localPlayer + VersionClass.GetData("isFlying")) != 0;
            set => MCM.writeInt(localPlayer + VersionClass.GetData("isFlying"), value ? 0 : 1);
        } // onGround

        public static bool inWater => MCM.readInt(localPlayer + VersionClass.GetData("inWater")) != 0; // inWater

        public static bool inInventory =>
            MCM.readInt(localPlayer + VersionClass.GetData("inInventory")) != 1; // inInventory

        public static bool isLookingAtEntity =>
            MCM.readInt(localPlayer + VersionClass.GetData("lookingEntityId")) != -1; // lookingEntity

        public static ulong lookingEntityId
        {
            get => MCM.readInt64(localPlayer + VersionClass.GetData("lookingEntityId"));
            set => MCM.writeInt64(localPlayer + VersionClass.GetData("lookingEntityId"), value);
        }

        public static int touchingObject => MCM.readInt(localPlayer + VersionClass.GetData("onGround2")); // onGround2

        public static float stepHeight
        {
            get => MCM.readFloat(localPlayer + VersionClass.GetData("stepHeight"));
            set => MCM.writeFloat(localPlayer + VersionClass.GetData("stepHeight"), value);
        } // stepHeight

        public static Vector3 lVector
        {
            get
            {
                Vector3 tempVec;

                var cYaw = (rotation.y + 89.9f) * (float)Math.PI / 178f;
                var cPitch = rotation.x * (float)Math.PI / 178f;

                tempVec = dirVect(cYaw, cPitch);

                return tempVec;
            }
            // set { }
        } // Looking Vector

        public static string username => MCM.readString(localPlayer + VersionClass.GetData("username"), 32); // Username

        public static string screenData =>
            MCM.readString(
                MCM.baseEvaluatePointer(HexHandler.ToULong(VersionClass.GetData("screenT+1")),
                    new[] { VersionClass.GetData("screenT+2") }), 128); // Username

        public static string type => MCM.readString(localPlayer + VersionClass.GetData("entityType"), 32); // Type

        public static int heldItemCount
        {
            get
            {
                var value = -1;
                value = MCM.readInt(localPlayer + VersionClass.GetData("helditemCount"));
                return value;
            }
        } // Held item count

        public static int holdItem
        {
            get
            {
                var value = -1;
                value = MCM.readInt(localPlayer + VersionClass.GetData("holdItem"));
                return value;
            }
        } // Are you holding an item?

        public static int holdItemId
        {
            get
            {
                var value = -1;
                value = MCM.readInt(localPlayer + VersionClass.GetData("holdItemId"));
                return value;
            }
        } // Whats the id of the item your holding?

        public static int swingAn
        {
            get
            {
                var value = -1;
                value = MCM.readInt(localPlayer + VersionClass.GetData("swingAn"));
                return value;
            }
            set => MCM.writeInt(localPlayer + VersionClass.GetData("swingAn"), value);
        } // Current swing animation point (0-4?)

        public static int worldAge
        {
            get
            {
                var value = -1;
                value = MCM.readInt(localPlayer + VersionClass.GetData("worldAge"));
                return value;
            }
            set => MCM.writeInt(localPlayer + VersionClass.GetData("worldAge"), value);
        } // Current world age

        // Level
        public static int isLookingAtBlock
        {
            get => MCM.readInt(level + VersionClass.GetData("LookingAtBlock"));
            set => MCM.writeInt(level + VersionClass.GetData("LookingAtBlock"), value);
        } // isLookingAtBlock

        public static iVector3 SelectedBlock
        {
            get
            {
                var vec = Base.IVec3();

                vec.x = MCM.readInt(level + VersionClass.GetData("SelectedBlock"));
                vec.y = MCM.readInt(level + VersionClass.GetData("SelectedBlock") + 4);
                vec.z = MCM.readInt(level + VersionClass.GetData("SelectedBlock") + 8);

                return vec;
            }
            set
            {
                MCM.writeInt(level + VersionClass.GetData("SelectedBlock"), value.x);
                MCM.writeInt(level + VersionClass.GetData("SelectedBlock") + 4, value.y);
                MCM.writeInt(level + VersionClass.GetData("SelectedBlock") + 8, value.z);
            }
        } // SelectedBlock

        public static int SideSelect
        {
            get => MCM.readInt(level + VersionClass.GetData("SideSelect"));
            set => MCM.writeInt(level + VersionClass.GetData("SideSelect"), value);
        } // SideSelect

        public static void teleport(AABB advancedAxis)
        {
            MCM.writeFloat(localPlayer + VersionClass.GetData("positionX"), advancedAxis.x.x);
            MCM.writeFloat(localPlayer + VersionClass.GetData("positionX") + 4, advancedAxis.x.y);
            MCM.writeFloat(localPlayer + VersionClass.GetData("positionX") + 8, advancedAxis.x.z);

            MCM.writeFloat(localPlayer + VersionClass.GetData("positionX") + 12, advancedAxis.y.x);
            MCM.writeFloat(localPlayer + VersionClass.GetData("positionX") + 16, advancedAxis.y.y);
            MCM.writeFloat(localPlayer + VersionClass.GetData("positionX") + 20, advancedAxis.y.z);
        } // Teleportation

        public static void teleport(float x, float y, float z)
        {
            teleport(new AABB(Base.Vec3(x, y, z), Base.Vec3(x + .6f, y + 1.8f, z + .6f)));
        } // Teleportation

        public static void teleport(Vector3 _Vec3)
        {
            teleport(_Vec3.x, _Vec3.y, _Vec3.z);
        } // Teleportation

        public static void SexActor(Actor actor)
        {
            var pos = actor.position;

            pos.x += Faketernal.Utils.NextFloat(-0.3f, 0.3f);
            pos.y += 1;
            pos.z += Faketernal.Utils.NextFloat(-0.3f, 0.3f);

            teleport(pos);
        } // Teleportation

        public static void Attack(Actor actor)
        {
            lookingEntityId = actor.addr;
            Mouse.MouseEvent(Mouse.MouseEventFlags.MOUSEEVENTF_LEFTDOWN);
        } // Attack

        public static Vector3 dirVect(float x, float y) // pretty sure this is fine wtf
        {
            var tempVec = Base.Vec3(); // create empty vector

            tempVec.x = (float)Math.Cos(x) * (float)Math.Cos(y);
            tempVec.y = (float)Math.Sin(y);
            tempVec.z = (float)Math.Sin(x) * (float)Math.Cos(y);

            return tempVec;
        } // Directional Vector

        // EntityList
        public static List<Actor> getTypeEntities(string type)
        {
            var entityList = new List<Actor>();
            for (var i = EntityListStart; i < EntityListEnd; i += 0x8)
            {
                if (i == EntityListStart) continue;
                var entity = new Actor(MCM.readInt64(i));
                foreach (var str in CustomDefines.friends)
                    if (str.ToLower() == entity.username.ToLower() && CustomDefines.nofriends == false)
                        continue;
                if (entity.type == type && entity.username.Length > 3)
                    entityList.Add(entity);
            }

            return entityList;
        }

        public static List<Actor> getEntites()
        {
            var entityList = new List<Actor>();
            for (var i = EntityListStart; i < EntityListEnd; i += 0x8)
            {
                if (i == EntityListStart) continue;
                var entity = new Actor(MCM.readInt64(i));
                entityList.Add(entity);
            }

            return entityList;
        }

        public static List<Actor> parseEntities(List<Actor> list)
        {
            var validEnts = new List<Actor>();
            var validCharacters =
                "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm1234567890 §"
                    .ToCharArray(); // § because some servers change usernames
            foreach (var ent in list)
                try
                {
                    var valid = true;
                    foreach (var chr in ent.username.Substring(0, 5))
                    {
                        var validChr = false;
                        foreach (var vChr in validCharacters)
                            if (chr == vChr)
                                validChr = true;
                        if (!validChr)
                            valid = false;
                    }

                    if (valid)
                        validEnts.Add(ent);
                }
                catch
                {
                    validEnts.Add(ent);
                }

            return validEnts;
        }

        public static Actor getClosestEntity()
        {
            Actor vEntity = null;
            var ents = getEntites();
            ents.ForEach(ent =>
            {
                if (vEntity == null)
                {
                    vEntity = ent;
                }
                else
                {
                    var dis1 = position.Distance(ent.position);
                    var dis2 = position.Distance(vEntity.position);

                    if (dis1 < dis2)
                        vEntity = ent;
                }
            });
            return vEntity;
        }

        public static Actor getClosestTypeEntity(string Type)
        {
            Actor vEntity = null;
            var ents = getTypeEntities(Type);
            ents.ForEach(ent =>
            {
                if (vEntity == null)
                {
                    vEntity = ent;
                }
                else
                {
                    var dis1 = position.Distance(ent.position);
                    var dis2 = position.Distance(vEntity.position);

                    if (dis1 < dis2)
                        vEntity = ent;
                }
            });
            return vEntity;
        }

        public static List<Actor> getPlayers()
        {
            if (CustomDefines.antibot)
                return parseEntities(getTypeEntities("player"));
            return getTypeEntities("player");
        }

        public static Actor getClosestPlayer()
        {
            Actor vEntity = null;
            var ents = new List<Actor>();

            if (CustomDefines.antibot)
                ents = parseEntities(getPlayers());
            else
                ents = getPlayers();

            ents.ForEach(ent =>
            {
                foreach (var str in CustomDefines.friends)
                    if (str.ToLower() == ent.username.ToLower() && CustomDefines.nofriends == false)
                        continue;
                if (vEntity == null)
                {
                    vEntity = ent;
                }
                else
                {
                    var dis1 = position.Distance(ent.position);
                    var dis2 = position.Distance(vEntity.position);

                    if (dis1 < dis2)
                        vEntity = ent;
                }
            });
            return vEntity;
        }

        public static class CustomDefines
        {
            public static bool antibot = true;
            public static bool nofriends = false;
            public static List<string> friends = new List<string> { "FootlongTrero" };
        }
    }

    // Struct Defines
    public class GamemodeRegistery // used for .gm (Gamemode)
    {
        private readonly List<List<string>> registery = new List<List<string>>
        {
            new List<string>
            {
                "0",
                "s",
                "survival"
            }, // Survival
            new List<string>
            {
                "1",
                "c",
                "creative"
            }, // Creative
            new List<string>
            {
                "2",
                "a",
                "adventure"
            } // Adventure
        }; // Gamemode Registery

        public GamemodeRegistery(out List<List<string>> list)
        {
            list = registery;
        }
    }

    public struct Vector3
    {
        public float x;
        public float y;
        public float z;

        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector3(string position)
        {
            try
            {
                var parsedStr = position.Replace(" ", "").Split(',');
                x = Convert.ToSingle(parsedStr[0]);
                y = Convert.ToSingle(parsedStr[1]);
                z = Convert.ToSingle(parsedStr[2]);
            }
            catch
            {
                var parsedStr = position.Replace(" ", "").Split(',');
                x = HexHandler.ToLong(parsedStr[0]);
                y = HexHandler.ToLong(parsedStr[1]);
                z = HexHandler.ToLong(parsedStr[2]);
            }
        }

        public float DistanceTo(Vector3 _Vec3)
        {
            float diff_x = x - _Vec3.x, diff_y = y - _Vec3.y, diff_z = z - _Vec3.z;
            var output = (float)Math.Sqrt(diff_x * diff_x + diff_y * diff_y + diff_z * diff_z);
            if ((int)output == 0) output = _Vec3.Distance(this);
            return output;
        }

        public float Distance(Vector3 _Vec3)
        {
            float diff_x = x - _Vec3.x, diff_y = y - _Vec3.y, diff_z = z - _Vec3.z;
            return (float)Math.Sqrt(diff_x * diff_x + diff_y * diff_y + diff_z * diff_z);
        } // messy distance i just neve cleaned up plz ignore ;-;

        public override string ToString()
        {
            return x + "," + y + "," + z;
        }

        internal float Distance(object postion)
        {
            throw new NotImplementedException();
        }
    }

    public struct iVector3
    {
        public int x;
        public int y;
        public int z;

        public iVector3(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public override string ToString()
        {
            return x + "," + y + "," + z;
        }
    }

    public struct Vector2
    {
        public float x;
        public float y;

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2(string position)
        {
            try
            {
                var parsedStr = position.Replace(" ", "").Split(',');
                x = Convert.ToSingle(parsedStr[0]);
                y = Convert.ToSingle(parsedStr[1]);
            }
            catch
            {
                var parsedStr = position.Replace(" ", "").Split(',');
                x = HexHandler.ToLong(parsedStr[0]);
                y = HexHandler.ToLong(parsedStr[1]);
            }
        }

        public override string ToString()
        {
            return x + "," + y;
        }
    }

    public struct AABB
    {
        public Vector3 x;
        public Vector3 y;

        public AABB(Vector3 x, Vector3 y)
        {
            this.x = x;
            this.y = y;
        }
    }
}

/*

--- MCBE 1.17 ---

onGround - 1E0
onGround2 - 1E4
stepHeight - 240
worldAge - 2B0
gamemode - 1E08
isFlying - 9C0
blocksTraveled_Ex - 250
blocksTraveled - 250 + 16
helditemCount - 228A
holdingItem - 2274
holdingItemId - 2280 (I think..?)
selectedHotbarId - 22F8
viewCreativeItems - 9D8
viewCreativeItemsSelectedCategory - 2370
entityType - 410
inInventory - 11E0
username - 920
gameDim - 370 18
positionX - 4D0
hitbox - 4D0 + 28
velocity - 50C
swingAn - 7A0
lookingEntityId - 10B8
inWater - 265
bodyRots - 148

 */