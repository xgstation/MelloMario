namespace MelloMario.Objects.Enemies.KoopaStates
{
    internal class MovingShell : BaseState<Koopa>, IKoopaState
    {
        public MovingShell(Koopa owner) : base(owner) { }

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

        public override void Update(int time) { }

        public void Pushed()
        {
            //do nothing for this sprint
        }
    }
}
