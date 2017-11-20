using MelloMario.MarioObjects;

namespace MelloMario.BlockObjects.QuestionStates
{
    internal class Used : BaseState<Question>, IBlockState
    {
        public Used(Question owner) : base(owner) { }

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
            //do nothing
        }

        public override void Update(int time) { }
    }
}
