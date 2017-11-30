namespace MelloMario.Objects.Enemies.GoombaStates
{
    #region

    using System;

    #endregion

    internal class Defeated : BaseState<Goomba>, IGoombaState
    {
        private int played;

        public Defeated(Goomba owner) : base(owner)
        {
            played = 0;
        }

        public void Show()
        {
            Owner.State = new Normal(Owner);
        }

        public void Defeat()
        {
        }

        public override void Update(int time)
        {
            if (played <= 2)
            {
                //TODO:Move this into soundcontroller
                //SoundManager.EnemyKill.Play();
                played += 1;
            }
        }
    }
}
