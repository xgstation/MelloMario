using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.MarioObjects.DirectionStates
{
    public interface Direction
    {
        void UpgradeToRightDirection();
        void UpgradeToLeftDirection();
        void Update(GameTime game);
    }
}
