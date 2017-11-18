using MelloMario.Sounds;
using Microsoft.Xna.Framework.Audio;

namespace MelloMario.EnemyObjects.KoopaStates
{
    internal class Defeated : BaseState<Koopa>, IKoopaState
    {
        private int played;

        public Defeated(Koopa owner) : base(owner)
        {
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

        public void Defeat() { }

        public override void Update(int time)
        {
            if (played <= 2)
            {
                if (SoundController.EnemyKill.State == SoundState.Stopped)
                    played += 1;
                SoundController.EnemyKill.Play();
            }
        }

        public void Pushed()
        {
            Owner.State = new MovingShell(Owner);
            //do nothing for this sprint
        }
    }
}