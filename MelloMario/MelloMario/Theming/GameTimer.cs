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
        private int timeRemain;
        private double elpased;
        public GameTimer(int startTime)
        {
            timeRemain = startTime;
        }

        private void UpdateSprite()
        {
            timeSprite = Factories.SpriteFactory.Instance.CreateTextSprite(timeRemain.ToString());
        }
        public void Update(int time)
        {
            if (elpased <= 1)
            {
                elpased += time;
            }
            else
            {
                elpased = 0;
                UpdateSprite();
            }
            timeRemain -= time/1000;
            UpdateSprite();
        }

        public void Draw(int time)
        {
            timeSprite.Draw(time, new Rectangle(650, 0, 350, 350), ZIndex.hud);
        }
    }
}
