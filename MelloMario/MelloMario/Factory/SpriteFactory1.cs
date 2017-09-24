using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sprite
{
    class SpriteFactory
    {
        private Texture2D goombaSpritesheet;
        private Texture2D greenKoopaSpritesheet;
        private Texture2D redKoopaSpritesheet;
        private Texture2D superMushroomSpritesheet;
        private Texture2D oneUpMushroomSpritesheet;
        private Texture2D coinSpritesheet;
        private Texture2D fireFlowerSpritesheet;
        private Texture2D starSpritesheet;
        private static SpriteFactory instance = new SpriteFactory();
        public static SpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }
        public void LoadAllTexture(ContentManager content)
        {
            goombaSpritesheet= content.Load<Texture2D>("goomba");
            greenKoopaSpritesheet= content.Load<Texture2D>("greenKoopa");
            redKoopaSpritesheet= content.Load<Texture2D>("redKoopa");
            superMushroomSpritesheet = content.Load<Texture2D>("superMushroom");
            oneUpMushroomSpritesheet= content.Load<Texture2D>("1-UpMushroom");
            coinSpritesheet = content.Load<Texture2D>("coin");
            fireFlowerSpritesheet = content.Load<Texture2D>("fireFlower");
            starSpritesheet= content.Load<Texture2D>("star");
        }

        public ISprite CreateGoombaSprite()
        {
            return new GoombaSprite(goombaSpritesheet, 1, 2);
        }

        public ISprite CreateKoopaSprite()
        {
            return new KoopaSprite(greenKoopaSpritesheet, redKoopaSpritesheet, 1, 6, 3, 4);
        }

        public ISprite CreateItemSprite()
        {
            return new ItemSprite(superMushroomSpritesheet, oneUpMushroomSpritesheet, starSpritesheet, fireFlowerSpritesheet, coinSpritesheet, 1, 4);
        }
    }
}
