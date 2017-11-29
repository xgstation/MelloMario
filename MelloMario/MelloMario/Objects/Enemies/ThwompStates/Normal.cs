namespace MelloMario.Objects.Enemies.ThwompStates
{
    #region

    using System;

    #endregion
    
    using MelloMario.Interfaces.Objects.States;
    using MelloMario.Objects.Enemies.PiranhaStates;

    [Serializable]
    internal class Normal : BaseTimedState<Thwomp>, IThwompState
    {
        public Normal(Thwomp owner) : base(owner, owner.NormalTime)
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

        protected override void OnTimer(int time)
        {
            if (!Owner.HasMarioBelow)
            {
                Owner.State = new MovingUp(Owner);
            }
        }
    }
}
