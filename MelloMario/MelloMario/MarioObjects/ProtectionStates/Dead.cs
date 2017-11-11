namespace MelloMario.MarioObjects.ProtectionStates
{
    class Dead : BaseState<Mario>, IMarioProtectionState
    {
        public Dead(Mario owner) : base(owner)
        {
        }

        public void Star()
        {
            Owner.ProtectionState = new Starred(Owner);
        }

        public void Protect()
        {
            Owner.ProtectionState = new Protected(Owner);
        }

        public override void Update(int time)
        {
        }
    }
}
