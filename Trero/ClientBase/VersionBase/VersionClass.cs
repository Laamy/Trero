namespace Trero.ClientBase.VersionBase // Just about finished this part tbh
{
    internal class VersionClass // versions
    {
        public static IVersion[] versions =
        {
            // MCBE 1.17.30
            new IVersion(new object[]
            {
                "MCBE-1.17.40",
                0x04208438, 0x0, 0x18, 0xB8, // LP > 178 > 40 > 10
                0x1D8, // onground
                0x1DC, // onground 2
                0x238, // stepHeight
                0x2A8, // worldAge
                0x1E6C, // gamemode
                0x9A0, // isFlying
                0x0, // blocksTraveled_Ex
                0x0, // blocksTraveled
                0x2370, // helditemCount
                0x0, // holdingItem
                0x0, // holdingItemId
                0x0, // selectedHotbarId
                0x0, // viewCreativeItems
                0x0, // viewCreativeItemsSelectedCategory
                0x400, // entityType
                0x0, // inInventory
                0x900, // username
                0x360, 0x0, // gameDim
                0x4C0, // position
                0x0, // hitbox - 0x4C0 + 24
                0x4F8, // veloicty
                0x7C0, // swingAn
                0x0, // lookingEntityId
                0x25D, // inWater
                0x138, // bodyRots
                0x360, // Level - 0x370
                0xA8, // entitylist+1 - 0x50
                0xB0, // entitylist+2 (BROKEN ON 1.17.30 once again!) - 0x68
                0x0, // lookingAtBlock
                0x0, // SelectedBlock
                0x0, // LookingAtBlock
                0x0, // SideSelect
                0x0, // ScreenT+1
                0x0, // ScreenT+2
                0x0, // Chatbase shit
                0x0, // Chatbase shit
                0x0, // Chatbase shit
                0x0, // Chatbase shit
                0x0, // Chatbase shit
                0x1140, // fieldOfView
                0x178, // EffectsClass
                0x40, // EffectsSubClass
                0x10, // EffectsColor
                0x490, // SpeedClass
                0x18, // SpeedSubClass
                0x2C0, // SpeedSubSubClass
                0x9C, // SpeedValue
                0x04170078, 0x490, 0x2A0, 0x8, // vKeyInfo
                0x4B, // inMenu
                0x50, // Hitting
                0x51, // Placing
                0x52, // Picking
                0x138, // mouseX
                0x13A, // mouseY
                0x13E, // eKeymap
                0x1D9, // onground2
            }),
        };

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
            "LookingAtBlock",
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
            "onGround2",
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