using MelloMario.Objects.Characters;

namespace MelloMario.Objects.Blocks.QuestionStates
{
    internal class Hidden : BaseState<Question>, IBlockState
    {
        public Hidden(Question owner) : base(owner) { }

        public void Show()
        {
            Owner.State = new Normal(Owner);
        }

        public void Hide()
        {
            //do nothing
        }

        public void Bump(Mario mario)
        {
            Owner.State = new Normal(Owner);
            Owner.Bump(mario);
        }

        public override void Update(int time)
        {
            //do nothing
        }
    }
}
