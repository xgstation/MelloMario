using MelloMario.MarioObjects.MovementStates;

namespace MelloMario.Commands

{
    class JumpPress : BaseCommand<IGameCharacter>
    {
    private IMarioMovementState movementState;
        public JumpPress(IGameCharacter character, IMarioMovementState move) : base(character)
        {
        movementState = move;
        }

        public override void Execute()
        {
        if (!(movementState is Jumping))
            Receiver.JumpPress();
        }
    }
}
