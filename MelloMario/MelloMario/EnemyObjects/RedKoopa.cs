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
    class RedKoopa : BaseEnemy
    {
        public IKoopaState State;

        public RedKoopa(Vector2 initLocation) : base(initLocation)
        {
            State = new RedKoopaNormal(this);
        }

        public void ChangeToNormal()
        {
            State.Show();
        }
        public void ChangeToJumpedOn()
        {
            State.JumpOn();
        }
        public void ChangeToDefeated()
        {
            State.Defeat();
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
