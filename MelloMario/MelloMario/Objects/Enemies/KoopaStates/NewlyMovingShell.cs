namespace MelloMario.Objects.Enemies.KoopaStates
{
    #region

    using System;

    #endregion

    [Serializable]
    internal class NewlyMovingShell : BaseTimedState<Koopa>, IKoopaState
    {
        public NewlyMovingShell(Koopa owner) : base(owner, 250)
        {
        }

        public void Show()
        {
            Owner.State = new Normal(Owner);
        }

        public void JumpOn()
        {
            Owner.State = new Defeated(Owner);
        }

        public void Defeat()
        {
            Owner.State = new Defeated(Owner);
        }

        protected override void OnTimer(int time)
        {
            Owner.State = new MovingShell(Owner);
        }

        public void Pushed()
        {
        }
    }
}
