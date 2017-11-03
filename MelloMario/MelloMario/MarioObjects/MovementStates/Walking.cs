using Microsoft.Xna.Framework;

namespace MelloMario.MarioObjects.MovementStates
{
    class Walking : BaseState<Mario>, IMarioMovementState
    {
        public Walking(Mario owner) : base(owner)
        {
        }

        public void Crouch()
        {
            Owner.MovementState = new Crouching(Owner);
        }

        public void Idle()
        {
            Owner.MovementState = new Standing(Owner);
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
        }

        public override void Update(GameTime time)
        {
            //sprite.Update(game);
        }
    }
}
