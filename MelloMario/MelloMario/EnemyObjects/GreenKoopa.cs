using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelloMario.EnemyObjects.KoopaStates;

namespace MelloMario.EnemyObjects
{
    public class GreenKoopa : BaseEnemy
    {
        public IKoopaState greenKoopaState;
        public GreenKoopa(Vector2 initLocation) : base(initLocation)
        {
            greenKoopaState = new GreenKoopaNormal(this);
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

        public override void Update(GameTime gameTime)
        {
            greenKoopaState.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            greenKoopaState.Draw(spriteBatch, Location);
        }

    }
}
