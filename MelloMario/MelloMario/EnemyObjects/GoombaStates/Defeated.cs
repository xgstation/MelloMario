using Microsoft.Xna.Framework;

namespace MelloMario.EnemyObjects.GoombaStates
{
    class Defeated : BaseState<Goomba>, IGoombaState
    {
        public Defeated(Goomba owner) : base(owner)
        {
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
        }
    }
}
