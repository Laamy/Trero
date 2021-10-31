namespace Trero.Modules
{
    internal class Limiter : Module
    {
        public Limiter() : base("CPU Limiter", (char)0x07, "Other", "Limit CPU Usage")
        {
        }

        public override void OnEnable()
        {
            Program.limiter = true;
            base.OnEnable();
        }

        public override void OnDisable()
        {
            Program.limiter = false;
            base.OnDisable();
        }
    }
}
