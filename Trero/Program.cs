#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using Trero.ClientBase;
using Trero.ClientBase.KeyBase;
using Trero.ClientBase.Memory;
using Trero.ClientBase.UIBase;
using Trero.ClientBase.VersionBase;
using Trero.Modules;
using Debug = Trero.Modules.Debug;
using Timer = Trero.Modules.Timer;

#endregion

namespace Trero
{
    internal static class Program
    {
        public static bool quit;
        public static bool limiter;
        public static bool unlimiter;
        public static EventHandler<EventArgs> mainThread;
        public static EventHandler<EventArgs> moduleToggled;
        public static List<Module> Modules = new List<Module>();

        private static bool debugmode = true;

        private static void Main(string[] args)
        {
            VersionClass.init();
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

            Console.WriteLine(" _____                 ");
            Console.WriteLine("|_   _|___ ___ ___ ___ ");
            Console.WriteLine("  | | |  _| -_|  _| . |");
            Console.WriteLine("  |_| |_| |___|_| |___|");
            Console.WriteLine("Trero v" + VersionClass.currentVersion.name);

            Task.Run(() => Application.Run(new Overlay())); // UI Application


            Console.WriteLine(@"LoopbackSenderAddr: " + Minecraft.CI.loopbackSender.addr.ToString("X"));
            Console.WriteLine(@"NoPacketAddr: " + Minecraft.CI.loopbackSender.PacketSenderAddr.ToString("X"));

            Console.WriteLine(@"Registering modules...");

            Modules.Add(new ClickGUI());
            Modules.Add(new Antibot());

            Modules.Add(new Debug());
            Modules.Add(new AirStuck());
            Modules.Add(new BulkFly());
            //Modules.Add(new AboveAura());
            Modules.Add(new AirJump());
            //Modules.Add(new TPAura());
            //Modules.Add(new ClosestPlayerDisplay());
            //Modules.Add(new PlayerDisplay());
            //Modules.Add(new Hitbox());
            Modules.Add(new FlickerExample());
            Modules.Add(new Phase());
            Modules.Add(new Noclip());
            Modules.Add(new NoYFly());
            Modules.Add(new PhaseDown());
            Modules.Add(new PhaseUp());
            //Modules.Add(new Sexaura());
            Modules.Add(new Fly());
            Modules.Add(new Jetpack());
            Modules.Add(new Eject());
            Modules.Add(new Speed());
            Modules.Add(new Bhop());
            Modules.Add(new Spider());
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
            //Modules.Add(new PlayerTP());
            //Modules.Add(new ClickTP());
            Modules.Add(new Glide());
            //Modules.Add(new Killaura());
            Modules.Add(new Limiter()); // CPU saver
            Modules.Add(new Unlimiter());
            //Modules.Add(new Friends());
            //Modules.Add(new Nofriends());
            Modules.Add(new MineplexFly());
            Modules.Add(new LongJump());
            Modules.Add(new Zoom());
            Modules.Add(new AutoWalk());
            //Modules.Add(new Rapeaura());
            Modules.Add(new HiveAntibot());
            Modules.Add(new StreamMode());
            Modules.Add(new Masturbator());
            Modules.Add(new Welcome());
            Modules.Add(new MineplexFlyv2());
            Modules.Add(new RainbowEffects());
            Modules.Add(new RapidHit());
            Modules.Add(new RapidPlace());
            Modules.Add(new OGMFly());
            Modules.Add(new FastFly());
            Modules.Add(new Velocity()); // Anti-KB
            Modules.Add(new Disabler());
            Modules.Add(new InPvPTower());
            Modules.Add(new FastWater());
            Modules.Add(new FixHitbox());
            Modules.Add(new MineplexStep());
            Modules.Add(new HiveBhop());
            Modules.Add(new Reach());
            Modules.Add(new HiveFly());
            Modules.Add(new AntiImmoblie());
            Modules.Add(new ArrayList());
            Modules.Add(new Jump());
            Modules.Add(new Watermark());
            Modules.Add(new CoordsHud());
            Modules.Add(new NoLagBack());
            Modules.Add(new Scaffold());
            Modules.Add(new CFreecam());
            Modules.Add(new ElytraFlight());
            Modules.Add(new Tower());
            Modules.Add(new Freelook());
            Modules.Add(new SaveConfig());
            Modules.Add(new LoadConfig());
            Modules.Add(new Timer());
            Modules.Add(new NoPacket());
            Modules.Add(new Blink());
            Modules.Add(new NoHurtcam());
            Modules.Add(new FastFall());
            Modules.Add(new ReverseStep());
            //Modules.Add(new SurroundTest());

            Console.WriteLine(@"Registered modules!");

            switch (debugmode)
            {
                case true:
                    Console.WriteLine(@"Registering DEBUG_MODULES...");

                    //Modules.Add(new TriggerBot());

                    Console.WriteLine(@"Registered DEBUG_MODULES!");
                    break;
            }

            Modules.Sort((c1, c2) => string.Compare(c2.name, c1.name, StringComparison.Ordinal));

            Console.WriteLine(@"Checking for config...");
            if (File.Exists("config.json"))
            {
                Console.WriteLine(@"Found config!");

                var config = new JavaScriptSerializer().Deserialize<ConfigIO>(File.ReadAllText("config.json"));

                foreach (var c in Modules)
                {
                    int index = 0;
                    foreach (var a in config.moduleNames)
                    {
                        if (c.name == a)
                        {
                            c.enabled = config.enableStates[index];
                            int index2 = 0;
                            foreach (var b in c.bypasses)
                            {
                                c.bypasses[index2].curIndex = config.moduleBypasses[index][index2];
                                index2++;
                            }
                            c.keybind = config.moduleKeybinds[index];
                        }
                        index++;
                    }
                }
            }
            else Console.WriteLine(@"No config found, ignoring...");

            //Console.WriteLine(@"--- Trero Terminal ---");
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

                        //Console.WriteLine(Game.bodyRots);
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

            // TODO: Commands -- i think ill do all the commands in about aweek if i get a good idea on where to put them
            // eject
            // bind (module) (key)
            // unbind (module)
            // toggle (module)
            // friend (add/remove) (plr)
            // draw (module) (true/false) - Show or hide module from array list
            // waypoint (add/remove/list/tp) (waypointName)
            // rename (module) (name)
            // block (plr) - ill replace there messages in chat to [Message deleted] x1(etc) using chat pointer
            // unblock
            // durability
            // nametags - if possible add armor and in hand display above their head

            Console.WriteLine(".help for help!");

            while (quit == false)
            {
                string cmd = Console.ReadLine();
                if (cmd.StartsWith("."))
                {
                    string[] arguments = "0,0,0,0,0,0,0".Split(',');
                    string command = cmd.Replace(".", "");

                    try
                    {
                        arguments = cmd.Split(' ');
                        command = arguments[0].Replace(".", "");
                        arguments = arguments.Skip(1).ToArray();
                    }
                    catch
                    {
                    }

                    switch (command)
                    {
                        case "help":
                            Console.WriteLine("");
                            Console.WriteLine("--- Commands ---");
                            Console.WriteLine(".help - List commands");
                            Console.WriteLine(".coords - Display your coords");
                            Console.WriteLine(".gamemode (id) - Change your gamemode");
                            Console.WriteLine(".tp (x) (y) (z) - Teleport to any coords you want");
                            Console.WriteLine(".vclip (num) - VClip set amount of blocks");
                            Console.WriteLine(".vflip (num) - VFlip set amount of blocks");
                            Console.WriteLine(".modules - Prints list of modules to console");
                            Console.WriteLine(".toggle (module) - Toggles a module");
                            Console.WriteLine("");
                            break;
                        case "coords":
                            Console.WriteLine(Game.position);
                            Console.WriteLine("");
                            break;
                        case "gamemode":

                            if (arguments.Length >= 1)
                            {
                                List<List<string>> reg;
                                new GamemodeRegistery(out reg);

                                for (int i = 0; i < reg.Count; ++i)
                                {
                                    for (int c = 0; c < reg[i].Count; ++c)
                                    {
                                        if (arguments[0] == reg[i][c])
                                        {
                                            Game.gamemode = i;
                                            Console.WriteLine("Gamemode changed to " + reg[i][2]);
                                        }
                                    }
                                }
                            }

                            Console.WriteLine("");
                            break;
                        case "tp":

                            if (arguments.Length >= 3)
                            {
                                Game.position = Base.Vec3(arguments[0], arguments[1], arguments[2]);
                                Console.WriteLine("Teleported to " + Game.position);
                            }

                            Console.WriteLine("");
                            break;
                        case "vclip":

                            if (arguments.Length >= 1)
                            {
                                Game.vclip(Convert.ToInt32(arguments[0]));
                                Console.WriteLine("VClipped " + arguments[0] + " blocks");
                            }

                            Console.WriteLine("");
                            break;
                        case "vflip":

                            if (arguments.Length >= 1)
                            {
                                Game.vflip(Convert.ToInt32(arguments[0]));
                                Console.WriteLine("VFlipped " + arguments[0] + " blocks");
                            }

                            Console.WriteLine("");
                            break;
                        case "modules":

                            Modules.Reverse();

                            for (int i = 0; i < Modules.Count; ++i)
                                Console.WriteLine((i + 1) + ". " + Modules[i].name);

                            Modules.Reverse();

                            Console.WriteLine("");
                            break;
                        case "toggle":

                            for (int i = 0; i < Modules.Count; ++i)
                            {
                                if (Modules[i].name.ToLower() == arguments[0])
                                {
                                    if (Modules[i].enabled) Modules[i].OnDisable();
                                    else Modules[i].OnEnable();
                                    Console.WriteLine("[" + Modules[i].enabled + "] " + Modules[i].name);
                                }
                            }

                            moduleToggled.Invoke(null, new EventArgs()); // Updated array list

                            Console.WriteLine("");
                            break;
                        default:
                            Console.WriteLine("Invalid command!");
                            Console.WriteLine("");
                            break;
                    }
                }
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
