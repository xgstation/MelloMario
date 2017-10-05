using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MelloMario.MarioObjects.DirectionStates
{
    class DirectionLeft : IMarioDirectionState
    {
        Mario mario;

        public DirectionLeft(Mario newMario)
        {
            mario = newMario;
            //setToStatic = true;
            //sprite = SpriteFactory.Instance.CreateMarioSprite("FireIdleLeft", setToStatic);
        }
        public void UpgradeToRightDirection()
        {
            mario.State = new DirectionRight(mario);
        }

        public void UpgradeToLeftDirection()
        {

        }


        public void Update(GameTime time)
        {
            //sprite.Update(game);
        }

    }
}
