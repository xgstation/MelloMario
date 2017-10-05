using MelloMario.ItemObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.ItemObjects.StarStates
{
    class StarDefeated : IItemState
    {

        private Star starItem;
        public StarDefeated(Star star1)
        {
            starItem = star1;

        }
        public void ChangeToNormal()
        {
            starItem.State = new StarNormalState(starItem);
        }
        public void ChangeToDefeated()
        {

        }
        public void Update(GameTime time)
        {

        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {

        }
    }
}
