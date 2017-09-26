using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MelloMario.BlockObjects.BrickStates;

namespace MelloMario.BlockObjects
{
    public class BrickBlock : BaseBlock
    {
        public IBlockState State;

        public BrickBlock(Vector2 location) : base(location)
        {
            State = new BrickSilent(this);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            State.Draw(spriteBatch, location);
        }

        public override void Update(GameTime gameTime)
        {
            State.Update(gameTime);
        }
    }
}
