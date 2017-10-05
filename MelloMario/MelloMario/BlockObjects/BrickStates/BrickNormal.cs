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
    class BrickNormal : IBlockState
    {
        private Brick block;
        private ISprite sprite;

        public BrickNormal(Brick block)
        {
            this.block = block;
            sprite = SpriteFactory.Instance.CreateBrickSprite("Normal");
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

        public void Update(GameTime time)
        {
            sprite.Update(gameTime);
        }
    }
}
