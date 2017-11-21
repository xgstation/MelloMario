namespace MelloMario.Objects.Enemies.KoopaStates
{
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
            Owner.State = new MovingShell(Owner);
        }

        public void Defeat() { }

        public override void Update(int time)
        {
            if (played <= 2)
            {
                //TODO:Move this into soundcontroller
                //if (SoundController.EnemyKill.State == SoundState.Stopped)
                //{
                //    played += 1;
                //}
                //SoundController.EnemyKill.Play();
            }
        }

        public void Pushed()
        {
            Owner.State = new MovingShell(Owner);
            //do nothing for this sprint
        }
    }
}
