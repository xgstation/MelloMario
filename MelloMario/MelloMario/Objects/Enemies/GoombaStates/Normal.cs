namespace MelloMario.Objects.Enemies.GoombaStates
{
    #region

    using System;

    #endregion

    internal class Normal : BaseState<Goomba>, IGoombaState
    {
        public Normal(Goomba owner) : base(owner)
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
