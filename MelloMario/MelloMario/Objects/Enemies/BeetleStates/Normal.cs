namespace MelloMario.Objects.Enemies.BeetleStates
{
    #region

    using System;

    #endregion

    [Serializable]
    internal class Normal : BaseState<Beetle>, IBeetleState
    {
        public Normal(Beetle owner) : base(owner)
        {
        }

        public void Show()
        {
        }

        public void JumpOn()
        {
            Owner.State = new Defeated(Owner);
        }

        public void Defeat()
        {
            Owner.State = new Defeated(Owner);
        }

        public override void Update(int time)
        {
        }

        public void Pushed()
        {
            //can't push normal koopa
        }
    }
}
