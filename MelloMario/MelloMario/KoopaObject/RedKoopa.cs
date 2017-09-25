using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.KoopaObject
{
    public class RedKoopa
    {
        public Interfaces.IKoopaState redKoopaState;
        public RedKoopa()
        {
            redKoopaState = new KoopaStates.RedKoopaNormalState(this);
        }

        public void transNormal()
        {
            redKoopaState.transNormal();
        }
        public void transJumpedOn()
        {
            redKoopaState.transJumpOn();
        }
        public void transDefeated()
        {
            redKoopaState.transDefeated();
        }

        public void Update(GameTime gameTime)
        {
            redKoopaState.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            redKoopaState.Draw(spriteBatch, location);
        }
    }
}
