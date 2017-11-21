namespace MelloMario.Objects.Characters.ProtectionStates
{
    internal class Normal : BaseState<Mario>, IMarioProtectionState
    {
        public Normal(Mario owner) : base(owner)
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
