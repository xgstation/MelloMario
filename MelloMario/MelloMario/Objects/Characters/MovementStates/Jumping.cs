namespace MelloMario.Objects.Characters.MovementStates
{
    #region

    using System;

    #endregion

    internal class Jumping : BaseTimedState<Mario>, IMarioMovementState
    {
        private IMarioMovementState previous;

        public Jumping(Mario owner) : base(owner, 200)
        {
            previous = owner.MovementState;
            Finished = false;
        }

        public bool Finished { get; set; }

        public void Crouch()
        {
            Owner.MovementState = new Crouching(Owner);
        }

        public void Idle()
        {
            previous = new Standing(Owner);
        }

        public void Land()
        {
            Owner.MovementState = previous;
        }

        public void Jump()
        {
        }

        public void Walk()
        {
            previous = new Walking(Owner);
        }

        protected override void OnTimer(int time)
        {
            Finished = true;
        }
    }
}
