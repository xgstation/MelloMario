﻿using MelloMario.ItemObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.ItemObjects.SuperMushroomStates
{
    public class SuperMushroomNormalState : IItemState
    {
        private ISprite mushroom;
        private SuperMushroom mushroomItem;
        public SuperMushroomNormalState(SuperMushroom mushroomItem1)
        {
            mushroomItem = mushroomItem1;
            mushroom = SpriteFactory.Instance.CreatSuperMushroomSprite();

        }
        public void ChangeToNormal()
        {

        }

        public void ChangeToDefeated()
        {
            mushroomItem.State = new SuperMushroomDefeatedState(mushroomItem);
        }
        public void Update(GameTime gameTime)
        {
            mushroom.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            mushroom.Draw(spriteBatch, location);
        }
    }
}
