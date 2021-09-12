
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trero.ClientBase;
using Trero.ClientBase.EntityBase;
using Trero.ClientBase.KeyBase;
using Trero.ClientBase.UIBase;
using Trero.ClientBase.VersionBase;
using Trero.Modules;
using Debug = Trero.Modules.Debug;
using Module = Trero.Modules.Module;

namespace Trero
{
    class Program
    {
        public static bool quit = false;
        public static List<Module> modules = new List<Module>();

        static void Main(string[] args)
        {
            MCM.openGame();
            MCM.openWindowHost();

            new Keymap();

            new Thread(() => { Application.Run(new Overlay()); }).Start(); // UI Application

            modules.Add(new ClickGUI()); // i enable these after displaying them via overlay.cs
            modules.Add(new Antibot());

            modules.Add(new Debug());
            modules.Add(new AirStuck());
            modules.Add(new BulkFly());
            modules.Add(new AboveAura());
            modules.Add(new AirJump());
            modules.Add(new TPAura());
            modules.Add(new ClosestPlayerDisplay());
            modules.Add(new PlayerDisplay());
            modules.Add(new TriggerBot());
            modules.Add(new Hitbox());
            modules.Add(new FlickerExample());
            modules.Add(new Phase());
            modules.Add(new Noclip());
            modules.Add(new NoYFly());
            modules.Add(new PhaseDown());
            modules.Add(new PhaseUp());
            modules.Add(new Sexaura());
            modules.Add(new Fly());
            modules.Add(new Jetpack());
            modules.Add(new Eject());
            modules.Add(new Speed());
            modules.Add(new Bhop());
            modules.Add(new SlimeWall());
            modules.Add(new LBSH());
            modules.Add(new Gamemode());
            modules.Add(new Teleport());
            modules.Add(new Step());
            modules.Add(new HighJump());
            modules.Add(new InventoryMove());
            modules.Add(new KillGame());
            modules.Add(new Jesus());
            modules.Add(new NoSwing());
            modules.Add(new CreativeFly());
            modules.Add(new PlayerTP());
            modules.Add(new ClickTP());
            modules.Add(new Glide());
            //modules.Add(new TestModule());

            // Recall (Teleportation)
            // Tower (Veloicty & getKey)

            // Note that these are all possible but might not be added/changed just ideas ill slowly filter through over time
            // also rather do things i can think of ways to actually do externally so dont ask for anything else if you 100% know its impossible :(
            // for yaammi to do list :penisve:
            // CubeCraftFly
            // MineplexFly - probs dont need this tbh
            // Killaura (Some kind of isLookingAtEntityId edit else Aimbot/TriggerBot
            // FastUse (Haven't put much thought into this so idk if its possible externally probs is)
            // AutoFish (Seems pretty simple and wont need pointers tbh)
            // Commands
            // Aimbot
            // NoHurtCam
            // InventoryDisplay // need local player inventory proxey
            // Zoom (FovPointer needed for W2S)
            // WorldToScreen/W2S (GameFunc)
            // Rader (Already possible btw as we have entitylist) // 100% adding these W2S ones btw
            // Tracers (W2S Required)
            // Waypoints (W2S Required)
            // ArrowTracers (W2S Required)
            // AutoWalk
            // Reach
            // FastWater
            // CustomTablist
            // Nuker
            // Antibot
            // BlockFly (Cant do this without scaffold so...)
            // Im never doing fightbot so fuck up
            // BowAimbot
            // ChestAura
            // CompassDisplay // when i get around to it i want to add waypoints into this aswell
            // FullBright
            // NoFriends
            // Fix scaffold
            // Noknockback

            // TODO: Commands -- i think ill do all the commands in about aweek if i get a good idea on where to put them
            // tp (x) (y) (z)
            // gm (registeryId)
            // eject
            // bind (module) (key)
            // unbind (module)
            // toggle (module)
            // friend (add/remove) (plr)
            // draw (module) (true/false) - Show or hide module from array list
            // coords
            // vclip (number)
            // waypoint (add/remove/list/tp) (waypointName)
            // rename (module) (name)
            // modules
            // block (plr) - ill replace there messages in chat to [Message deleted] x1(etc) using chat pointer
            // unblock
            // durability

            modules[0].onDisable();

            modules.Sort((c1, c2) => c2.name.CompareTo(c1.name)); // ABC Order

            VersionClass.setVersion(VersionClass.versions[0]);

            // Keymap.keyEvent += keyParse;

            Console.WriteLine("--- Trero Terminal ---");
            Console.WriteLine("Welcome to the trero terminal");
            Console.WriteLine("");
            Console.WriteLine("--- Trero Keybinds ---");
            Console.WriteLine("R - ClampJet");
            Console.WriteLine("P - Terminate Process");
            Console.WriteLine("Y - Hitboxes");
            Console.WriteLine("C - PhaseUp(ServerBypass)");
            Console.WriteLine("V - PhaseDown(ServerBypass)");

            // Console.WriteLine(Game.level.ToString("X"));

            while (quit == false) // freeze
            {
                if (Overlay.handle == null) return;

                //Thread.Sleep(1);
                foreach (Module mod in modules)
                    if (mod.enabled)
                        mod.onTick();
            }
        }
        /*
         
        Game.isLookingAtBlock = 0;
        Game.SideSelect = 1;
        Game.SelectedBlock = Base.iVec3((int)Game.position.x, (int)Game.position.y - 1, (int)Game.position.z);

        Mouse.MouseEvent(Mouse.MouseEventFlags.MOUSEEVENTF_RIGHTDOWN);

        */
    }
}
