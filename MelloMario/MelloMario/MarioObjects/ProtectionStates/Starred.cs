using Microsoft.Xna.Framework;

namespace MelloMario.MarioObjects.ProtectionStates
{
    class Starred : BaseTimedState<Mario>, IMarioProtectionState
    {
        protected override void OnTimer(int time)
        {
            Owner.ProtectionState = new Normal(Owner);
        }

        public Starred(Mario owner) : base(owner, 10000) //orignially 15000
        {
        }

        public void Star()
        {
            Owner.ProtectionState = new Starred(Owner);
        }

        public void Protect()
        {
            //do nothing since star overrides protect
        }
    }
}
