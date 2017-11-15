using Microsoft.Xna.Framework.Audio;
using MelloMario.Sounds;

namespace MelloMario.EnemyObjects.KoopaStates
{
    class Defeated : BaseState<Koopa>, IKoopaState
    {
        private SoundEffectInstance stompSound;
        private int played;

        public Defeated(Koopa owner) : base(owner)
        {
            stompSound = SoundController.EnemyKill.CreateInstance();
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

        public void Defeat()
        {
        }

        public override void Update(int time)
        {
            if (played <= 2)
            {
                stompSound.Play();
                played += 1;
            }
        }

        public void Pushed()
        {
            Owner.State = new MovingShell(Owner);
            //do nothing for this sprint
        }
    }
}
