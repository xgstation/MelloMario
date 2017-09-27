﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.BlockObjects.QuestionStates
{
    class QuestionSilent : IBlockState
    {
        private QuestionBlock block;
        private ISprite sprite;
        private int itemQuantity;

        public QuestionSilent(QuestionBlock block)
        {
            this.block = block;
            sprite = SpriteFactory.Instance.CreateQuestion("Silent");
            itemQuantity = 1;
        }
        public QuestionSilent(QuestionBlock block, int itemQuantity) : this(block)
        {
            this.itemQuantity = itemQuantity;
        }

        public void ChangeToSilent()
        {
            //do nothing
        }

        public void ChangeToDestroyed()
        {
            //question blocks cant be destroyed
        }

        public void ChangeToHidden()
        {
            block.State = new QuestionHidden(block);
        }

        public void ChangeToUsed()
        {
            block.State = new QuestionUsed(block);
        }

        public void ChangeToBumped()
        {
            block.State = new QuestionBumped(block, itemQuantity);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch, location);
        }

        public void Update(GameTime gameTime)
        {
            sprite.Update(gameTime);
        }
    }
}