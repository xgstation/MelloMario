using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.BlockObjects.QuestionStates
{
    class Used : IBlockState
    {
        public QuestionBlock questionBlock { get; set; }
        public ISprite sprite { get; set; }
        //Hitting size of Brick: 16x16 pixels
        public Point Size { get; set; }
        public int ItemQuantity { get; set; }
        public Rectangle boundary { get; set; }
        public Boolean isVisible { get; set; }

        public Used(QuestionBlock questionBlock, Vector2 location)
        {
            this.questionBlock = questionBlock;
            sprite = SpriteFactory.Instance.CreateUsedSprite();
            Size = new Point(16, 16);
            boundary = new Rectangle(location.ToPoint(), Size);
            ItemQuantity = 0;
            isVisible = true;
        }
        public Used(BaseBlock brickBlock, Vector2 location)
        {
            brickBlock = new QuestionBlock(location);
            sprite = SpriteFactory.Instance.CreateUsedSprite();
            Size = new Point(16, 16);
            boundary = new Rectangle(location.ToPoint(), Size);
            ItemQuantity = 0;
            isVisible = true;
        }

        public void changeToHidden()
        {
            isVisible = false;
        }

        public void changeToVisible()
        {
            isVisible = true;
        }
        public void changeToNormal()
        {
            questionBlock.state = new Normal(questionBlock, boundary.Location.ToVector2());
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
