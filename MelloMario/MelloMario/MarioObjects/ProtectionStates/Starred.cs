namespace MelloMario.MarioObjects.ProtectionStates
{
    internal class Starred : BaseTimedState<Mario>, IMarioProtectionState
    {
        public Starred(Mario owner) : base(owner, 10000) //orignially 15000
        { }

        public void Star()
        {
            Owner.ProtectionState = new Starred(Owner);
        }

        public void Protect()
        {
            //do nothing since star overrides protect
        }

        protected override void OnTimer(int time)
        {
            Owner.ProtectionState = new Normal(Owner);
        }
    }
}