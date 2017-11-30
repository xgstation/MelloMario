namespace MelloMario.Objects.Enemies.BeetleStates
{
    #region

    using System;

    #endregion

    [Serializable]
    internal class Defeated : BaseState<Beetle>, IBeetleState
    {
        private readonly int played;

        public Defeated(Beetle owner) : base(owner)
        {
            played = 0;
        }

        public void Show()
        {
            Owner.State = new Normal(Owner);
        }

        public void JumpOn()
        {
            Owner.State = new NewlyMovingShell(Owner);
        }

        public override void Update(int time)
        {
            if (played <= 2)
            {
                //TODO:Move this into soundcontroller
                //if (SoundManager.EnemyKill.State == SoundState.Stopped)
                //{
                //    played += 1;
                //}
                //SoundManager.EnemyKill.Play();
            }
        }

        public void Wear()
        {
            Owner.State = new Worn(Owner);
        }

        public void Pushed()
        {
            Owner.State = new NewlyMovingShell(Owner);
            //do nothing for this sprint
        }
    }
}
