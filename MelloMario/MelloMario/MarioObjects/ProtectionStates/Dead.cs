using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using MelloMario.Sounds;

namespace MelloMario.MarioObjects.ProtectionStates
{
    class Dead : BaseState<Mario>, IMarioProtectionState
    {
        private SoundEffectInstance deadSound;

        public Dead(Mario owner) : base(owner)
        {
            deadSound = SoundController.death.CreateInstance();
            deadSound.Play();
            MediaPlayer.Stop();
        }

        public void Star()
        {
            Owner.ProtectionState = new Starred(Owner);
        }

        public void Protect()
        {
            Owner.ProtectionState = new Protected(Owner);
        }

        public override void Update(int time)
        {
        }
    }
}
