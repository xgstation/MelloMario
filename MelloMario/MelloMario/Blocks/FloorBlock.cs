using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.Blocks
{
    class FloorBlock
    {
        private ISprite sprite;
        public IBlockState state { get; set; }
        public FloorBlock()
        {
            this.state = new BlockStates.Silent();
            //TODO : Constructor for Sprite
        }
        public FloorBlock(IBlockState state)
        {
            this.state = state;
            //TODO : Constructor for Sprite
        }
        public void Update() { }
        public void Draw(SpriteBatch spriteBatch, Vector2 location) { }
    }
}
