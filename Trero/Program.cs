#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trero.ClientBase;
using Trero.ClientBase.KeyBase;
using Trero.ClientBase.UIBase;
using Trero.ClientBase.VersionBase;
using Trero.Modules;
using Debug = Trero.Modules.Debug;

#endregion

namespace Trero
{
    internal static class Program
    {
        public static bool quit;
        public static bool limiter;
        public static bool unlimiter;
        public static EventHandler<EventArgs> mainThread;
        public static readonly List<Module> Modules = new List<Module>();

        private static void Main(string[] args)
        {
            try
            {
                Process cProc = Process.GetProcessesByName("Minecraft.Windows")[0];
            }
            catch
            {
                try
                {
                    Process wProc = Process.GetProcessesByName("ApplicationFrameHost")[0];
                    wProc.Kill();
                }
                catch { }
            }

            Process.Start("minecraft://");

            MCM.openGame();
            MCM.openWindowHost();

            // ReSharper disable once ObjectCreationAsStatement
            new Keymap();

            Console.WriteLine("Trero v" + VersionClass.currentVersion.name);

            new Thread(() => { Application.Run(new Overlay()); }).Start(); // UI Application

            // Debug shit here
            Console.WriteLine(Game.keyInfo.ToString("X"));

            Console.WriteLine(@"Registering modules...");

            Modules.Add(new ClickGUI());
            Modules.Add(new Antibot());

            Modules.Add(new Debug());
            Modules.Add(new AirStuck());
            Modules.Add(new BulkFly());
            Modules.Add(new AboveAura());
            Modules.Add(new AirJump());
            Modules.Add(new TPAura());
            Modules.Add(new ClosestPlayerDisplay());
            Modules.Add(new PlayerDisplay());
            Modules.Add(new TriggerBot()); // no longer in accessable lp area
            Modules.Add(new Hitbox());
            Modules.Add(new FlickerExample());
            Modules.Add(new Phase());
            Modules.Add(new Noclip());
            Modules.Add(new NoYFly());
            Modules.Add(new PhaseDown());
            Modules.Add(new PhaseUp());
            Modules.Add(new Sexaura());
            Modules.Add(new Fly());
            Modules.Add(new Jetpack());
            Modules.Add(new Eject());
            Modules.Add(new Speed());
            Modules.Add(new Bhop());
            Modules.Add(new SlimeWall());
            Modules.Add(new LBSH());
            Modules.Add(new Gamemode());
            Modules.Add(new Teleport());
            Modules.Add(new Step());
            Modules.Add(new HighJump());
            Modules.Add(new InventoryMove());
            Modules.Add(new KillGame());
            Modules.Add(new Jesus());
            Modules.Add(new NoSwing());
            Modules.Add(new CreativeFly());
            Modules.Add(new PlayerTP());
            Modules.Add(new ClickTP());
            Modules.Add(new Glide());
            Modules.Add(new Killaura());
            Modules.Add(new AntiImmoblie());
            //Modules.Add(new Reach());
            Modules.Add(new Limiter()); // CPU saver
            Modules.Add(new Unlimiter()); // Remove safty ill make these a single module soon
            Modules.Add(new Friends());
            Modules.Add(new Nofriends());
            Modules.Add(new MineplexFly());
            Modules.Add(new LongJump());
            Modules.Add(new Zoom());
            Modules.Add(new AutoWalk());
            Modules.Add(new Rapeaura());
            Modules.Add(new HiveAntibot());
            Modules.Add(new StreamMode());
            Modules.Add(new Masturbator());
            Modules.Add(new Welcome());
            Modules.Add(new MineplexFlyv2());
            Modules.Add(new RainbowEffects());
            Modules.Add(new Velocity()); // Anti-KB
            //Modules.Add(new NoLagBack());
            Modules.Add(new RapidHit());
            Modules.Add(new RapidPlace());

            Console.WriteLine(@"Registered modules!");

            //Console.WriteLine("LookingEntityID Address: " + (Game.localPlayer + 0x0).ToString("X"));

            //modules.Add(new TestModule());

            // Recall (Teleportation)
            // Tower (Veloicty & getKey)

            // Note that these are all possible but might not be added/changed just ideas ill slowly filter through over time
            // also rather do things i can think of ways to actually do externally so dont ask for anything else if you 100% know its impossible :(
            // for yaammi to do list :penisve:
            // CubeCraftFly
            // FastUse (Haven't put much thought into this so idk if its possible externally probs is)
            // AutoFish (Seems pretty simple and wont need pointers tbh)
            // Commands
            // Aimbot
            // NoHurtCam
            // InventoryDisplay // need local player inventory proxey
            // Zoom - Turns out this is also in local player as FieldOfView so i still need fov pointer Oof
            // Fov (FovPointer needed for W2S)
            // WorldToScreen/W2S (GameFunc)
            // Rader (Already possible btw as we have entitylist) // 100% adding these W2S ones btw
            // Tracers (W2S Required)
            // Waypoints (W2S Required)
            // ArrowTracers (W2S Required)
            // Reach
            // FastWater
            // CustomTablist
            // Nuker
            // BlockFly (Cant do this without scaffold so...)
            // Im never doing fightbot so fuck up
            // BowAimbot
            // ChestAura
            // CompassDisplay // when i get around to it i want to add waypoints into this aswell
            // FullBright
            // Fix scaffold
            // Noknockback

            // TreroInternal - Modules List
            // 

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
            // nametags - if possible add armor and in hand display above their head

            Modules.Sort((c1, c2) => string.Compare(c2.name, c1.name, StringComparison.Ordinal)); // ABC Order

            // Keymap.keyEvent += keyParse;

            Console.WriteLine(@"--- Trero Terminal ---");
            Console.WriteLine(@"Welcome to the trero terminal");
            Console.WriteLine(@"");

            mainThread += moduleTick;

            Task.Run(() =>
            {
                while (quit == false)
                {
                    try // fixed any future errors here
                    {
                        if (limiter && !unlimiter)
                            Thread.Sleep(1);

                        if (!unlimiter)
                            Thread.Sleep(1);
                        Thread.Sleep(5);

                        mainThread.Invoke(null, new EventArgs());
                    }
                    catch
                    {
                    }
                }
            });

            /*RenderClass.CreateAction((g) =>
            {
                Thread.Sleep(1);

                var rect = MCM.getMinecraftRect();

                var cvE = new Placement();
                GetWindowPlacement(MCM.mcWinHandle,
                    ref cvE);
                var vE = 0;
                var vA = 0;
                if (cvE.showCmd == 3)
                {
                    vE = 8;
                    vA = 2;
                }

                PointF hgfs = new PointF(rect.Left + 9 + vA, rect.Top + 35 + vE);

                g.DrawString("Hello world!", font, Brushes.Blue, new PointF(0, 0)); // new PointF(0, 0)
            }, MCM.mcProcHandle);*/

            while (quit == false)
            {
                Console.ReadKey();
            }
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowPlacement(IntPtr hWnd, ref Placement lpwndpl);

        public static Font font = new Font("Arial", 16, FontStyle.Regular);

        private static void moduleTick(object sender, EventArgs e)
        {
            foreach (var mod in Modules.Where(mod => mod.enabled))
                mod.OnTick();
        }

        /*

Game.isLookingAtBlock = 0;
Game.SideSelect = 1;
Game.SelectedBlock = Base.iVec3((int)Game.position.x, (int)Game.position.y - 1, (int)Game.position.z);

Mouse.MouseEvent(Mouse.MouseEventFlags.MOUSEEVENTF_RIGHTDOWN);

*/
    }

    public struct Placement
    {
        public int length;
        public int flags;
        public int showCmd;
        public Point ptMinPosition;
        public Point ptMaxPosition;
        public Rectangle rcNormalPosition;
    }
}