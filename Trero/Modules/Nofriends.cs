#region

using Trero.ClientBase;

#endregion

namespace Trero.Modules
{
    internal class Nofriends : Module
    {
        public Nofriends() : base("Nofriends", (char)0x07, "Other", "Ignore all friends, just like you irl!")
        {
        }

        public override void OnEnable()
        {
            Game.CustomDefines.nofriends = true;
            base.OnEnable();
        }

        public override void OnDisable()
        {
            Game.CustomDefines.nofriends = false;
            base.OnDisable();
        }
    }
}