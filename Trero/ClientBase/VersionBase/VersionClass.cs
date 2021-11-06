using System.Collections.Generic;
using System.IO;

namespace Trero.ClientBase.VersionBase // Just about finished this part tbh
{
    internal class VersionClass // versions
    {
        public static void init()
        {
            versions.Add(new IVersion(new object[] {
                    "MCBE-1.17.41",
                    0x041FC2A0, 0x0, 0x50, 0x138, // LP > 178 > 40 > 10
                    0x1D8, // onground
                    0x1DC, // onground 2
                    0x238, // stepHeight
                    0x2A8, // worldAge
                    0x1D84, // gamemode
                    0x980, // isFlying
                    0x240, // blocksTraveled_Ex
                    0x258, // blocksTraveled
                    0x2370, // helditemCount
                    0x236A, // holdingItem
                    0x0, // holdingItemId
                    0x023D8, // selectedHotbarId
                    0x0, // viewCreativeItems
                    0x0, // viewCreativeItemsSelectedCategory
                    0x400, // entityType
                    0x1148, // inInventory
                    0x8E0, // username
                    0x360, 0x0, // gameDim
                    0x4C0, // position
                    0x0, // hitbox - 0x4C0 + 24
                    0x4F8, // veloicty
                    0x7C8, // swingAn
                    0x0, // lookingEntityId
                    0x25D, // inWater
                    0x138, // bodyRots
                    0x370, // Level - 0x370
                    0xA8, // entitylist+1 - 0x50
                    0xB0, // entitylist+2 (BROKEN ON 1.17.30 once again!) - 0x68
                    0xA28, // lookingAtBlock
                    0xA30, // SelectedBlock Position
                    0xA9C, // SideSelect
                    0x0, // ScreenT+1
                    0x0, // ScreenT+2
                    0x0, // Chatbase shit
                    0x0, // Chatbase shit
                    0x0, // Chatbase shit
                    0x0, // Chatbase shit
                    0x0, // Chatbase shit
                    0x1058, // fieldOfView
                    0x178, // EffectsClass
                    0x40, // EffectsSubClass
                    0x10, // EffectsColor
                    0x490, // SpeedClass
                    0x18, // SpeedSubClass
                    0x2C0, // SpeedSubSubClass
                    0x9C, // SpeedValue
                    0x04171058, 0x490, 0x2A0, 0x8, // vKeyInfo
                    0x4B, // inMenu
                    0x50, // Hitting
                    0x51, // Placing
                    0x52, // Picking
                    0x138, // mouseX
                    0x13A, // mouseY
                    0x13E, // eKeymap
                    0x1D9, // onGround3 
                    0x2C9, // isInLava
                    0x1DA, // walkingIntoBlock
                    0x214C, // exactPos
                    0xB0, 0xD0, // timer
                    0xD0 // LoopbackSender
                }));
        }
        public static List<IVersion> versions = new List<IVersion>();

        public static IVersion versionStruct = new IVersion(new object[]
        {
            "version",
            "baseOffset", "baseOffset+1", "baseOffset+2", "baseOffset+3", // BaseOffsets
            "onGround",
            "onGround2",
            "stepHeight",
            "worldAge",
            "gamemode",
            "isFlying",
            "blocksTraveled_Ex",
            "blocksTraveled",
            "helditemCount",
            "holdingItem",
            "holdingItemId",
            "selectedHotbarId",
            "viewCreativeItems",
            "viewCreativeItemsSelectedCategory",
            "entityType",
            "inInventory",
            "username",
            "gameDim", "gameDim+1",
            "positionX",
            "hitbox",
            "velocity",
            "swingAn",
            "lookingEntityId",
            "inWater",
            "bodyRots",
            "level",
            "entitylist+1",
            "entitylist+2",
            "lookingAtBlock", // why is this defiend two times i wonder...
            "SelectedBlock",
            "SideSelect",
            "screenT+1", "screenT+2",
            "chatBase", "chatBase+1", "chatBase+2", "chatBase+3", "chatBase+4",
            "fieldOfView",
            "EffectsClass+1",
            "EffectsClass+2",
            "EffectsColor",
            "SpeedClass+1",
            "SpeedClass+2",
            "SpeedClass+3",
            "SpeedValue",
            "gameMap+1",
            "gameMap+2",
            "gameMap+3",
            "gameMap+4",
            "inMenu",
            "Hitting",
            "Placing",
            "Picking",
            "mouseX",
            "mouseY",
            "eKeymap",
            "onGround3",
            "isInLava",
            "walkingIntoBlock",
            "exactPos",
            "timer1",
            "timer2",
            "loopbackSender",
        });

        private static IVersion _cv;

        public static IVersion currentVersion => _cv ?? (_cv = versions[0]);

        public static ulong GetData(string data)
        {
            for (var i = 0; i < versionStruct.sdk.Length; ++i)
                if (versionStruct.sdk[i].ToString() == data)
                    return HexHandler.ToULong(currentVersion.sdk[i]);
            return 0x0;
        }

        public static void setVersion(IVersion version)
        {
            _cv = version;
        }
    }
}
