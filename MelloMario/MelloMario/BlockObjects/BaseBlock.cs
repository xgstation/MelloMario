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
        public ISprite sprite { get; set; }
        private Boolean isVisible;
        private Boolean isUsed = false;
        public IBlockState state { get; set; }
        //Using Rectangle to record location and hitting boundary
        public Rectangle Boundary { get; set; }
        public BrickBlock(Vector2 location, Boolean isUsed)
        {
            this.isUsed = isUsed;
            if (!isUsed)
            {
                state = new BlockStates.Silent(this);
            }
            else
            {
                state = new BlockStates.Used(this);
            }
            Boundary = new Rectangle(location.ToPoint(), new Point(16, 16));
        }
        public void SetSpriteBasedOnState()
        {
            sprite = state.GetSprite();
        }
        public void Update(GameTime gameTime)
        {
            sprite.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            if (isVisible)
            {
                sprite.Draw(spriteBatch, location);
            }
        }
    }
}
