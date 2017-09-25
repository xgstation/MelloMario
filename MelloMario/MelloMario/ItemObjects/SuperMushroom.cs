using MelloMario.ItemObjects.SuperMushroomStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.ItemObjects
{
    public class SuperMushroom : BaseItem
    {
        public IItemState mushroomState;

        public SuperMushroom(Vector2 initLocation) : base(initLocation)
        {
            mushroomState = new SuperMushroomNormalState(this);
        }

        public void TransNormal()
        {
            mushroomState.transNormal();
        }

        public void TransDefeated()
        {
            mushroomState.transDefeated();
        }

        public override void Update(GameTime gameTime)
        {
            mushroomState.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            mushroomState.Draw(spriteBatch, Location);
        }
    }
}
