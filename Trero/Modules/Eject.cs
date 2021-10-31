using System.Windows.Forms;

namespace Trero.Modules
{
    internal class Eject : Module
    {
        public Eject() : base("Eject", (char)0x07, "Other", "Eject the client ui")
        {
        } // Not defined

        public override void OnEnable()
        {
            Program.quit = true;
            Application.Exit(); // ?
        }
    }
}