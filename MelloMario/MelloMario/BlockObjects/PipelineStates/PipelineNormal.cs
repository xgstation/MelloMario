using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.BlockObjects.PipelineStates
{
    class PipelineNormal : IBlockState
    {
        private PipelineBlock block;
        private ISprite sprite;

        public PipelineNormal(PipelineBlock block)
        {
            this.block = block;
            sprite = SpriteFactory.Instance.CreatePipeline();
        }

        public void Bump()
        {
            //do nothing
        }

        public void Destroy()
        {
            //do nothing
        }

        public void Hide()
        {
            //do nothing
        }

        public void Show()
        {
            //do nothing
        }

        public void UseUp()
        {
            //do nothing
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
