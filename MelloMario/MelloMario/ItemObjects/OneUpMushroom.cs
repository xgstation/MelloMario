using MelloMario.ItemObjects.OneUpMushroomStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.ItemObjects
{
    public class OneUpMushroom : BaseItem
    {
        public IItemState oneUpMushroomState;

        public OneUpMushroom(Vector2 initLocation) : base(initLocation)
        {
            oneUpMushroomState = new OneUpMushroomNormalState(this);
        }

        public void TransNormal()
        {
            oneUpMushroomState.transNormal();
        }

        public void TransDefeated()
        {
            oneUpMushroomState.transDefeated();
        }

        public override void Update(GameTime gameTime)
        {
            oneUpMushroomState.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            oneUpMushroomState.Draw(spriteBatch, Location);
        }
    }
}
