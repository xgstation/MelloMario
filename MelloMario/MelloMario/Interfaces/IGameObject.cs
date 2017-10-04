using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario
{
    public interface IGameObject
    {
        //Rectangle Boundary();

        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}
