using System.Collections.Generic;
using MelloMario.Sprites;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using MelloMario.Sprites.BlockSprites;
using Microsoft.Xna.Framework;

namespace MelloMario.Factories
{
    class SpriteFactory : ISpriteFactory
    {
        private static ISpriteFactory instance = new SpriteFactory();

        private ContentManager content;

        private IDictionary<string, Texture2D> textures;

        private SpriteFactory()
        {
            textures = new Dictionary<string, Texture2D>();
        }

        private Texture2D GetTexture(string name)
        {
            if (!textures.ContainsKey(name))
            {
                textures.Add(name, content.Load<Texture2D>(name));
            }

            return textures[name];
        }

        public static ISpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

        public void BindContentManager(ContentManager newContentManager)
        {
            content = newContentManager;
        }

        public ISprite CreateTextSprite(string text, float fontSize = 18f)
        {
            return new TextSprite(text, content.Load<SpriteFont>("Font\\text"), new Point(), ZIndex.hud, fontSize);
        }

        public ISprite CreatSplashSprite()
        {
            return new SplashSprite();
        }

        public ISprite CreateMarioSprite(string powerUpStatus, string movementStatus, string protectionStatus, string facing)
        {
            switch (protectionStatus)
            {
                case "Protected":
                    return new FlashingAnimatedSprite(GetTexture(powerUpStatus + movementStatus + facing), movementStatus == "Walking" ? 3 : 1, 1, 0, 0, 2, powerUpStatus == "Standard" ? 2 : movementStatus == "Crouching" ? 3 : 4);
                case "Starred":
                    return new FlickingAnimatedSprite(GetTexture(powerUpStatus + movementStatus + facing), movementStatus == "Walking" ? 3 : 1, 1, 0, 0, 2, powerUpStatus == "Standard" ? 2 : movementStatus == "Crouching" ? 3 : 4);
                case "Normal":
                    return new AnimatedSprite(GetTexture(powerUpStatus + movementStatus + facing), movementStatus == "Walking" ? 3 : 1, 1, 0, 0, 2, powerUpStatus == "Standard" ? 2 : movementStatus == "Crouching" ? 3 : 4);
                case "Dead":
                    return new StaticSprite(GetTexture(protectionStatus), zIndex: ZIndex.foreground);
                case "GameOver":
                    return new StaticSprite(GetTexture(powerUpStatus + movementStatus + facing), zIndex: ZIndex.hud);
                default:
                    //it should never hit this case, if it does there is an error somewhere
                    //else in the code
                    return null;
            }
        }

        public ISprite CreateGoombaSprite(string status)
        {
            switch (status)
            {
                case "Defeated":
                    return new StaticSprite(GetTexture("GoombaDead"));
                case "Normal":
                    return new AnimatedSprite(GetTexture("Goomba"), 2, 1);
                default:
                    //it should never hit this case, if it does there is an error somewhere
                    //else in the code
                    return null;
            }
        }

        public ISprite CreateKoopaSprite(string color, string status)
        {
            switch (status)
            {
                case "DefeatedLeft":
                case "DefeatedRight":
                case "MovingShellLeft":
                case "MovingShellRight":
                    return new StaticSprite(GetTexture(color + "KoopaDead"), 0, 0, 2, 3);
                case "ShellLeft":
                case "ShellRight":
                    return new StaticSprite(GetTexture(color + "KoopaStepped"), 0, 0, 2, 3);
                case "NormalLeft":
                    return new AnimatedSprite(GetTexture(color + "KoopaLeft"), 2, 1, 0, 0, 2, 3);
                case "NormalRight":
                    return new AnimatedSprite(GetTexture(color + "KoopaRight"), 2, 1, 0, 0, 2, 3);
                default:
                    //it should never hit this case, if it does there is an error somewhere
                    //else in the code
                    return null;
            }
        }

        public ISprite CreatePiranhaSprite(string color)
        {
            switch (color)
            {
                case "Green":
                    return new AnimatedSprite(GetTexture("Piranha"), 2, 1, 0, 0, 2, 3);
                case "Cyan":
                    return new AnimatedSprite(GetTexture("Piranha"), 2, 1, 0, 3, 2, 3);
                case "Red":
                    return new AnimatedSprite(GetTexture("Piranha"), 2, 1, 0, 6, 2, 3);
                case "Gray":
                    return new AnimatedSprite(GetTexture("Piranha"), 2, 1, 0, 9, 2, 3);
                default:
                    return null;
            }
        }

        public ISprite CreateFireSprite()
        {
            return new AnimatedSprite(GetTexture("Fire"), 2, 2, 0, 0, 1, 1);
        }

        public ISprite CreateStarSprite()
        {
            return new AnimatedSprite(GetTexture("Star"), 4, 1);
        }

        public ISprite CreateCoinSprite(bool isHud = false)
        {
            if (isHud)
            {
                return new AnimatedSprite(GetTexture("Coin"), 4, 1, 0, 0, 1, 2, 100, ZIndex.hud);
            }
            return new AnimatedSprite(GetTexture("Coin"), 4, 1, 0, 0, 1, 2);
        }

        public ISprite CreateSuperMushroomSprite()
        {
            return new StaticSprite(GetTexture("SuperMushroom"));
        }

        public ISprite CreateFireFlowerSprite()
        {
            return new AnimatedSprite(GetTexture("FireFlower"), 8, 1);
        }

        public ISprite CreateOneUpMushroomSprite()
        {
            return new StaticSprite(GetTexture("OneUpMushroom"));
        }

        public ISprite CreateQuestionSprite(string status)
        {
            switch (status)
            {
                case "Bumped":
                    return new AnimatedSprite(GetTexture("Question"), 3, 1, zIndex: ZIndex.level);
                case "Normal":
                    return new AnimatedSprite(GetTexture("Question"), 3, 1, zIndex: ZIndex.level);
                case "Used":
                    return new StaticSprite(GetTexture("BlockSheet"), 54, 0, zIndex: ZIndex.level);
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
                    return new StaticSprite(GetTexture("BlockSheet"), 2, 0, zIndex: ZIndex.level);
                case "Destroyed":
                    return new BrickPieceSprite(GetTexture("BrickPieces"));
                case "Normal":
                    return new StaticSprite(GetTexture("BlockSheet"), 2, 0, zIndex: ZIndex.level);
                case "Used":
                    return new StaticSprite(GetTexture("BlockSheet"), 54, 0, zIndex: ZIndex.level);
                default:
                    //it should never hit this case, if it does there is an error somewhere
                    //else in the code
                    return null;
            }
        }

        public ISprite CreateFloorSprite()
        {
            return new StaticSprite(GetTexture("BlockSheet"), 0, 0, zIndex: ZIndex.level);
        }

        public ISprite CreateStairSprite()
        {
            return new StaticSprite(GetTexture("BlockSheet"), 0, 2, zIndex: ZIndex.level);
        }

        public ISprite CreatePipelineSprite(string type)
        {
            switch (type)
            {
                case "LeftIn":
                    return new StaticSprite(GetTexture("BlockSheet"), 0, 16, zIndex: ZIndex.foreground);
                case "RightIn":
                    return new StaticSprite(GetTexture("BlockSheet"), 2, 16, zIndex: ZIndex.foreground);
                case "Left":
                    return new StaticSprite(GetTexture("BlockSheet"), 0, 18, zIndex: ZIndex.foreground);
                case "Right":
                    return new StaticSprite(GetTexture("BlockSheet"), 2, 18, zIndex: ZIndex.foreground);
                //TODO: Implement cases below
                case "TopLeftIn":
                case "BottomLeftIn":
                case "TopRightIn":
                case "BottomRightIn":
                case "Top":
                case "Bottom":
                default:
                    //it should never hit this case, if it does there is an error somewhere
                    //else in the code
                    return null;
            }
        }

        public ISprite CreateFlagSprite(bool isTop)
        {
            if (isTop)
            {
                return new StaticSprite(GetTexture("BlockSheet"), 32, 16, zIndex: ZIndex.level);
            }
            else
            {
                return new StaticSprite(GetTexture("BlockSheet"), 32, 18, zIndex: ZIndex.level);
            }
        }

        public ISprite CreateSceneSprite(string type, ZIndex zIndex)
        {
            switch (type)
            {
                case "ShortCloud":
                    return new StaticSprite(GetTexture("BlockSheet"), 0, 40, 6, 4, zIndex);
                case "ShortSmileCloud":
                    return new StaticSprite(GetTexture("BlockSheet"), 8, 42, 2, 2, zIndex);
                case "LongCloud":
                    return new StaticSprite(GetTexture("BlockSheet"), 16, 44, 8, 2, zIndex);
                case "LongSmileCloud":
                    return new StaticSprite(GetTexture("BlockSheet"), 10, 40, 6, 4, zIndex);
                case "CastleTop":
                    return new StaticSprite(GetTexture("BlockSheet"), 22, 0, 2, 2, zIndex);
                case "CastleTopSolid":
                    return new StaticSprite(GetTexture("BlockSheet"), 22, 2, 2, 2, zIndex);
                case "CastleBody":
                    return new StaticSprite(GetTexture("BlockSheet"), 26, 0, 2, 2, zIndex);
                case "CastleDoorLeft":
                    return new StaticSprite(GetTexture("BlockSheet"), 24, 0, 2, 2, zIndex);
                case "CastleDoorRight":
                    return new StaticSprite(GetTexture("BlockSheet"), 28, 0, 2, 2, zIndex);
                case "CastleDoorTop":
                    return new StaticSprite(GetTexture("BlockSheet"), 24, 2, 2, 2, zIndex);
                case "CastleDoor":
                    return new StaticSprite(GetTexture("BlockSheet"), 26, 2, 2, 2, zIndex);
                default:
                    return null;
            }
        }

        public ISprite CreateTitle(ZIndex zIndex)
        {
            return new StaticSprite(GetTexture("Title"), 0, 0, 22, 11, zIndex);
        }
    }
}
