using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario
{
    public interface ISprite
    {
        void Update(GameTime game);
        void Draw(SpriteBatch spriteBatch, Vector2 location);
    }
}
