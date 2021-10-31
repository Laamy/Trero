namespace Trero.Modules
{
    internal class Unlimiter : Module
    {
        public Unlimiter() : base("CPU Unlimiter", (char)0x07, "Other", "Removes all cpu limitations and allows the client to use as much as it wants")
        {
        }

        public override void OnEnable()
        {
            Program.unlimiter = true;
            base.OnEnable();
        }

        public override void OnDisable()
        {
            Program.unlimiter = false;
            base.OnDisable();
        }
    }
}
