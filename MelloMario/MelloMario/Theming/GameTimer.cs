using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MelloMario.Theming
{
    class GameTimer
    {
        private ISprite timeSprite;
        private float timeRemain;
        public GameTimer(float startTime)
        {
            timeRemain = startTime;
        }

        private void UpdateSprite()
        {
            
        }
        public void Update(GameTime time)
        {
            timeRemain = time.ElapsedGameTime.Milliseconds;

        }
        
    }
}
