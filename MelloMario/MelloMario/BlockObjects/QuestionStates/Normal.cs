using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.BlockObjects.QuestionStates
{
    class Normal : IBlockState
    {
        public QuestionBlock questionBlock { get; set; }
        public ISprite sprite { get; set; }
        //Hitting size of Brick: 16x16 pixels
        public Point Size { get; set; }
        public int ItemQuantity { get; set; }
        public Rectangle boundary { get; set; }
        public Boolean isVisible { get; set; }
        public Normal(QuestionBlock questionBlock, Vector2 location)
        {
            this.questionBlock = questionBlock;
            sprite = SpriteFactory.Instance.CreateQuestionSprite();
            Size = new Point(16, 16);
            boundary = new Rectangle(location.ToPoint(), Size);
            ItemQuantity = 1;
            isVisible = true;
        }
        public Normal(QuestionBlock questionBlock, Vector2 location, int ItemQuantity) : this(questionBlock, location)
        {
            this.ItemQuantity = ItemQuantity;
        }
        public Normal(QuestionBlock questionBlock, Vector2 location, Boolean isVisible) : this(questionBlock, location)
        {
            this.isVisible = isVisible;
        }
        public void changeToBumped()
        {
            questionBlock.state = new Bumped(questionBlock, boundary.Location.ToVector2());
        }

        public void changeToHidden()
        {
            isVisible = false;
        }

        public void changeToVisible()
        {
            isVisible = true;
        }

        public void changeToUsed()
        {
            questionBlock.state = new QuestionStates.Used(questionBlock, boundary.Location.ToVector2());
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
