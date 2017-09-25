using MelloMario.ItemObjects.FireFlowerStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.ItemObjects
{
    public class FireFlower : BaseItem
    {
        public IItemState flowerState;

        public FireFlower(Vector2 initLocation) : base(initLocation)
        {
            flowerState = new FireFlowerNormalState(this);
        }

        public void TransNormal()
        {
            flowerState.transNormal();
        }

        public void TransDefeated()
        {
            flowerState.transDefeated();
        }

        public override void Update(GameTime gameTime)
        {
            flowerState.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            flowerState.Draw(spriteBatch, Location);
        }
    }
}
