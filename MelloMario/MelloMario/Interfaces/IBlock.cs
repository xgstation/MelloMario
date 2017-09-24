using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario
{
    interface IBlock
    {
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch, Vector2 location);
    }
}
