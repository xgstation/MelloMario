namespace MelloMario.Objects.Characters.MovementStates
{
    #region

    using System;

    #endregion

    internal class Standing : BaseState<Mario>, IMarioMovementState
    {
        public Standing(Mario owner) : base(owner)
        {
        }

        public void Crouch()
        {
            Owner.MovementState = new Crouching(Owner);
        }

        public void Idle()
        {
        }

        public void Land()
        {
        }

        public void Jump()
        {
            Owner.MovementState = new Jumping(Owner);
        }

        public void Walk()
        {
            Owner.MovementState = new Walking(Owner);
        }

        public override void Update(int time)
        {
            //sprite.Update(game);
        }
    }
}
