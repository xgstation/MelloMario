using MelloMario.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.ItemObjects.FireFlowerStates
{
    class FireFlowerNormal : IItemState
    {
        private FireFlower flower;

        public FireFlowerNormal(FireFlower flower1)
        {
            flower = flower1;
        }

        public void Show()
        {
        }

        public void Collect()
        {
            flower.State = new FireFlowerDefeated(flower);
        }

        public void Update(GameTime time)
        {
        }

    }
}
