using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace MelloMario.BlockObjects
{
    class PipelineBlock : BaseBlock
    {
        public IBlockState State;

        public PipelineBlock(Vector2 location) : base(location)
        {
            State = new PipelineStates.PipelineSilent(this);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            State.Draw(spriteBatch, location);
        }

        public override void Update(GameTime time)
        {
            State.Update(gameTime);
        }
    }
}
