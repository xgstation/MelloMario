using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.Interfaces
{
    public interface ItemState
    {
        void transNormal();
        void transDefeated();
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch, Vector2 location);
    }
}
