using MelloMario.Sounds;
using Microsoft.Xna.Framework.Media;

namespace MelloMario.MarioObjects.ProtectionStates
{
    internal class Dead : BaseTimedState<Mario>, IMarioProtectionState
    {
        public Dead(Mario owner) : base(owner, 1500)
        {
            SoundController.Death.Play();
            MediaPlayer.Stop();
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
    }
}