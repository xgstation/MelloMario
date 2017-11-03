using Microsoft.Xna.Framework;

namespace MelloMario.ItemObjects.CoinStates
{
    class Normal : BaseState<Coin>, IItemState
    {
        public Normal(Coin owner) : base(owner)
        {
        }

        public void Show()
        {
        }

        public void Collect()
        {

        }

        public override void Update(GameTime time)
        {
        }
    }
}
