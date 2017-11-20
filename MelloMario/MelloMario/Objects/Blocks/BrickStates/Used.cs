using MelloMario.Objects.Characters;

namespace MelloMario.Objects.Blocks.BrickStates
{
    internal class Used : BaseState<Brick>, IBlockState
    {
        public Used(Brick owner) : base(owner) { }

        public void Show()
        {
            Owner.State = new Normal(Owner);
        }

        public void Hide()
        {
            Owner.State = new Hidden(Owner);
        }

        public void Bump(Mario mario)
        {
            // nothing
        }

        public override void Update(int time) { }
    }
}
