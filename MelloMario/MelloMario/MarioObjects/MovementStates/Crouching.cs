namespace MelloMario.MarioObjects.MovementStates
{
    internal class Crouching : BaseState<Mario>, IMarioMovementState
    {
        public Crouching(Mario owner) : base(owner) { }

        public void Crouch() { }

        public void Idle()
        {
            Owner.MovementState = new Standing(Owner);
        }

        public void Land() { }

        public void Jump() { }

        public void Walk() { }

        public override void Update(int time)
        {
            //sprite.Update(game);
        }
    }
}