using Microsoft.Xna.Framework.Audio;
using MelloMario.Sounds;

namespace MelloMario.EnemyObjects.GoombaStates
{
    class Defeated : BaseState<Goomba>, IGoombaState
    {
        private SoundEffectInstance stompSound;
        private int played;

        public Defeated(Goomba owner) : base(owner)
        {
            stompSound = SoundController.enemyKill.CreateInstance();
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
                stompSound.Play();
                played += 1;
            }
        }
    }
}
