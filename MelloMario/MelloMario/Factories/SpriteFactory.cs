using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelloMario.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MelloMario
{
    class SpriteFactory : ISpriteFactory
    {
        private static ISpriteFactory instance = new SpriteFactory();

        Dictionary<String, Texture2D> stringToMarioTexture;
        //player textures
        //dead
        private Texture2D Dead;
        //fire
        private Texture2D FireCrouchingLeft, FireIdleLeft, FireJumpingLeft, FireWalkingLeft;
        private Texture2D FireCrouchingRight, FireIdleRight, FireJumpingRight, FireWalkingRight;
        //super
        private Texture2D SuperCrouchingLeft,SuperIdleLeft, SuperJumpingLeft,SuperWalkingLeft;
        private Texture2D SuperCrouchingRight,SuperIdleRight, SuperJumpingRight, SuperWalkingRight;
        //standard
        private Texture2D StandardIdleLeft, StandardJumpingLeft, StandardWalkingLeft;
        private Texture2D StandardIdleRight, StandardJumpingRight, StandardWalkingRight;

        private Texture2D goombaSpritesheet;
        private Texture2D greenKoopaSpritesheet;
        private Texture2D redKoopaSpritesheet;
        private Texture2D superMushroomSpritesheet;
        private Texture2D oneUpMushroomSpritesheet;
        private Texture2D coinSpritesheet;
        private Texture2D fireFlowerSpritesheet;
        private Texture2D starSpritesheet;

        public SpriteFactory()
        {
        }

        public static ISpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

        public void LoadAllTextures(ContentManager content)
        {
            // TODO: Implement lazy-loading with singleton pattern?
            goombaSpritesheet = content.Load<Texture2D>("goomba");
            greenKoopaSpritesheet = content.Load<Texture2D>("greenKoopa");
            redKoopaSpritesheet = content.Load<Texture2D>("redKoopa");
            superMushroomSpritesheet = content.Load<Texture2D>("superMushroom");
            oneUpMushroomSpritesheet = content.Load<Texture2D>("1-UpMushroom");
            coinSpritesheet = content.Load<Texture2D>("coin");
            fireFlowerSpritesheet = content.Load<Texture2D>("fireFlower");
            starSpritesheet = content.Load<Texture2D>("star");
            //dead
            Dead = content.Load<Texture2D>("Dead");
            //fire
            FireCrouchingLeft = content.Load<Texture2D>("FireCrouchingLeft");
            FireIdleLeft = content.Load<Texture2D>("FireIdleLeft");
            FireWalkingLeft = content.Load<Texture2D>("FireWalkingLeft");
            FireJumpingLeft = content.Load<Texture2D>("FireJumpingLeft");
            FireCrouchingRight = content.Load<Texture2D>("FireCrouchingRight");
            FireIdleRight = content.Load<Texture2D>("FireIdleRight");
            FireWalkingRight = content.Load<Texture2D>("FireWalkingRight");
            FireJumpingRight = content.Load<Texture2D>("FireJumpingRight");
            //standard
            StandardIdleLeft = content.Load<Texture2D>("StandardIdleLeft");
            StandardWalkingLeft = content.Load<Texture2D>("StandardWalkingLeft");
            StandardJumpingLeft = content.Load<Texture2D>("StandardJumpingLeft");
            StandardIdleRight = content.Load<Texture2D>("StandardIdleRight");
            StandardWalkingRight = content.Load<Texture2D>("StandardWalkingRight");
            StandardJumpingRight = content.Load<Texture2D>("StandardJumpingRight");
            //super
            SuperCrouchingLeft = content.Load<Texture2D>("SuperCrouchingLeft");
            SuperIdleLeft = content.Load<Texture2D>("SuperIdleLeft");
            SuperWalkingLeft = content.Load<Texture2D>("SuperWalkingLeft");
            SuperJumpingLeft = content.Load<Texture2D>("SuperJumpingLeft");
            SuperCrouchingRight = content.Load<Texture2D>("SuperCrouchingRight");
            SuperIdleRight = content.Load<Texture2D>("SuperIdleRight");
            SuperWalkingRight = content.Load<Texture2D>("SuperWalkingRight");
            SuperJumpingRight = content.Load<Texture2D>("SuperJumpingRight");
            //dictionary
            stringToMarioTexture = new Dictionary<String, Texture2D>
            {
                {"FireCrouchingLeft",FireCrouchingLeft},{"FireIdleLeft",FireIdleLeft}, {"FireJumpingLeft",FireJumpingLeft},{"FireWalkingLeft",FireWalkingLeft},
                {"FireCrouchingRight",FireCrouchingRight},{"FireIdleRight",FireIdleRight},{"FireJumpingRight",FireJumpingRight},{"FireWalkingRight",FireWalkingRight},
                {"SuperCrouchingLeft",SuperCrouchingLeft},{"SuperIdleLeft",SuperIdleLeft},{"SuperJumpingLeft",SuperJumpingLeft},{"SuperWalkingLeft",SuperWalkingLeft},
                {"SuperCrouchingRight",SuperCrouchingRight},{"SuperIdleRight",SuperIdleRight},{"SuperJumpingRight",SuperJumpingRight},{"SuperWalkingRight",SuperWalkingRight},
                {"StandardIdleLeft",StandardIdleLeft},{"StandardJumpingLeft",StandardJumpingLeft},{"StandardWalkingLeft",StandardWalkingLeft},
                {"StandardIdleRight",StandardIdleRight},{"StandardJumpingRight",StandardJumpingRight},{"StandardWalkingRight",StandardWalkingRight},
            };

        }
        
        public ISprite CreateMarioSprite(string status, bool Static)
        {
            //change
            
            ISprite sprite;
            
            if (Static)
            {
                sprite = new StaticSprite(stringToMarioTexture[status]);
            }
            else
            {
                // animated
                // add additional parameters when motion is involved
                sprite = new AnimatedSprite(stringToMarioTexture[status],3,3);
            }
            return sprite;
        }

        public ISprite CreateGoombaSprite(string status)
        {
            switch (status)
            {
                case "Normal":
                    return new GoombaSprite(goombaSpritesheet, 1, 3, false);
                case "Defeated":
                    return new GoombaSprite(goombaSpritesheet, 1, 3, true);
                default:
                    throw new Exception("Unknown sprite");
            }
        }
        
        public ISprite CreateRedKoopaSprite(string status)
        {
            switch (status)
            {
                case "Normal":
                    return new RedKoopaSprite(redKoopaSpritesheet, 3, 2, false, false);
                case "JumpOn":
                    return new RedKoopaSprite(redKoopaSpritesheet, 3, 2, true, false);
                case "Defeated":
                    return new RedKoopaSprite(redKoopaSpritesheet, 3, 2, false, true);
                default:
                    throw new Exception("Unknown sprite");
            }
        }

        public ISprite CreateGreenKoopaSprite(string status)
        {
            switch (status)
            {
                case "Normal":
                    return new GreenKoopaSprite(greenKoopaSpritesheet, 3, 2, false, false);
                case "JumpOn":
                    return new GreenKoopaSprite(greenKoopaSpritesheet, 3, 2, true, false);
                case "Defeated":
                    return new GreenKoopaSprite(greenKoopaSpritesheet, 3, 2, false, true);
                default:
                    throw new Exception("Unknown sprite");
            }
        }

        public ISprite CreatCoinSprite()
        {
            return new CoinSprite(coinSpritesheet, 1, 4);
        }

        public ISprite CreateStarSprite()
        {
            return new StarSprite(starSpritesheet, 1, 4);
        }

        public ISprite CreateFlowerSprite()
        {
            return new StarSprite(fireFlowerSpritesheet, 1, 8);
        }

        public ISprite CreatSuperMushroomSprite()
        {
            return new superMushroomSprite(superMushroomSpritesheet);
        }

        public ISprite CreateOneUpMushroomSprite()
        {
            return new oneUpshroomSprite(oneUpMushroomSpritesheet);
        }
    }
}
