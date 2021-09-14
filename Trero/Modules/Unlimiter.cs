namespace Trero.Modules
{
    internal class Limiter : Module
    {
        public Limiter() : base("Limiter", (char)0x07)
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