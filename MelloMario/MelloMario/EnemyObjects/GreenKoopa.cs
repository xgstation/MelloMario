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
    class GreenKoopa : BaseEnemy
    {
        public IKoopaState State;

        public GreenKoopa(Vector2 initLocation) : base(initLocation)
        {
            State = new GreenKoopaNormal(this);
        }

        public void ChangeToNormal()
        {
            State.ChangeToNormal();
        }
        public void ChangeToJumpedOn()
        {
            State.ChangeToJumpOn();
        }
        public void ChangeToDefeated()
        {
            State.ChangeToDefeated();
        }

        public override void Update(GameTime time)
        {
            State.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            State.Draw(spriteBatch, Location);
        }

    }
}
