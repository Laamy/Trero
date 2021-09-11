
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
            modules.Add(new Fly());
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

            //modules[0].onDisable();

            modules.Sort((c1, c2) => c2.name.CompareTo(c1.name)); // ABC Order

            VersionClass.setVersion(VersionClass.versions[0]);

            Keymap.keyEvent += keyParse;

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
                    mod.onLoop();
            }
        }

        private static void keyParse(object sender, KeyEvent e)
        {
            ; // Keymap Handler

            /*if (e.vkey == vKeyCodes.KeyHeld && e.key == Keys.LControlKey)
            {
                Mouse.MouseEvent(Mouse.MouseEventFlags.MOUSEEVENTF_LEFTDOWN);
            }*/

            if (e.vkey == vKeyCodes.KeyUp && e.key == Keys.R)
            {
                Game.velocity = Base.Vec3();
            }

            /*if (e.vkey == vKeyCodes.KeyUp && e.key == Keys.G)
            {
                // 0x892A45 => "89 41"
                MCM.writeBaseBytes(0x892A45, MCM.ceByte2Bytes("89 41 18")); // Restore with original assembly code
                MCM.writeBaseBytes(0x898385, MCM.ceByte2Bytes("C7 40 18 03 00 00 00"));
            }
            if (e.vkey == vKeyCodes.KeyDown && e.key == Keys.G)
            {
                Game.isLookingAtBlock = 0;

                MCM.writeBaseBytes(0x892A45, MCM.ceByte2Bytes("90 90 90")); // Nop assembly code
                MCM.writeBaseBytes(0x898385, MCM.ceByte2Bytes("90 90 90 90 90 90 90"));
            }*/

            // Fixed noclip on run

            if (e.vkey == vKeyCodes.KeyHeld) // broken
            {
                if (e.key == Keys.G)
                {
                    Game.isLookingAtBlock = 0;
                    Game.SideSelect = 1;
                    Game.SelectedBlock = Base.iVec3((int)Game.position.x, (int)Game.position.y - 1, (int)Game.position.z);

                    Mouse.MouseEvent(Mouse.MouseEventFlags.MOUSEEVENTF_RIGHTDOWN);
                }///Scaffold (Broken)
                else if (e.key == Keys.X)
                {
                    Game.onGround = true;

                    Game.velocity = Base.Vec3();

                    Vector3 newVel = Base.Vec3();

                    float cy = (Game.rotation.y + 89.9f) * ((float)Math.PI / 180F);

                    if (Keymap.GetAsyncKeyState((char)(Keys.W)))
                        newVel.z = (float)Math.Sin(cy) * (4 / 5f); ///Working Fly With No Height 


                    if (Keymap.GetAsyncKeyState((char)(Keys.W)))
                        newVel.x = (float)Math.Cos(cy) * (4 / 5f);
                    Game.velocity = newVel;
                }///No Y Fly
                else if (e.key == Keys.T)
                {

                    
                    Game.onGround = true;

                    Game.velocity = Base.Vec3();

                    Vector3 newVel = Base.Vec3();

                    float cy = (Game.rotation.y + 89.9f) * ((float)Math.PI / 180F);


                    if (Keymap.GetAsyncKeyState((char)(Keys.W)))
                        newVel.z = (float)Math.Sin(cy) * (12 / 16f);
                    if (Keymap.GetAsyncKeyState((char)(Keys.Space)))
                        newVel.y += 0.89f;
                    if (Keymap.GetAsyncKeyState((char)(Keys.LShiftKey))) ///Working Fly 
                        newVel.y -= 0.89f;

                    if (Keymap.GetAsyncKeyState((char)(Keys.W)))
                        newVel.x = (float)Math.Cos(cy) * (12 / 16f);
                    Game.velocity = newVel;
                    Game.velocity = newVel;
                }///Fly
            }
        }
    }
}
