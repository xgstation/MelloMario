﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.ItemObjects.OneUpMushroomStates
{
    public class OneUpMushroomDefeatedState : IItemState
    {
        private OneUpMushroom mushroomItem;
        public OneUpMushroomDefeatedState(OneUpMushroom mushroomItem1)
        {
            mushroomItem = mushroomItem1;
        }
        public void ChangeToNormal()
        {
            mushroomItem.State = new OneUpMushroomNormalState(mushroomItem);
        }
        public void ChangeToDefeated()
        {

        }
        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {

        }
    }
}
