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
        private SpriteBatch spriteBatch;

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

        public void BindSpriteBatch(SpriteBatch newSpriteBatch)
        {
            spriteBatch = newSpriteBatch;
        }

        public ISprite CreateTextSprite(string text)
        {
            return new TextSprite(spriteBatch, text, content.Load<SpriteFont>("Font\\text"), new Point(), ZIndex.hud);
        }

        public ISprite CreatSplashSprite()
        {
            return new SplashSprite(spriteBatch, new Point(), ZIndex.hud);
        }

        public ISprite CreateMarioSprite(string powerUpStatus, string movementStatus, string protectionStatus, string facing)
        {
            switch (protectionStatus)
            {
                case "Protected":
                    return new FlashingAnimatedSprite(spriteBatch, GetTexture(powerUpStatus + movementStatus + facing), movementStatus == "Walking" ? 3 : 1, 1, 0, 0, 2, powerUpStatus == "Standard" ? 2 : movementStatus == "Crouching" ? 3 : 4);
                case "Starred":
                    return new FlickingAnimatedSprite(spriteBatch, GetTexture(powerUpStatus + movementStatus + facing), movementStatus == "Walking" ? 3 : 1, 1, 0, 0, 2, powerUpStatus == "Standard" ? 2 : movementStatus == "Crouching" ? 3 : 4);
                case "Normal":
                    return new AnimatedSprite(spriteBatch, GetTexture(powerUpStatus + movementStatus + facing), movementStatus == "Walking" ? 3 : 1, 1, 0, 0, 2, powerUpStatus == "Standard" ? 2 : movementStatus == "Crouching" ? 3 : 4);
                case "Dead":
                    return new StaticSprite(spriteBatch, GetTexture(protectionStatus));
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
                    return new StaticSprite(spriteBatch, GetTexture("GoombaDead"));
                case "Normal":
                    return new AnimatedSprite(spriteBatch, GetTexture("Goomba"), 2, 1);
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
                    return new StaticSprite(spriteBatch, GetTexture(color + "KoopaDead"), 0, 0, 2, 3);
                case "ShellLeft":
                case "ShellRight":
                    return new StaticSprite(spriteBatch, GetTexture(color + "KoopaStepped"), 0, 0, 2, 3);
                case "NormalLeft":
                    return new AnimatedSprite(spriteBatch, GetTexture(color + "KoopaLeft"), 2, 1, 0, 0, 2, 3);
                case "NormalRight":
                    return new AnimatedSprite(spriteBatch, GetTexture(color + "KoopaRight"), 2, 1, 0, 0, 2, 3);
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
                    return new AnimatedSprite(spriteBatch, GetTexture("Piranha"), 2, 1, 0, 0, 2, 3);
                case "Cyan":
                    return new AnimatedSprite(spriteBatch, GetTexture("Piranha"), 2, 1, 0, 3, 2, 3);
                case "Red":
                    return new AnimatedSprite(spriteBatch, GetTexture("Piranha"), 2, 1, 0, 6, 2, 3);
                case "Gray":
                    return new AnimatedSprite(spriteBatch, GetTexture("Piranha"), 2, 1, 0, 9, 2, 3);
                default:
                    return null;
            }
        }

        public ISprite CreateFireSprite()
        {
            return new AnimatedSprite(spriteBatch, GetTexture("Fire"), 2, 2, 0, 0, 1, 1);
        }

        public ISprite CreateStarSprite()
        {
            return new AnimatedSprite(spriteBatch, GetTexture("Star"), 4, 1);
        }

        public ISprite CreateCoinSprite(bool isHud = false)
        {
            if (isHud)
            {
                return new AnimatedSprite(spriteBatch, GetTexture("Coin"), 4, 1, 0, 0, 1, 2, 100, ZIndex.hud);
            }
            return new AnimatedSprite(spriteBatch, GetTexture("Coin"), 4, 1, 0, 0, 1, 2);
        }

        public ISprite CreateSuperMushroomSprite()
        {
            return new StaticSprite(spriteBatch, GetTexture("SuperMushroom"));
        }

        public ISprite CreateFireFlowerSprite()
        {
            return new AnimatedSprite(spriteBatch, GetTexture("FireFlower"), 8, 1);
        }

        public ISprite CreateOneUpMushroomSprite()
        {
            return new StaticSprite(spriteBatch, GetTexture("OneUpMushroom"));
        }

        public ISprite CreateQuestionSprite(string status)
        {
            switch (status)
            {
                case "Bumped":
                    return new AnimatedSprite(spriteBatch, GetTexture("Question"), 3, 1, zIndex: ZIndex.level);
                case "Normal":
                    return new AnimatedSprite(spriteBatch, GetTexture("Question"), 3, 1, zIndex: ZIndex.level);
                case "Used":
                    return new StaticSprite(spriteBatch, GetTexture("BlockSheet"), 54, 0, zIndex: ZIndex.level);
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
                    return new StaticSprite(spriteBatch, GetTexture("BlockSheet"), 2, 0, zIndex: ZIndex.level);
                case "Destroyed":
                    return new BrickPieceSprite(spriteBatch, GetTexture("BrickPieces"));
                case "Normal":
                    return new StaticSprite(spriteBatch, GetTexture("BlockSheet"), 2, 0, zIndex: ZIndex.level);
                case "Used":
                    return new StaticSprite(spriteBatch, GetTexture("BlockSheet"), 54, 0, zIndex: ZIndex.level);
                default:
                    //it should never hit this case, if it does there is an error somewhere
                    //else in the code
                    return null;
            }
        }

        public ISprite CreateFloorSprite()
        {
            return new StaticSprite(spriteBatch, GetTexture("BlockSheet"), 0, 0, zIndex: ZIndex.level);
        }

        public ISprite CreateStairSprite()
        {
            return new StaticSprite(spriteBatch, GetTexture("BlockSheet"), 0, 2, zIndex: ZIndex.level);
        }

        public ISprite CreatePipelineSprite(string type)
        {
            switch (type)
            {
                case "LeftIn":
                    return new StaticSprite(spriteBatch, GetTexture("BlockSheet"), 0, 16, zIndex: ZIndex.foreground);
                case "RightIn":
                    return new StaticSprite(spriteBatch, GetTexture("BlockSheet"), 2, 16, zIndex: ZIndex.foreground);
                case "Left":
                    return new StaticSprite(spriteBatch, GetTexture("BlockSheet"), 0, 18, zIndex: ZIndex.foreground);
                case "Right":
                    return new StaticSprite(spriteBatch, GetTexture("BlockSheet"), 2, 18, zIndex: ZIndex.foreground);
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
                return new StaticSprite(spriteBatch, GetTexture("BlockSheet"), 32, 16, zIndex: ZIndex.level);
            }
            else
            {
                return new StaticSprite(spriteBatch, GetTexture("BlockSheet"), 32, 18, zIndex: ZIndex.level);
            }
        }

        public ISprite CreateSceneSprite(string type, ZIndex zIndex)
        {
            switch (type)
            {
                case "ShortCloud":
                    return new StaticSprite(spriteBatch, GetTexture("BlockSheet"), 0, 40, 6, 4, zIndex);
                case "ShortSmileCloud":
                    return new StaticSprite(spriteBatch, GetTexture("BlockSheet"), 8, 42, 2, 2, zIndex);
                case "LongCloud":
                    return new StaticSprite(spriteBatch, GetTexture("BlockSheet"), 16, 44, 8, 2, zIndex);
                case "LongSmileCloud":
                    return new StaticSprite(spriteBatch, GetTexture("BlockSheet"), 10, 40, 6, 4, zIndex);
                default:
                    return null;
            }
        }
    }
}
