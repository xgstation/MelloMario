﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MelloMario.EnemyObjects.GoombaStates;

namespace MelloMario.EnemyObjects
{
    class Goomba: BaseEnemy
    {
        public IGoombaState State;

        public Goomba(Vector2 initLocation) : base(initLocation)
        {
            State = new GoombaNormal(this);
        }

        public void ChangeToNormal()
        {
            State.ChangeToNormal();
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
