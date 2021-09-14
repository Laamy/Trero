namespace Trero.Modules
{
    internal class Unlimiter : Module
    {
        public Unlimiter() : base("Unlimiter", (char)0x07)
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