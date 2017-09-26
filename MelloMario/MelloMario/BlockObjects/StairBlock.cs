using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MelloMario.BlockObjects.StairStates;

namespace MelloMario.BlockObjects
{
    public class StairBlock : BaseBlock
    {
        public IBlockState State;

        public StairBlock(Vector2 location) : base(location)
        {
            State = new StairSilent(this);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            State.Draw(spriteBatch,location);
        }

        public override void Update(GameTime gameTime)
        {
            State.Update(gameTime);
        }
    }
}
