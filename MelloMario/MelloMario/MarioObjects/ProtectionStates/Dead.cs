using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using MelloMario.Sounds;

namespace MelloMario.MarioObjects.ProtectionStates
{
    class Dead : BaseTimedState<Mario>, IMarioProtectionState
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
