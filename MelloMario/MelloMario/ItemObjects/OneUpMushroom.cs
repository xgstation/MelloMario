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
        public IItemState State;

        public OneUpMushroom(Vector2 initLocation) : base(initLocation)
        {
            State = new OneUpMushroomNormalState(this);
        }

        public void TransNormal()
        {
            State.transNormal();
        }

        public void TransDefeated()
        {
            State.transDefeated();
        }

        public override void Update(GameTime gameTime)
        {
            State.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            State.Draw(spriteBatch, Location);
        }
    }
}
