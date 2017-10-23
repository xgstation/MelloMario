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
using MelloMario.BlockObjects;
using MelloMario.Sprites.BlockSprites;

namespace MelloMario.Factories
{
    class SpriteFactory : ISpriteFactory
    {
        private static ISpriteFactory instance = new SpriteFactory();

        IDictionary<String, Texture2D> stringToMarioTexture;

        private SpriteBatch spriteBatch;

        private Texture2D goombaSpritesheet;
        private Texture2D goombaDeadSpritesheet;
        private Texture2D greenKoopaSpritesheet;
        private Texture2D greenKoopaSteppedSpritesheet;
        private Texture2D greenKoopaLeft;
        private Texture2D greenKoopaRight;
        private Texture2D greenKoopaDeadSpritesheet;
        private Texture2D redKoopaSpritesheet;
        private Texture2D redKoopaLeft;
        private Texture2D redKoopaRight;
        private Texture2D redKoopaSteppedSpritesheet;
        private Texture2D redKoopaDeadSpritesheet;
        private Texture2D superMushroomSpritesheet;
        private Texture2D oneUpMushroomSpritesheet;
        private Texture2D coinSpritesheet;
        private Texture2D fireFlowerSpritesheet;
        private Texture2D starSpritesheet;

        private Texture2D blockSpritesheet;
        private Texture2D brickPieceSpritesheet;
        private Texture2D questionSpriteSheet;

        private SpriteFactory()
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
            blockSpritesheet = content.Load<Texture2D>("BlockSheet");
            brickPieceSpritesheet = content.Load<Texture2D>("BrickPieces");
            questionSpriteSheet = content.Load<Texture2D>("Question");
            // TODO: Implement lazy-loading with singleton pattern?

            //these files are not in the repository, i am assigning all of them to an existing
            //texture so we can compile add the textures and restore this chunk
            goombaSpritesheet = content.Load<Texture2D>("Goomba");
            goombaDeadSpritesheet = content.Load<Texture2D>("GoombaDead");
            greenKoopaSpritesheet = content.Load<Texture2D>("GreenKoopa");
            greenKoopaLeft = content.Load<Texture2D>("GreenKoopaLeft");
            greenKoopaRight = content.Load<Texture2D>("GreenKoopaRight");
            greenKoopaSteppedSpritesheet = content.Load<Texture2D>("GreenKoopaStepped");
            greenKoopaDeadSpritesheet = content.Load<Texture2D>("GreenKoopaDead");
            redKoopaSpritesheet = content.Load<Texture2D>("RedKoopa");
            redKoopaLeft = content.Load<Texture2D>("RedKoopaLeft");
            redKoopaRight = content.Load<Texture2D>("RedKoopaRight");
            redKoopaSteppedSpritesheet = content.Load<Texture2D>("RedKoopaStepped");
            redKoopaDeadSpritesheet = content.Load<Texture2D>("RedKoopaDead");
            superMushroomSpritesheet = content.Load<Texture2D>("SuperMushroom");
            oneUpMushroomSpritesheet = content.Load<Texture2D>("OneUpMushroom");
            coinSpritesheet = content.Load<Texture2D>("Coin");
            fireFlowerSpritesheet = content.Load<Texture2D>("FireFlower");
            starSpritesheet = content.Load<Texture2D>("Star");

            //dictionary
            stringToMarioTexture = new Dictionary<String, Texture2D>
            {
                { "Dead", content.Load<Texture2D>("Dead")},
                { "FireCrouchingLeft",content.Load<Texture2D>("FireCrouchingLeft")},{"FireStandingLeft",content.Load<Texture2D>("FireStandingLeft")},
                { "FireJumpingLeft",content.Load<Texture2D>("FireJumpingLeft")},{"FireWalkingLeft",content.Load<Texture2D>("FireWalkingLeft")},
                { "FireCrouchingRight",content.Load<Texture2D>("FireCrouchingRight")},{"FireStandingRight",content.Load<Texture2D>("FireStandingRight")},
                { "FireJumpingRight",content.Load<Texture2D>("FireJumpingRight")},{"FireWalkingRight",content.Load<Texture2D>("FireWalkingRight")},
                { "StandardStandingLeft",content.Load<Texture2D>("StandardStandingLeft")},{"StandardJumpingLeft",content.Load<Texture2D>("StandardJumpingLeft")},
                { "StandardWalkingLeft",content.Load<Texture2D>("StandardWalkingLeft")},{"StandardStandingRight",content.Load<Texture2D>("StandardStandingRight")},
                { "StandardJumpingRight",content.Load<Texture2D>("StandardJumpingRight")},{"StandardWalkingRight",content.Load<Texture2D>("StandardWalkingRight")},
                { "SuperCrouchingLeft",content.Load<Texture2D>("SuperCrouchingLeft")},{"SuperStandingLeft",content.Load<Texture2D>("SuperStandingLeft")},
                { "SuperJumpingLeft",content.Load<Texture2D>("SuperJumpingLeft")},{"SuperWalkingLeft",content.Load<Texture2D>("SuperWalkingLeft")},
                { "SuperCrouchingRight",content.Load<Texture2D>("SuperCrouchingRight")},{"SuperStandingRight",content.Load<Texture2D>("SuperStandingRight")},
                { "SuperJumpingRight",content.Load<Texture2D>("SuperJumpingRight")},{"SuperWalkingRight",content.Load<Texture2D>("SuperWalkingRight")},
            };
        }

        public void BindSpriteBatch(SpriteBatch newSpriteBatch)
        {
            spriteBatch = newSpriteBatch;
        }

        public ISprite CreateMarioSprite(string status, bool isStatic)
        {
            ISprite sprite;

            if (isStatic)
            {
                sprite = new StaticSprite(spriteBatch, stringToMarioTexture[status]);
            }
            else
            {
                sprite = new AnimatedSprite(spriteBatch, stringToMarioTexture[status], 3, 1);
            }

            return sprite;
        }

        public ISprite CreateGoombaSprite(string status)
        {
            switch (status)
            {
                case "Defeated":
                    return new StaticSprite(spriteBatch, goombaDeadSpritesheet);
                case "Normal":
                    return new AnimatedSprite(spriteBatch, goombaSpritesheet, 2, 1);
                default:
                    //it should never hit this case, if it does there is an error somewhere
                    //else in the code
                    return null;
            }
        }

        public ISprite CreateGreenKoopaSprite(string status)
        {
            switch (status)
            {
                case "DefeatedLeft":
                case "DefeatedRight":
                    return new StaticSprite(spriteBatch, greenKoopaDeadSpritesheet);
                case "ShellLeft":
                case "ShellRight":
                    return new StaticSprite(spriteBatch, greenKoopaSteppedSpritesheet);
                case "NormalLeft":
                    return new AnimatedSprite(spriteBatch, greenKoopaLeft, 2, 1);
                case "NormalRight":
                    return new AnimatedSprite(spriteBatch, greenKoopaRight, 2, 1);
                default:
                    //it should never hit this case, if it does there is an error somewhere
                    //else in the code
                    return null;
            }
        }

        public ISprite CreateRedKoopaSprite(string status)
        {
            switch (status)
            {
                case "DefeatedLeft":
                case "DefeatedRight":
                    return new StaticSprite(spriteBatch, redKoopaDeadSpritesheet);
                case "ShellLeft":
                case "ShellRight":
                    return new StaticSprite(spriteBatch, redKoopaSteppedSpritesheet);
                case "NormalLeft":
                    return new AnimatedSprite(spriteBatch, redKoopaLeft, 2, 1);
                case "NormalRight":
                    return new AnimatedSprite(spriteBatch, redKoopaRight, 2, 1);
                default:
                    //it should never hit this case, if it does there is an error somewhere
                    //else in the code
                    return null;
            }
        }

        public ISprite CreateStarSprite()
        {
            return new AnimatedSprite(spriteBatch, starSpritesheet, 4, 1);
        }

        public ISprite CreateCoinSprite()
        {
            return new AnimatedSprite(spriteBatch, coinSpritesheet, 4, 1);
        }

        public ISprite CreateSuperMushroomSprite()
        {
            return new StaticSprite(spriteBatch, superMushroomSpritesheet);
        }

        public ISprite CreateFireFlowerSprite()
        {
            return new AnimatedSprite(spriteBatch, fireFlowerSpritesheet, 8, 1);
        }

        public ISprite CreateOneUpMushroomSprite()
        {
            return new StaticSprite(spriteBatch, oneUpMushroomSpritesheet);
        }

        public ISprite CreateQuestionSprite(string status)
        {
            switch (status)
            {
                case "Bumped":
                    return new AnimatedSprite(spriteBatch, questionSpriteSheet, 3, 1, ZIndex.front);
                case "Normal":
                    return new AnimatedSprite(spriteBatch, questionSpriteSheet, 3, 1, ZIndex.front);
                case "Used":
                    return new SlicedSprite(spriteBatch, blockSpritesheet, 33, 28, 27, 0, ZIndex.front);
                default:
                    //it should never hit this case, if it does there is an error somewhere
                    //else in the code
                    return null;
            }
        }

        public ISprite CreateBrickSprite(string status)
        {
            switch (status)
            {
                case "Bumped":
                    return new SlicedSprite(spriteBatch, blockSpritesheet, 33, 28, 1, 0, ZIndex.front);
                case "Destroyed":
                    return new BrickPieceSprite(spriteBatch, brickPieceSpritesheet);
                case "Normal":
                    return new SlicedSprite(spriteBatch, blockSpritesheet, 33, 28, 1, 0, ZIndex.front);
                case "Used":
                    return new SlicedSprite(spriteBatch, blockSpritesheet, 33, 28, 27, 0, ZIndex.front);
                default:
                    //it should never hit this case, if it does there is an error somewhere
                    //else in the code
                    return null;
            }
        }

        public ISprite CreateFloorSprite()
        {
            return new SlicedSprite(spriteBatch, blockSpritesheet, 33, 28, 0, 0, ZIndex.front);
        }

        public ISprite CreateStairSprite()
        {
            return new SlicedSprite(spriteBatch, blockSpritesheet, 33, 28, 0, 1, ZIndex.front);
        }

        public ISprite CreatePipelineSprite(String type)
        {
            switch (type)
            {
                case "LeftIn":
                    return new SlicedSprite(spriteBatch, blockSpritesheet, 33, 28, 0, 8, ZIndex.front);
                case "RightIn":
                    return new SlicedSprite(spriteBatch, blockSpritesheet, 33, 28, 1, 8, ZIndex.front);
                case "Left":
                    return new SlicedSprite(spriteBatch, blockSpritesheet, 33, 28, 0, 9, ZIndex.front);
                case "Right":
                    return new SlicedSprite(spriteBatch, blockSpritesheet, 33, 28, 1, 9, ZIndex.front);
                default:
                    //it should never hit this case, if it does there is an error somewhere
                    //else in the code
                    return null;
            }
        }
    }
}
