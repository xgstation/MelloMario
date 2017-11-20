namespace MelloMario.Objects.Characters.MovementStates
{
    internal class Walking : BaseState<Mario>, IMarioMovementState
    {
        public Walking(Mario owner) : base(owner) { }

        public void Crouch()
        {
            Owner.MovementState = new Crouching(Owner);
        }

        public void Idle()
        {
            Owner.MovementState = new Standing(Owner);
        }

        public void Land() { }

        public void Jump()
        {
            Owner.MovementState = new Jumping(Owner);
        }

        public void Walk() { }

        public override void Update(int time)
        {
            //sprite.Update(game);
        }
    }
}
