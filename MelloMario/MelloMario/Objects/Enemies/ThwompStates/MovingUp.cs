﻿namespace MelloMario.Objects.Enemies.ThwompStates
{
    #region

    using System;

    #endregion

    internal class MovingUp : BaseState<Thwomp>, IState
    {
        private readonly int initialY;

        public MovingUp(Thwomp owner) : base(owner)
        {
            initialY = owner.Boundary.Y;
        }

        public override void Update(int time)
        {
            if (Owner.Boundary.Y <= initialY)
            {
                //Owner.State = new Show(Owner);
            }
        }
    }
}
