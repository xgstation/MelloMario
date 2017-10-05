using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.MarioObjects.MovementStates
{
    public interface Movement
    {
        void UpgradeToMovementCrouching();
        void UpgradeToMovementldle();
        void UpgradeToMovementJumping();
        void UpgradeToMovementWalking();
        void Update(GameTime game);
    }
}
