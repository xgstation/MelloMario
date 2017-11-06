using Microsoft.Xna.Framework;

namespace MelloMario.MarioObjects.ProtectionStates
{
    class Protected : BaseTimedState<Mario>, IMarioProtectionState
    {
        protected override void OnTimer(int time)
        {
            Owner.ProtectionState = new Normal(Owner);
        }

        public Protected(Mario owner) : base(owner, 1000)
        {
        }

        public void Protect()
        {
            //refresh protection
            Owner.ProtectionState = new Protected(Owner);
        }

        public void Star()
        {
            Owner.ProtectionState = new Starred(Owner);
        }
    }
}
