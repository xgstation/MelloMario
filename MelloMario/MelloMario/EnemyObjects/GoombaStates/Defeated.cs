using Microsoft.Xna.Framework.Audio;
using MelloMario.Sounds;

namespace MelloMario.EnemyObjects.GoombaStates
{
    class Defeated : BaseState<Goomba>, IGoombaState
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
                SoundController.EnemyKill.Play();
                played += 1;
            }
        }
    }
}
