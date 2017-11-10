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
        private int timeRemain; //in mileSeconds
        public GameTimer(int startTime)
        {
            timeRemain = startTime * 1000;
        }

        private void UpdateSprite()
        {
            timeSprite = Factories.SpriteFactory.Instance.CreateTextSprite((timeRemain/1000).ToString());
        }
        public void Update(int time)
        {
            timeRemain -= time;
            UpdateSprite();
        }

        public void Draw(int time)
        {
            timeSprite.Draw(time, new Rectangle(650, 0, 350, 350), ZIndex.hud);
        }
    }
}
