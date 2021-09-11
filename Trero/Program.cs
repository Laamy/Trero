
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

        public static bool scaffolding = false;

        public static List<Module> modules = new List<Module>();

        static void Main(string[] args)
        {
            MCM.openGame();
            MCM.openWindowHost();

            new Keymap();

            new Thread(() => { Application.Run(new Overlay()); }).Start(); // UI Application

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
            modules.Add(new ClickGUI());
            modules.Add(new Phase());
            modules.Add(new Noclip());
            modules.Add(new NoYFly());
            modules.Add(new PhaseDown());
            modules.Add(new PhaseUp());
            modules.Add(new KillGame());
            modules.Add(new Sexaura());
            modules.Add(new Fly());

            // Step (stepHeight)
            // PlayerTp (Teleportation)
            // ClickTP (Teleportation)
            // CreativeFly (isFlying)
            // HighJump (Velocity)
            // Jesus (inWater & velocity)
            // NoSwing (swingAn)
            // Recall (Teleportation)
            // Tower (Veloicty & getKey)
            // TpBypass (Hive maybe a little to much for you rn though to learn how that works so ignore this if you want 2)


            // for yaammi to do list :penisve:
            // Jetpack
            // Glide
            // CubeCraftFly
            // MineplexFly
            // Bhop
            // Speed
            // InventoryMove
            // Aimbot
            // AutoWalk
            // Gamemode
            // Reach
            // Antibot
            // BowAimbot
            // ChestAura
            // CompassDisplay
            // FullBright
            // Zoom (FovPointer needed for world2screen)
            // WorldToScreen (GameFunc)
            // NoFriends
            // Commands
            // NoHurtCam
            // Rader (Already possible btw as we have entitylist)
            // Fix scaffold
            // Spider
            // Tracers (W2S Required)
            // ArrowTracers (W2S Required)
            // Noknockback
            // Waypoints (W2S Required)
            // 

            // TODO: Commands
            // tp
            // gm
            // eject
            // bind
            // toggle

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

            while (true) // freeze
            {
                Thread.Sleep(1);
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
