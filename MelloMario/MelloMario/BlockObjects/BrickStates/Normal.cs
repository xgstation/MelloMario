using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.BlockObjects.BrickStates
{
    class Normal : IBlockState
    {
        public BrickBlock brickBlock { get; set; }
        public ISprite sprite { get; set; }
        //Hitting size of Brick: 16x16 pixels
        public Point Size { get; set; }
        public int ItemQuantity { get; set; }
        public Rectangle boundary { get; set; }
        public Boolean isVisible { get; set; }
        public Normal(BrickBlock brickBlock, Vector2 location)
        {
            this.brickBlock = brickBlock;
            sprite = SpriteFactory.Instance.CreateBrickSprite();
            Size = new Point(16, 16);
            boundary = new Rectangle(location.ToPoint(), Size);
            ItemQuantity = 0;
            isVisible = true;
        }
        public Normal(BrickBlock brickBlock, Vector2 location, Boolean isVisible) : this (brickBlock, location)
        {
            this.isVisible = isVisible;
        }
        public void changeToBumped()
        {
            brickBlock.state = new Bumped(brickBlock, boundary.Location.ToVector2());
        }
        public void changeToDestroied()
        {
            brickBlock.state = new Destroied(brickBlock, boundary.Location.ToVector2());
        }

        public void changeToHidden()
        {
            isVisible = false;
        }

        public void changeToUsed()
        {
            brickBlock.state = new QuestionStates.Used(brickBlock, boundary.Location.ToVector2());
        }

        public virtual void Update(GameTime gameTime)
        {
            sprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isVisible)
                sprite.Draw(spriteBatch, boundary.Location.ToVector2());
        }

        public ISprite GetSprite()
        {
            return sprite;
        }

        public Rectangle GetBoundary()
        {
            return boundary;
        }
    }
}
