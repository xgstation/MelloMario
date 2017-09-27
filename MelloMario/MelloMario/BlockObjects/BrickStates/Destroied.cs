using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.BlockObjects.BrickStates
{
    class Destroied : IBlockState
    {
        private BrickBlock brickBlock;
        private ISprite []sprites;
        private Boolean isVisible;
        private Vector2 origin;
        public Destroied(BrickBlock brickBlock, Vector2 location)
        {
            this.brickBlock = brickBlock;
            origin = location;
            sprites = new ISprite[4]; 
            sprites[0] = SpriteFactory.Instance.CreateBrickPieceSprite(Sprites.BrickPieceSprite.Part.LeftBottom);
            sprites[1] = SpriteFactory.Instance.CreateBrickPieceSprite(Sprites.BrickPieceSprite.Part.LeftTop);
            sprites[2] = SpriteFactory.Instance.CreateBrickPieceSprite(Sprites.BrickPieceSprite.Part.RightBottom);
            sprites[3] = SpriteFactory.Instance.CreateBrickPieceSprite(Sprites.BrickPieceSprite.Part.RightTop);
            isVisible = true;
        }

        public Destroied(BrickBlock brickblock,Vector2 location, Boolean isVisible) : this(brickblock, location)
        {
            this.isVisible = isVisible;
        } 
        public void changeToNomral()
        {
            brickBlock.state = new Normal(brickBlock, origin);
        }

        public void changeToHidden()
        {
            isVisible = false;
        }

        public void changeToUsed()
        {
            brickBlock.state = new QuestionStates.Used(brickBlock, origin);
        }

        public void Update(GameTime gameTime)
        {
            foreach (ISprite sprite in sprites)
                sprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (ISprite sprite in sprites)
                sprite.Draw(spriteBatch, origin);
        }

        public Rectangle GetBoundary()
        {
            return new Rectangle(origin.ToPoint(), new Point(0, 0));
        }
    }
}
