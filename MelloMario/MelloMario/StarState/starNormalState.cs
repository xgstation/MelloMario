using MelloMario.ItemObject;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.StarState
{
    public class StarNormalState : Interfaces.ItemState
    {
        private ISprite star;
        private Star starItem;
        public StarNormalState(Star star1)
        {
            starItem = star1;
            star = SpriteFactory.Instance.CreateStarSprite();
        }
        public void transNormal()
        {

        }
        public void transDefeated()
        {
            starItem.starState = new StarDefeatedState(starItem);
        }

        public void Update(GameTime gameTime)
        {
            star.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            star.Draw(spriteBatch, location);
        }
    }
}
