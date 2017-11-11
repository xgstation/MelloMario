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
        private int Coin;
        private int Score;
        private string firstLine;
        private string secondLine;
        public GameHUD(GameModel model, int startTime)
        {
            this.model = model;
            Coin = model.Coins;
            Score = model.Score;
            timeRemain = startTime * 1000;
            firstLine = "MARIO           WORLD    TIME";
            secondLine = Score.ToString().PadLeft(6, '0') + "    *" + Coin.ToString().PadLeft(2, '0') + "    " +
                         model.WorldIndex + "      " + timeRemain / 1000;
            UpdateSprite();
        }
        public int GetTimeRemain { get { return timeRemain / 1000; } }
        private void UpdateSprite()
        {
            textSprite = Factories.SpriteFactory.Instance.CreateTextSprite(firstLine + "\n" + secondLine);
        }
        public void Update(int time)
        {
            elapsed += time;
            if (elapsed > 50 || Coin != model.Coins || Score != model.Score)
            {
                Score = model.Score;
                Coin = model.Coins;
                secondLine = Score.ToString().PadLeft(6, '0') + "    *" + Coin.ToString().PadLeft(2, '0') + "    " +
                             model.WorldIndex + "      " + timeRemain / 1000;
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
