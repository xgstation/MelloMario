namespace MelloMario.Objects.Enemies.PiranhaStates
{
    #region

    using MelloMario.Interfaces.Objects.States;

    #endregion

    internal class Defeated : BaseState<Piranha>, IPiranhaState
    {
        public Defeated(Piranha owner) : base(owner) { }

        public void Defeat()
        {
            //Do nothing
        }

        public override void Update(int time) { }
    }
}
