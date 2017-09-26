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
        public IKoopaState State;

        public RedKoopa(Vector2 initLocation) : base(initLocation)
        {
            State = new RedKoopaNormal(this);
        }

        public void transNormal()
        {
            State.transNormal();
        }
        public void transJumpedOn()
        {
            State.transJumpOn();
        }
        public void transDefeated()
        {
            State.transDefeated();
        }

        public override void Update(GameTime gameTime)
        {
            State.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            State.Draw(spriteBatch, Location);
        }
    }
}
