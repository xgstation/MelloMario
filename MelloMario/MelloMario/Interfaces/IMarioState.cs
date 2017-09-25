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
    interface IMarioState
    {
        void down();
        void idle();
        void up();
        void right();
        void left();
        void die();
        void changeToFireState();
        void changeToSuperState();
        void changeToStandardState();
        void Update(GameTime game);
        void Draw(SpriteBatch spriteBatch, Vector2 location);
    }
}
