namespace MelloMario.EnemyObjects.KoopaStates
{
    class Defeated : BaseState<Koopa>, IKoopaState
    {
        public Defeated(Koopa owner) : base(owner)
        {
        }

        public void Show()
        {
            Owner.State = new Normal(Owner);
        }

        public void JumpOn()
        {
            Owner.State = new MovingShell(Owner);
        }

        public void Defeat()
        {
        }

        public override void Update(int time)
        {
        }

        public void Pushed()
        {
            Owner.State = new MovingShell(Owner);
            //do nothing for this sprint
        }
    }
}
