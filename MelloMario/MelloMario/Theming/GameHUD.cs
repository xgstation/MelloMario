using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MelloMario.Theming
{
    class GameHUD
    {
        private GameModel model;

        private ISprite textSprite;
        private int timeRemain; //in mileSeconds
        private int elapsed;

        private string firstLine;
        public GameHUD(GameModel model, int startTime)
        {
            this.model = model;
            firstLine = "MARIO           WORLD    TIME";
            timeRemain = startTime * 1000;
            UpdateSprite();
        }
        public int GetTimeRemain { get { return timeRemain / 1000; } }
        private void UpdateSprite()
        {
            string combined = model.Score.ToString().PadLeft(6, '0') + "    *" + model.Coins.ToString().PadLeft(2, '0') + "    " + model.WorldIndex + "      " + timeRemain / 1000;
            textSprite = Factories.SpriteFactory.Instance.CreateTextSprite(firstLine + "\n" + combined);
        }
        public void Update(int time)
        {
            elapsed += time;
            if (elapsed >= 50)
            {
                UpdateSprite();
                elapsed = 0;
            }
            timeRemain -= time;
        }

        public void Draw(int time)
        {
            textSprite.Draw(time, new Rectangle(42, 42, 800, 200), ZIndex.hud);
        }
    }
}
