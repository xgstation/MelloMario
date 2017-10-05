﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.EnemyObjects.KoopaStates
{
    class GreenKoopaNormal : IKoopaState
    {
        private ISprite greenKoopa;
        private GreenKoopa enemyGreenKoopa;
        public GreenKoopaNormal(GreenKoopa koopaGreen)
        {
            enemyGreenKoopa = koopaGreen;
            greenKoopa = SpriteFactory.Instance.CreateGreenKoopaSprite("Normal");
        }
        public void ChangeToNormal()
        {

        }

        public void ChangeToJumpOn()
        {
            enemyGreenKoopa.State = new GreenKoopaJumpOn(enemyGreenKoopa);
        }

        public void ChangeToDefeated()
        {
            enemyGreenKoopa.State = new GreenKoopaDefeated(enemyGreenKoopa);
        }

        public void Update(GameTime time)
        {
            greenKoopa.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            greenKoopa.Draw(spriteBatch, location);
        }
    }
}
