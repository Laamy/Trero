namespace Trero.ClientBase.VersionBase // Just about finished this part tbh
{
    internal class VersionClass // versions
    {
        public static IVersion[] versions =
        {
            // All 1.17 versions supported

            // MCBE 1.17
            new IVersion(new object[]
            {
                "MCBE-1.17.11",
                0x041457D8, 0x0, 0x20, 0xC8,
                0x1E0,
                0x1E4,
                0x240,
                0x2B0,
                0x1E08,
                0x9C0,
                0x250,
                0x250 + 16,
                0x228A,
                0x2274,
                0x0,
                0x22F8,
                0x9D8,
                0x2370,
                0x410,
                0x11E0,
                0x920,
                0x370, 0x18,
                0x4D0,
                0x4D0 + 28,
                0x50C,
                0x7A0,
                0x10B8,
                0x265,
                0x148,
                0x378,
                0x58, 0x68, // Full entity list size
                0x0, // 0x95F4
                0x988,
                0x98C,
                0x990,
                0x040A41F8, 0x814,
                0x04120400, 0x8, 0x48, 0xA0, 0x128
            }) // MCBE 1.17.11
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
            "chatBase", "chatBase+1", "chatBase+2", "chatBase+3", "chatBase+4"
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