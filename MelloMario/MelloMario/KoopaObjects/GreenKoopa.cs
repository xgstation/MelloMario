using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.KoopaObjects
{
    public class GreenKoopa
    {
        public Interfaces.IKoopaState greenKoopaState;
        public GreenKoopa()
        {
            greenKoopaState = new GreenKoopaNormalState(this);
        }

        public void transNormal()
        {
            greenKoopaState.transNormal();
        }
        public void transJumpedOn()
        {
            greenKoopaState.transJumpOn();
        }
        public void transDefeated()
        {
            greenKoopaState.transDefeated();
        }

        public void Update(GameTime gameTime)
        {
            greenKoopaState.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            greenKoopaState.Draw(spriteBatch, location);
        }

    }
}
