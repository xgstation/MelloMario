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

        Dictionary<String, Texture2D> stringToMarioTexture;

        private Texture2D goombaSpritesheet;
        private Texture2D goombaDeadSpritesheet;
        private Texture2D greenKoopaSpritesheet;
        private Texture2D greenKoopaSteppedSpritesheet;
        private Texture2D greenKoopaDeadSpritesheet;
        private Texture2D redKoopaSpritesheet;
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
            greenKoopaSteppedSpritesheet = content.Load<Texture2D>("GreenKoopaStepped");
            greenKoopaDeadSpritesheet = content.Load<Texture2D>("GreenKoopaDead");
            redKoopaSpritesheet = content.Load<Texture2D>("RedKoopa");
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
                { "FireCrouchingLeft",content.Load<Texture2D>("FireCrouchingLeft")},{"FireIdleLeft",content.Load<Texture2D>("FireIdleLeft")},
                { "FireJumpingLeft",content.Load<Texture2D>("FireJumpingLeft")},{"FireWalkingLeft",content.Load<Texture2D>("FireWalkingLeft")},
                { "FireCrouchingRight",content.Load<Texture2D>("FireCrouchingRight")},{"FireIdleRight",content.Load<Texture2D>("FireIdleRight")},
                { "FireJumpingRight",content.Load<Texture2D>("FireJumpingRight")},{"FireWalkingRight",content.Load<Texture2D>("FireWalkingRight")},
                { "SuperCrouchingLeft",content.Load<Texture2D>("SuperCrouchingLeft")},{"SuperIdleLeft",content.Load<Texture2D>("SuperIdleLeft")},
                { "SuperJumpingLeft",content.Load<Texture2D>("SuperJumpingLeft")},{"SuperWalkingLeft",content.Load<Texture2D>("SuperWalkingLeft")},
                { "SuperCrouchingRight",content.Load<Texture2D>("SuperCrouchingRight")},{"SuperIdleRight",content.Load<Texture2D>("SuperIdleRight")},
                { "SuperJumpingRight",content.Load<Texture2D>("SuperJumpingRight")},{"SuperWalkingRight",content.Load<Texture2D>("SuperWalkingRight")},
                { "StandardIdleLeft",content.Load<Texture2D>("StandardIdleLeft")},{"StandardJumpingLeft",content.Load<Texture2D>("StandardJumpingLeft")},
                { "StandardWalkingLeft",content.Load<Texture2D>("StandardWalkingLeft")},{"StandardIdleRight",content.Load<Texture2D>("StandardIdleRight")},
                { "StandardJumpingRight",content.Load<Texture2D>("StandardJumpingRight")},{"StandardWalkingRight",content.Load<Texture2D>("StandardWalkingRight")},
            };
        }

        public ISprite CreateMarioSprite(string status, bool isStatic)
        {
            ISprite sprite;

            if (isStatic)
            {
                sprite = new StaticSprite(stringToMarioTexture[status]);
            }
            else
            {
                sprite = new AnimatedSprite(stringToMarioTexture[status], 3, 1);
            }

            return sprite;
        }

        public ISprite CreateGoombaSprite(string status)
        {
            switch (status)
            {
                case "Normal":
                    return new AnimatedSprite(goombaSpritesheet, 2, 1);
                case "Defeated":
                    return new StaticSprite(goombaSpritesheet);
                default:
                    throw new Exception("Unknown sprite");
            }
        }

        public ISprite CreateRedKoopaSprite(string status)
        {
            switch (status)
            {
                case "Normal":
                    return new AnimatedSprite(redKoopaSpritesheet, 2, 2);
                case "JumpOn":
                    return new StaticSprite(redKoopaSteppedSpritesheet);
                case "Defeated":
                    return new StaticSprite(redKoopaDeadSpritesheet);
                default:
                    throw new Exception("Unknown sprite");
            }
        }

        public ISprite CreateGreenKoopaSprite(string status)
        {
            switch (status)
            {
                case "Normal":
                    return new AnimatedSprite(greenKoopaSpritesheet, 2, 2);
                case "JumpOn":
                    return new StaticSprite(greenKoopaSteppedSpritesheet);
                case "Defeated":
                    return new StaticSprite(greenKoopaDeadSpritesheet);
                default:
                    throw new Exception("Unknown sprite");
            }
        }

        public ISprite CreateStarSprite()
        {
            return new AnimatedSprite(starSpritesheet, 4, 1);
        }

        public ISprite CreateCoinSprite()
        {
            return new AnimatedSprite(coinSpritesheet, 4, 1);
        }

        public ISprite CreateSuperMushroomSprite()
        {
            return new StaticSprite(superMushroomSpritesheet);
        }

        public ISprite CreateFlowerSprite()
        {
            return new AnimatedSprite(fireFlowerSpritesheet, 8, 1);
        }

        public ISprite CreateOneUpMushroomSprite()
        {
            return new StaticSprite(oneUpMushroomSpritesheet);
        }

        public ISprite CreateQuestionSprite(string v)
        {
            switch (v)
            {
                case "Used":
                    return new SlicedSprite(blockSpritesheet, 33, 28, 27, 0);
                case "Silent":
                    return new AnimatedSprite(questionSpriteSheet, 1, 3);
                default:
                    throw new Exception("Unknown sprite");
            }
        }

        public ISprite CreateBrickSprite(string v)
        {
            switch (v)
            {
                case "DestroyedLT":
                    return new BrickPieceSprite(brickPieceSpritesheet, BrickPieceSprite.Part.LeftTop, 0, 0);
                case "DestroyedLB":
                    return new BrickPieceSprite(brickPieceSpritesheet, BrickPieceSprite.Part.LeftBottom, 1, 0);
                case "DestroyedRT":
                    return new BrickPieceSprite(brickPieceSpritesheet, BrickPieceSprite.Part.RightTop, 0, 1);
                case "DestroyedRB":
                    return new BrickPieceSprite(brickPieceSpritesheet, BrickPieceSprite.Part.RightBottom, 1, 1);
                case "Used":
                    return new SlicedSprite(blockSpritesheet, 33, 28, 27, 0);
                case "Silent":
                    return new SlicedSprite(blockSpritesheet, 33, 28, 1, 0);
                default:
                    throw new Exception("Unknown sprite");
            }
        }

        public ISprite CreateFloorSprite()
        {
            return new SlicedSprite(blockSpritesheet, 33, 28, 0, 0);
        }

        public ISprite CreateStairSprite()
        {
            return new SlicedSprite(blockSpritesheet, 33, 28, 1, 0);
        }

        public ISprite CreatePipelineSprite()
        {
            return new SlicedSprite(blockSpritesheet, 33, 28, 8, 0);
        }
    }
}
