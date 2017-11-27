namespace MelloMario.Objects.Enemies.ThwompStates
{
    using MelloMario.Interfaces.Objects.States;
    #region

    using System;

    #endregion

    [Serializable]
    internal class Defeated : BaseState<Thwomp>, IThwompState
    {
        public Defeated(Thwomp owner) : base(owner)
        {
        }

        public void Show()
        {
            Owner.State = new Normal(Owner);
        }

        public void Defeat()
        {
        }

        public override void Update(int time)
        {
        }
    }
}
