namespace MelloMario.Objects.Enemies.KoopaStates
{
    #region

    using System;

    #endregion

    internal class Defeated : BaseState<Koopa>, IKoopaState
    {
        private readonly int played;

        public Defeated(Koopa owner) : base(owner)
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

        public void Defeat()
        {
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

        public void Pushed()
        {
            Owner.State = new NewlyMovingShell(Owner);
            //do nothing for this sprint
        }
    }
}
