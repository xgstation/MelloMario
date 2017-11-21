namespace MelloMario.Objects.Enemies.ThwompStates
{
    using System;

    [Serializable]
    internal class Normal : BaseState<Thwomp>, IThwompState
    {
        public Normal(Thwomp owner) : base(owner)
        {
        }

        public void Show()
        {
        }

        public void Defeat()
        {
            Owner.State = new Defeated(Owner);
        }

        public override void Update(int time)
        {
        }
    }
}
