
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Trero.ClientBase;
using Trero.ClientBase.EntityBase;
using Trero.ClientBase.KeyBase;
using Trero.ClientBase.UIBase;
using Trero.ClientBase.VersionBase;
using Trero.Modules;
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

            modules.Add(new AirJump());

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

            while (true) // freeze
            {
                if (!Game.isValid) return;

                ToggleLoop();
            }
        }

        static float speed = 12f; // 5.6f
        static int flicker = 0;

        static bool hidden = false;

        [DllImport("kernel32.dll")] static extern IntPtr GetConsoleWindow();
        [DllImport("user32.dll")] static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        static int t
        {
            get
            {
                hidden = !hidden;
                return (hidden ? 0 : 5);
            }
        }

        public static bool toggled_1 = false; // i would make a class for this then list them but i wont rn
        public static Keys toggled_1_KeyBind = Keys.Z;

        public static bool toggled_2 = false;
        public static Keys toggled_2_KeyBind = Keys.X;

        public static bool toggled_3 = false;
        public static Keys toggled_3_KeyBind = Keys.C;

        private static void ToggleLoop()
        {
            if (toggled_1) // if toggled_1 has been enabled
            {

            }
            if (toggled_2) // if toggled_2 has been enabled
            {

            }
            if (toggled_3) // if toggled_3 has been enabled
            {

            }
        }

        private static void keyParse(object sender, KeyEvent e)
        {
            ; // Keymap Handler

            if (e.vkey == vKeyCodes.KeyDown && e.key == Keys.G)
            {
                Mouse.MouseEvent(Mouse.MouseEventFlags.MOUSEEVENTF_LEFTDOWN);
            }

            if (e.vkey == vKeyCodes.KeyDown || e.vkey == vKeyCodes.KeyUp)
                if (Overlay.handle != null)
                    Overlay.handle.Validate();

            if (e.vkey == vKeyCodes.KeyDown)
                {
                if (e.key == Keys.Tab)
                {
                    ; // Toggle console

                    ShowWindow(GetConsoleWindow(), t);
                }
                if (e.key == toggled_1_KeyBind) toggled_1 = !toggled_1;
                if (e.key == toggled_2_KeyBind) toggled_1 = !toggled_2;
                if (e.key == toggled_3_KeyBind) toggled_1 = !toggled_3;
                
            }

            if (e.vkey == vKeyCodes.KeyUp)
            {
                if (e.key == Keys.R)
                {
                    Game.velocity = Base.Vec3();
                }
                if (e.key == Keys.I)
                {
                    Vector3 pos = Game.position;

                    Vector3 specialPos = Game.position;
                    specialPos.x += 0.6f;
                    specialPos.y += -1.8f;
                    specialPos.z += 0.6f;

                    AABB newAABB = new AABB(pos, specialPos);

                    Game.teleport(newAABB); // Noclip(No colliding full stop)
                }
            }

            if (e.vkey == vKeyCodes.KeyHeld) // broken
            {
                if (e.key == Keys.R)
                {
                    flicker++;

                    Vector3 newVel = Base.Vec3();

                    float cy = (Game.rotation.y + 89.9f) * ((float)Math.PI / 180F);
                    newVel.x = (float)Math.Cos(cy) * (speed / 9f);

                    newVel.y = -0.05f;
                    if (flicker == 360 / 32)
                    {
                        Vector3 newPos = Game.position;
                        newPos.y += 0.005f;
                        newPos.z += 0.003f;
                        newPos.x += 0.003f;

                        if (Keymap.GetAsyncKeyState((char)(Keys.LShiftKey)))
                            newPos.y -= 0.05f;
                        if (Keymap.GetAsyncKeyState((char)(Keys.Space)))
                            newPos.y += 0.08f;

                        Game.position = newPos;
                    }
                    if (flicker == (360/16))
                    {
                        Vector3 newPos = Game.position;
                        newPos.y -= 0.003f;
                        newPos.z -= 0.003f;
                        newPos.x -= 0.003f;
                        Game.position = newPos;
                    }

                    if (flicker == 360/32)
                        flicker = 0;

                    newVel.z = (float)Math.Sin(cy) * (speed / 9f);

                    Game.velocity = newVel;
                }
                else if (e.key == Keys.P)
                {
                    Process.GetProcessesByName("ApplicationFrameHost")[0].Kill();
                    Process.GetProcessesByName("Minecraft.Windows")[0].Kill();
                }
                else if (e.key == Keys.Y)
                {
                    foreach (var entity in Game.getPlayers())
                    {
                        entity.hitbox = Base.Vec2(0.6f, 1.8f);
                    }
                    foreach (var entity in Game.getTypeEntities_Antibot("player", new string[] {
                        "shop", "buy", "\r", "\n", /* Extra */
                        "tap to open", "tap to play", /*Mineplex antibot*/
                        "right click", "item shop", "squads", "upgrades"/*Nethergames antibot*/
                    }))
                    {
                        entity.hitbox = Base.Vec2(7, 7);
                    }
                }
                else if (e.key == Keys.S)
                {
                    Actor entity = Game.getClosestPlayer();
                    if (entity != null)
                    Console.WriteLine(entity.username);

                    /*Vector3 pos = entity.position;
                    if (Game.position.Distance(pos) < 4)
                    {
                        pos.y += 3;
                        Game.position = pos;
                    }*/
                }
                else if (e.key == Keys.C)
                {
                    Game.velocity = Base.Vec3();

                    Vector3 newPos = Game.position;
                    newPos.y += 0.01f;
                    Game.position = newPos;
                }
                else if (e.key == Keys.V)
                {
                    Game.velocity = Base.Vec3();

                    Vector3 newPos = Game.position;
                    newPos.y += -0.01f;
                    Game.position = newPos;
                }
                else if (e.key == Keys.G)
                {
                    Mouse.MouseEvent(Mouse.MouseEventFlags.MOUSEEVENTF_RIGHTDOWN);
                    Game.velocity = Base.Vec3();
                    Game.isLookingAtBlock = 0;
                    


                }
                else if (e.key == Keys.Y)
                {
                    foreach (var entity in Game.getPlayers())
                    {
                        entity.hitbox = Base.Vec2(0.6f, 1.8f);
                    }
                    foreach (var entity in Game.getTypeEntities_Antibot("player", new string[] {
                        "shop", "buy", "\r", "\n", /* Extra */
                        "tap to open", "tap to play", /*Mineplex antibot*/
                        "right click", "item shop", "squads", "upgrades"/*Nethergames antibot*/
                    }))
                    {
                        entity.hitbox = Base.Vec2(7, 7);
                    }
                }
                else if (e.key == Keys.X)
                {
                    Game.onGround = true;

                    Game.velocity = Base.Vec3();

                    Vector3 newVel = Base.Vec3();

                    float cy = (Game.rotation.y + 89.9f) * ((float)Math.PI / 180F);

                    if (Keymap.GetAsyncKeyState((char)(Keys.W)))
                        newVel.z = (float)Math.Sin(cy) * (8 / 9f); ///Working Fly With No Height 


                    if (Keymap.GetAsyncKeyState((char)(Keys.W)))
                        newVel.x = (float)Math.Cos(cy) * (8 / 9f);
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
                }  ///Fly
            }
        }
    }
}
