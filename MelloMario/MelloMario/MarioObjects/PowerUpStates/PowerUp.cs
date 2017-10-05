using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.MarioObjects.PowerUpStates
{
    public interface PowerUp
    {
        void UpgradeToPowerUpDead();
        void UpgradeToPowerUpFire();
        void UpgradeToPowerUpStandard();
        void UpgradeToPowerUpSuper();
        void Update(GameTime gameTime);
    }
}
