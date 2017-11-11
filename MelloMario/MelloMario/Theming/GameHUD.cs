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
        private ISprite coinSprite;
        private int elapsed;

        private int timeRemain;
        private int coins;
        private int score;
        private string firstLine;
        private string secondLine;
        public GameHUD(GameModel model)
        {
            this.model = model;
            timeRemain = model.Time;
            coins = model.Coins;
            score = model.Score;
            firstLine = "MARIO           WORLD    TIME";
            secondLine = score.ToString().PadLeft(6, '0') + "    *" + coins.ToString().PadLeft(2, '0') + "    " +
                         model.WorldIndex + "      " + timeRemain;
            coinSprite = Factories.SpriteFactory.Instance.CreateCoinSprite();
            UpdateTextSprite();
        }
        private void UpdateTextSprite()
        {
            textSprite = Factories.SpriteFactory.Instance.CreateTextSprite(firstLine + "\n" + secondLine);
        }
        public void Update(int time)
        {
            elapsed += time;
            if (elapsed > 50 || coins != model.Coins || score != model.Score)
            {
                score = model.Score;
                coins = model.Coins;
                timeRemain = model.Time;
                secondLine = score.ToString().PadLeft(6, '0') + "    *" + coins.ToString().PadLeft(2, '0') + "    " +
                             model.WorldIndex + "      " + timeRemain;
                UpdateTextSprite();
                elapsed = 0;
            }
        }

        public void Draw(int time)
        {
            textSprite.Draw(time, new Rectangle(42, 42, 800, 200), ZIndex.hud);
            coinSprite.Draw(time, new Rectangle(255, 74, 26, 30), ZIndex.item);
        }
    }
}
