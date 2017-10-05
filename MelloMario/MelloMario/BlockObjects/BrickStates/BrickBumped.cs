using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MelloMario.Factories;

namespace MelloMario.BlockObjects.BrickStates
{
    class BrickBumped : IBlockState
    {
        private Brick block;
        private ISprite sprite;
        private float elapsed;
        private float speedY = -7f;
        private float accelerationY = 1.5f;
        private float offset;
        public BrickBumped(Brick brickBlock)
        {
            block = brickBlock;
            sprite = SpriteFactory.Instance.CreateBrickSprite("Normal");
        }

        public void Show()
        {
            block.State = new BrickNormal(block);
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
            // Do nothing
        }

        public void Destroy()
        {
            block.State = new BrickDestroyed(block);
        }

        public void Update(GameTime time)
        {
            elapsed += ((float)gameTime.ElapsedGameTime.Milliseconds) / 20;
            offset = 0.5f * accelerationY * (float)Math.Pow(elapsed, 2.0f) + speedY * elapsed;

            if (offset >= 0)
            {
                Show();
            }
        }
    }
}
