namespace MelloMario.Objects.Characters.ProtectionStates
{
    #region

    using System;
    using MelloMario.Objects.Enemies;

    #endregion

    internal class Dead : BaseTimedState<Mario>, IMarioProtectionState
    {
        public Dead(Mario owner) : base(owner, 1500)
        {
            //MediaPlayer.Stop();

            //TODO:Move this into soundcontroller
            //SoundManager.Death.Play();
            owner.OnDeath();
        }

        public void Star()
        {
            Owner.ProtectionState = new Starred(Owner);
        }

        public void Protect()
        {
            Owner.ProtectionState = new Protected(Owner);
        }

        protected override void OnTimer(int time)
        {
            Owner.TransToGameOver();
        }

        public void Helmet()
        {
            //do nothing
        }
    }
}
