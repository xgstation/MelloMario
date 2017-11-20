namespace MelloMario.ItemObjects.StarStates
{
    internal class Normal : BaseState<Star>, IItemState
    {
        public Normal(Star owner) : base(owner) { }

        public void Show() { }

        public void Collect() { }

        public override void Update(int time) { }
    }
}
