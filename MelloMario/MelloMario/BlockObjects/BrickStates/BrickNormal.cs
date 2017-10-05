﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.BlockObjects.BrickStates
{
    class BrickNormal : IBlockState
    {
        private BrickBlock block;
        private ISprite sprite;

        public BrickNormal(BrickBlock block)
        {
            this.block = block;
            sprite = SpriteFactory.Instance.CreateBrick("Normal");
        }

        public void Show()
        {
            //do nothing
        }

        public void Destroy()
        {
            block.State = new BrickDestroyed(block);
        }

        public void Hide()
        {
            block.State = new BrickHidden(block);
        }

        public void UseUp()
        {
            block.State = new BrickUsed(block);
        }

        public void Bump()
        {
            block.State = new BrickBumped(block);
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