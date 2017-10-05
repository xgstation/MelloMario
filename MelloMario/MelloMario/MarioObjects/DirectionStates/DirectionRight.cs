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
    class DirectionRight : IMarioDirectionState
    {
        Mario mario;
       // ISprite sprite;
        //bool setToStatic;
       
        public DirectionRight(Mario newMario)
        {
            mario = newMario;
            //setToStatic = true;
            //sprite = SpriteFactory.Instance.CreateMarioSprite("FireIdleLeft", setToStatic);
        }
        public void UpgradeToRightDirection()
        {
            
        }

        public void UpgradeToLeftDirection()
        {
            mario.State = new DirectionLeft(mario);
        }


        public void Update(GameTime game)
        {
            //sprite.Update(game);
        }
    }
}
