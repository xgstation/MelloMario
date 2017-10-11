using MelloMario.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.ItemObjects.FireFlowerStates
{
    class Normal : IItemState
    {
        private FireFlower flower;

        public Normal(FireFlower flower1)
        {
            flower = flower1;
        }

        public void Show()
        {
        }

        public void Collect()
        {
        }

        public void Update(GameTime time)
        {
        }

    }
}
