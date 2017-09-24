using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.Blocks
{
    class BrickBlock : IBlock
    {
        private ISprite sprite;
        public IBlockState state { get; set; }
        public BrickBlock ()
        {
            this.state = new BlockStates.Silent();
            //TODO : Constructor for Sprite
        }
        public BrickBlock (IBlockState state)
        {
            this.state = state;
            //TODO : Constructor for Sprite
        }

        public void Update()
        {
            sprite.Update();
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch, location);
        }
    }
}
