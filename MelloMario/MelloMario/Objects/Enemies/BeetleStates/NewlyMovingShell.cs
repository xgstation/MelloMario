﻿namespace MelloMario.Objects.Enemies.BeetleStates
{
    #region

    using System;

    #endregion

    internal class NewlyMovingShell : BaseTimedState<Beetle>, IBeetleState
    {
        public NewlyMovingShell(Beetle owner) : base(owner, 500)
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

        public void Wear()
        {
            Owner.State = new Worn(Owner);
        }

        public void Pushed()
        {
        }

        protected override void OnTimer(int time)
        {
            Owner.State = new MovingShell(Owner);
        }
    }
}
