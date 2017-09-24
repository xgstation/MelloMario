using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.BlockObjects
{
    class QuestionBlock : IBlock
    {
        private ISprite sprite;
        private Boolean isUsed;
        public IBlockState state { get; set; }
        //Using Rectangle to record location and hitting boundary
        public Rectangle Boundary { get; set; }

        public QuestionBlock(Vector2 location, Boolean isUsed)
        {
            this.isUsed = isUsed;
            if (!isUsed)
            {
                this.state = new BlockStates.Silent(this);
            }
            else
            {
                this.state = new BlockStates.Used(this);
            }
            this.Boundary = new Rectangle(location.ToPoint(), new Point(16, 16));
        }
        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            throw new NotImplementedException();
        }
    }
}
