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
        public IKoopaState State;

        public GreenKoopa(Vector2 initLocation) : base(initLocation)
        {
            State = new GreenKoopaNormal(this);
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
