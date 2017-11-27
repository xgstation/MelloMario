namespace MelloMario.Objects.Enemies.ThwompStates
{
    #region

    using System;
    using MelloMario.Interfaces.Objects.States;

    #endregion

    [Serializable]
    internal class MovingUp : BaseState<Thwomp>, IThwompState
    {
        private readonly int initialY;

        public MovingUp(Thwomp owner) : base(owner)
        {
            initialY = owner.Boundary.Y;
        }

        public override void Update(int time)
        {
            if (Owner.Boundary.Y <= initialY - 48)
            {
                //Owner.State = new Show(Owner);
            }
        }

        public void Defeat()
        {
            Owner.State = new Defeated(Owner);
        }
    }
}

