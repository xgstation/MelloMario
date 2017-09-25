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
    public class RedKoopa : BaseEnemy
    {
        public IKoopaState redKoopaState;
        public RedKoopa(Vector2 initLocation) : base(initLocation)
        {
            redKoopaState = new RedKoopaNormal(this);
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

        public override void Update(GameTime gameTime)
        {
            redKoopaState.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            redKoopaState.Draw(spriteBatch, Location);
        }
    }
}
