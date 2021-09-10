using System;
using System.Collections.Generic;
using System.Linq;
using Trero.ClientBase.EntityBase;
using Trero.ClientBase.VersionBase;

namespace Trero.ClientBase
{
    class Game
    {
        public static ulong clientInstance
        {
            get
            {
                return MCM.baseEvaluatePointer(HexHandler.toULong(VersionClass.currentVersion.sdk[0]), new ulong[] {
                    VersionClass.getData("baseOffset+1"),
                    VersionClass.getData("baseOffset+2")
                });
            }
        } // clientInstance
        public static ulong localPlayer
        {
            get
            {
                return MCM.evaluatePointer(clientInstance, new ulong[] {
                    VersionClass.getData("baseOffset+3"),
                    0x0
                });
            }
        } // localPlayer
        public static ulong level
        {
            get
            {
                return MCM.readInt64(localPlayer + VersionClass.getData("level"));
            }
        } // level
        public static ulong EntityListStart
        {
            get
            {
                return MCM.readInt64(level + VersionClass.getData("entitylist+1"));
            }
        } // entityliststart
        public static ulong EntityListEnd
        {
            get
            {
                return MCM.readInt64(level + VersionClass.getData("entitylist+2"));
            }
        } // entitylistend

        public static bool isNull
        {
            get
            {
                if (screenData.StartsWith("toast_screen"))
                    return false;
                return true;
            }
        } // isValid

        public static void teleport(AABB advancedAxis)
        {
            MCM.writeFloat(localPlayer + (ulong)VersionClass.getData("positionX"), advancedAxis.x.x);
            MCM.writeFloat(localPlayer + (ulong)VersionClass.getData("positionX") + 4, advancedAxis.x.y);
            MCM.writeFloat(localPlayer + (ulong)VersionClass.getData("positionX") + 8, advancedAxis.x.z);

            MCM.writeFloat(localPlayer + (ulong)VersionClass.getData("positionX") + 12, advancedAxis.y.x);
            MCM.writeFloat(localPlayer + (ulong)VersionClass.getData("positionX") + 16, advancedAxis.y.y);
            MCM.writeFloat(localPlayer + (ulong)VersionClass.getData("positionX") + 20, advancedAxis.y.z);
        } // Teleportation
        public static void teleport(float x, float y, float z)
        {
            teleport(new AABB(Base.Vec3(x, y, z), Base.Vec3(x + .6f, y + 1.8f, z + .6f)));
        } // Teleportation
        public static void teleport(Vector3 _Vec3)
        {
            teleport(_Vec3.x, _Vec3.y, _Vec3.z);
        } // Teleportation

        public static Vector3 position
        {
            get
            {
                Vector3 vec = Base.Vec3();

                vec.x = MCM.readFloat(localPlayer + VersionClass.getData("positionX"));
                vec.y = MCM.readFloat(localPlayer + VersionClass.getData("positionX") + 4);
                vec.z = MCM.readFloat(localPlayer + VersionClass.getData("positionX") + 8);

                return vec;
            }
            set => teleport(value);
        } // Position
        public static Vector3 lookingAtPosition
        {
            get
            {
                Vector3 vec = Base.Vec3();

                vec.x = MCM.readFloat(localPlayer - VersionClass.getData("lookingAtBlock"));
                vec.y = MCM.readFloat(localPlayer - VersionClass.getData("lookingAtBlock") + 4);
                vec.z = MCM.readFloat(localPlayer - VersionClass.getData("lookingAtBlock") + 8);

                return vec;
            }
        } // Position
        public static int gamemode
        {
            get
            {
                int gamemode = -1;
                gamemode = (int)(MCM.readInt64(localPlayer + VersionClass.getData("positionX")) / 4294967296);
                return gamemode;
            }
            set => MCM.writeInt64(localPlayer + VersionClass.getData("positionX"), (ulong)(value * 4294967296));
        } // Gamemode
        public static int isFalling
        {
            get
            {
                int value = -1;
                value = (int)(MCM.readInt64(localPlayer + VersionClass.getData("JumpForBHop")));
                return value;
            }
            set => MCM.writeInt64(localPlayer + VersionClass.getData("positionX"), (ulong)(value * 4294967296));
        }
        public static Vector3 velocity
        {
            get
            {
                Vector3 vec = Base.Vec3();

                vec.x = MCM.readFloat(localPlayer + VersionClass.getData("velocity"));
                vec.y = MCM.readFloat(localPlayer + VersionClass.getData("velocity") + 4);
                vec.z = MCM.readFloat(localPlayer + VersionClass.getData("velocity") + 8);

                return vec;
            }
            set
            {
                MCM.writeFloat(localPlayer + VersionClass.getData("velocity"), value.x);
                MCM.writeFloat(localPlayer + VersionClass.getData("velocity") + 4, value.y);
                MCM.writeFloat(localPlayer + VersionClass.getData("velocity") + 8, value.z);
            }
        } // Velocity
        public static Vector2 rotation
        {
            get
            {
                Vector2 vec = Base.Vec2();

                vec.x = MCM.readFloat(localPlayer + VersionClass.getData("bodyRots"));
                vec.y = MCM.readFloat(localPlayer + VersionClass.getData("bodyRots") + 4);

                return vec;
            }
        } // Rotations
        public static Vector2 compassRotations
        {
            get
            {
                Vector2 vec = rotation;

                vec.y = (new float[] { 1, 2, 3, 4 }).OrderBy(v => Math.Abs((long)v - ((vec.y + 180f) / 90f))).First();
                vec.x = (new float[] { 1, 2, 3, 4 }).OrderBy(v => Math.Abs((long)v - ((vec.x + 90f) / 180f))).First();

                return vec;
            }
        } // CompassRotations
        public static bool onGround
        {
            get => MCM.readInt(localPlayer + VersionClass.getData("onGround")) != 0;
            set
            {
                if (value) MCM.writeInt(localPlayer + VersionClass.getData("onGround"), 16777473);
                else MCM.writeInt(localPlayer + VersionClass.getData("onGround"), 0);
            }
        } // onGround
        public static bool isLookingAtEntity
        {
            get => MCM.readInt(localPlayer + VersionClass.getData("lookingEntityId")) != -1;
        } // lookingEntity
        public static int lookingEntityId
        {
            get => MCM.readInt(localPlayer + VersionClass.getData("lookingEntityId"));
            set => MCM.writeInt(localPlayer + VersionClass.getData("lookingEntityId"), value);
        }
        public static bool onGround2
        {
            get => MCM.readInt(localPlayer + VersionClass.getData("onGround2")) != 0;
        } // onGround2
        public static float stepHeight
        {
            get => MCM.readFloat(localPlayer + VersionClass.getData("stepHeight"));
            set => MCM.writeFloat(localPlayer + VersionClass.getData("stepHeight"), value);
        } // stepHeight
        public static Vector3 dirVect(float x, float y)
        {
            Vector3 tempVec = Base.Vec3(); // create empty vector

            tempVec.x = (float)Math.Cos(x) * (float)Math.Cos(y);
            tempVec.y = (float)Math.Sin(y);
            tempVec.z = (float)Math.Sin(x) * (float)Math.Cos(y);

            return tempVec;
        } // Directional Vector
        public static Vector3 lVector
        {
            get
            {
                Vector3 tempVec;

                float cYaw = rotation.y + 89.9f * (float)Math.PI / 178f;
                float cPitch = rotation.x * (float)Math.PI / 178f;

                tempVec = dirVect(cYaw, cPitch);

                return tempVec;
            }
            // set { }
        } // Looking Vector
        public static string username
        {
            get => MCM.readString(localPlayer + VersionClass.getData("username"), 32);
        } // Username
        public static string screenData
        {
            get => MCM.readString(MCM.baseEvaluatePointer(HexHandler.toULong(VersionClass.getData("screenT+1")), new ulong[] { VersionClass.getData("screenT+2") }), 128);
        } // Username
        public static string type
        {
            get => MCM.readString(localPlayer + VersionClass.getData("entityType"), 32);
        } // Type
        public int heldItemCount
        {
            get
            {
                int value = -1;
                value = (int)(MCM.readInt(localPlayer + VersionClass.getData("helditemCount")));
                return value;
            }
        } // Held item count
        public int holdItem
        {
            get
            {
                int value = -1;
                value = (int)(MCM.readInt(localPlayer + VersionClass.getData("holdItem")));
                return value;
            }
        } // Are you holding an item?
        public int holdItemId
        {
            get
            {
                int value = -1;
                value = (int)(MCM.readInt(localPlayer + VersionClass.getData("holdItemId")));
                return value;
            }
        } // Whats the id of the item your holding?
        public int swingAn
        {
            get
            {
                int value = -1;
                value = (int)(MCM.readInt(localPlayer + VersionClass.getData("swingAn")));
                return value;
            }
            set => MCM.writeInt(localPlayer + VersionClass.getData("swingAn"), value);
        } // Current swing animation point (0-4?)
        public int worldAge
        {
            get
            {
                int value = -1;
                value = (MCM.readInt(localPlayer + VersionClass.getData("worldAge")));
                return value;
            }
            set => MCM.writeInt(localPlayer + VersionClass.getData("worldAge"), value);
        } // Current world age

        // Level
        public static int isLookingAtBlock
        {
            get
            {
                return MCM.readInt(level + VersionClass.getData("LookingAtBlock"));
            }
            set => MCM.writeInt(level + VersionClass.getData("LookingAtBlock"), value);
        } // isLookingAtBlock
        public static iVector3 SelectedBlock
        {
            get
            {
                iVector3 vec = Base.iVec3();

                vec.x = MCM.readInt(level + VersionClass.getData("SelectedBlock"));
                vec.y = MCM.readInt(level + VersionClass.getData("SelectedBlock") + 4);
                vec.z = MCM.readInt(level + VersionClass.getData("SelectedBlock") + 8);

                return vec;
            }
            set
            {
                MCM.writeInt(level + VersionClass.getData("SelectedBlock"), value.x);
                MCM.writeInt(level + VersionClass.getData("SelectedBlock") + 4, value.y);
                MCM.writeInt(level + VersionClass.getData("SelectedBlock") + 8, value.z);
            }
        } // SelectedBlock
        public static int SideSelect
        {
            get
            {
                return MCM.readInt(level + VersionClass.getData("SideSelect"));
            }
            set => MCM.writeInt(level + VersionClass.getData("SideSelect"), value);
        } // SideSelect

        // EntityList
        public static List<Actor> getTypeEntities(string type)
        {
            List<Actor> entityList = new List<Actor>();
            for (ulong i = EntityListStart; i < EntityListEnd; i += 0x8)
            {
                if (i == EntityListStart) continue;
                Actor entity = new Actor(MCM.readInt64(i));
                if (entity.type == type && entity.username.Length > 3)
                    entityList.Add(entity);
            }
            return entityList;
        }
        public static List<Actor> getTypeEntities_Antibot(string type, string[] antibotSettings)
        {
            List<Actor> entityList = new List<Actor>();
            for (ulong i = EntityListStart; i < EntityListEnd; i += 0x8)
            {
                if (i == EntityListStart) continue;
                Actor entity = new Actor(MCM.readInt64(i));
                if (entity.type == type && entity.username.Length > 3) // Antibot so we dont hit npcs
                {
                    bool allow = true;
                    foreach (string str in antibotSettings)
                    {
                        if (entity.username.ToLower().Contains(str.ToLower()))
                            allow = false;
                    }
                    if (allow)
                        entityList.Add(entity);
                }
            }
            return entityList;
        }
        public static List<Actor> getEntites()
        {
            List<Actor> entityList = new List<Actor>();
            for (ulong i = EntityListStart; i < EntityListEnd; i += 0x8)
            {
                if (i == EntityListStart) continue;
                Actor entity = new Actor(MCM.readInt64(i));
                entityList.Add(entity);
            }
            return entityList;
        }
        public static List<Actor> parseEntities(List<Actor> list)
        {
            List<Actor> validEnts = new List<Actor>();
            char[] validCharacters = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm1234567890 §".ToCharArray(); // § because some servers change usernames
            foreach (var ent in list)
            {
                bool valid = true;
                foreach (char chr in ent.username.Substring(0,5).ToCharArray())
                {
                    bool validChr = false;
                    foreach (char vChr in validCharacters)
                    {
                        if (chr == vChr)
                            validChr = true;
                    }
                    if (!validChr)
                        valid = false;
                }
                if (valid)
                    validEnts.Add(ent);
            }
            return validEnts;
        }
        public static Actor getClosestEntity()
        {
            Actor vEntity = null;
            List<Actor> ents = Game.getEntites();
            ents.ForEach((Actor ent) => {
                if (vEntity == null)
                    vEntity = ent;
                else
                {
                    float dis1 = position.Distance(ent.position);
                    float dis2 = position.Distance(vEntity.position);

                    if (dis1 < dis2)
                        vEntity = ent;
                }
            });
            return vEntity;
        }
        public static Actor getClosestTypeEntity(string Type)
        {
            Actor vEntity = null;
            List<Actor> ents = Game.getTypeEntities(Type);
            ents.ForEach((Actor ent) => {
                if (vEntity == null)
                    vEntity = ent;
                else
                {
                    float dis1 = position.Distance(ent.position);
                    float dis2 = position.Distance(vEntity.position);

                    if (dis1 < dis2)
                        vEntity = ent;
                }
            });
            return vEntity;
        }
        public static List<Actor> getPlayers() => parseEntities(getTypeEntities("player"));
        public static Actor getClosestPlayer()
        {
            Actor vEntity = null;
            List<Actor> ents = parseEntities(getPlayers());
            ents.ForEach((Actor ent) => {
                if (vEntity == null)
                    vEntity = ent;
                else
                {
                    float dis1 = position.Distance(ent.position);
                    float dis2 = position.Distance(vEntity.position);

                    if (dis1 < dis2)
                        vEntity = ent;
                }
            });
            return vEntity;
        }
    }

    // Struct Defines
    public class GamemodeRegistery // used for .gm (Gamemode)
    {
        private List<List<string>> registery = new List<List<string>> {
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

        public GamemodeRegistery(out List<List<string>> list) => list = registery;
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
                string[] parsedStr = position.Replace(" ", "").Split(',');
                this.x = Convert.ToSingle(parsedStr[0]);
                this.y = Convert.ToSingle(parsedStr[1]);
                this.z = Convert.ToSingle(parsedStr[2]);
            }
            catch
            {
                string[] parsedStr = position.Replace(" ", "").Split(',');
                this.x = HexHandler.toLong(parsedStr[0]);
                this.y = HexHandler.toLong(parsedStr[1]);
                this.z = HexHandler.toLong(parsedStr[2]);
            }
        }
        public float DistanceTo(Vector3 _Vec3)
        {
            float diff_x = x - _Vec3.x, diff_y = y - _Vec3.y, diff_z = z - _Vec3.z;
            float output = (float)Math.Sqrt(diff_x * diff_x + diff_y * diff_y + diff_z * diff_z);
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
                string[] parsedStr = position.Replace(" ", "").Split(',');
                this.x = Convert.ToSingle(parsedStr[0]);
                this.y = Convert.ToSingle(parsedStr[1]);
            }
            catch
            {
                string[] parsedStr = position.Replace(" ", "").Split(',');
                this.x = HexHandler.toLong(parsedStr[0]);
                this.y = HexHandler.toLong(parsedStr[1]);
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
