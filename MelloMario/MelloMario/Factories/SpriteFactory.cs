using System.Collections.Generic;
using MelloMario.Sprites;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using MelloMario.Theming;
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

        
        private Texture2D GetTexture(string spriteSheetName, string color, Rectangle range)
        {
            if (!textures.ContainsKey(spriteSheetName + color))
            {
                var fullSheet = content.Load<Texture2D>(spriteSheetName);
                var subTexture = new Texture2D(fullSheet.GraphicsDevice, range.Width, range.Height);
                var colors = new Color[range.Width * range.Height];
                fullSheet.GetData(0, range, colors, 0, colors.Length);
                subTexture.SetData(colors);
                textures.Add(spriteSheetName + color, subTexture);
            }
            return textures[spriteSheetName + color];
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

        public ISprite CreateMarioSprite(string status, string protectionStatus, bool isStatic)
        {
            switch (protectionStatus)
            {
                case "Protected":
                    return new FlashingAnimatedSprite(spriteBatch, GetTexture(status), GameConst.ANIMATION_INTERVAL, isStatic ? 1 : 3, 1, ZIndex.item);
                case "Starred":
                    return new FlickingAnimatedSprite(spriteBatch, GetTexture(status), GameConst.ANIMATION_INTERVAL, isStatic ? 1 : 3, 1, ZIndex.item);
                case "Normal":
                case "Dead":
                    return new AnimatedSprite(spriteBatch, GetTexture(status), GameConst.ANIMATION_INTERVAL, isStatic ? 1 : 3, 1, ZIndex.item);
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
                    return new AnimatedSprite(spriteBatch, GetTexture("Goomba"), GameConst.ANIMATION_INTERVAL, 2, 1);
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
                case "MovingShellLeft":
                case "MovingShellRight":
                    return new StaticSprite(spriteBatch, GetTexture("GreenKoopaDead"));
                case "ShellLeft":
                case "ShellRight":
                    return new StaticSprite(spriteBatch, GetTexture("GreenKoopaStepped"));
                case "NormalLeft":
                    return new AnimatedSprite(spriteBatch, GetTexture("GreenKoopaLeft"), GameConst.ANIMATION_INTERVAL, 2, 1);
                case "NormalRight":
                    return new AnimatedSprite(spriteBatch, GetTexture("GreenKoopaRight"), GameConst.ANIMATION_INTERVAL, 2, 1);
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
                case "MovingShellLeft":
                case "MovingShellRight":
                    return new StaticSprite(spriteBatch, GetTexture("RedKoopaDead"));
                case "ShellLeft":
                case "ShellRight":
                    return new StaticSprite(spriteBatch, GetTexture("RedKoopaStepped"));
                case "NormalLeft":
                    return new AnimatedSprite(spriteBatch, GetTexture("RedKoopaLeft"), GameConst.ANIMATION_INTERVAL, 2, 1);
                case "NormalRight":
                    return new AnimatedSprite(spriteBatch, GetTexture("RedKoopaRight"), GameConst.ANIMATION_INTERVAL, 2, 1);
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
                    return new AnimatedSprite(spriteBatch, GetTexture("Piranha", "Green", new Rectangle(0, 0, 64, 48)), GameConst.ANIMATION_INTERVAL, 2, 1);
                case "Cyan":
                    return new AnimatedSprite(spriteBatch, GetTexture("Piranha", "Cyan", new Rectangle(0, 48, 64, 48)), GameConst.ANIMATION_INTERVAL, 2, 1);
                case "Red":
                    return new AnimatedSprite(spriteBatch, GetTexture("Piranha", "Red", new Rectangle(0, 96, 64, 48)), GameConst.ANIMATION_INTERVAL, 2, 1);
                case "Gray":
                    return new AnimatedSprite(spriteBatch, GetTexture("Piranha", "Gray", new Rectangle(0, 144, 64, 48)), GameConst.ANIMATION_INTERVAL, 2, 1);
                default:
                    return null;
            }
        }

        public ISprite CreateFireSprite()
        {
            return new AnimatedSprite(spriteBatch,GetTexture("Fire"), GameConst.ANIMATION_INTERVAL, 2, 2);
        }
        public ISprite CreateStarSprite()
        {
            return new AnimatedSprite(spriteBatch, GetTexture("Star"), GameConst.ANIMATION_INTERVAL, 4, 1);
        }

        public ISprite CreateCoinSprite()
        {
            return new AnimatedSprite(spriteBatch, GetTexture("Coin"), GameConst.ANIMATION_INTERVAL, 4, 1);
        }

        public ISprite CreateSuperMushroomSprite()
        {
            return new StaticSprite(spriteBatch, GetTexture("SuperMushroom"));
        }

        public ISprite CreateFireFlowerSprite()
        {
            return new AnimatedSprite(spriteBatch, GetTexture("FireFlower"), GameConst.ANIMATION_INTERVAL, 8, 1);
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
                    return new AnimatedSprite(spriteBatch, GetTexture("Question"), GameConst.ANIMATION_INTERVAL, 3, 1, ZIndex.level);
                case "Normal":
                    return new AnimatedSprite(spriteBatch, GetTexture("Question"), GameConst.ANIMATION_INTERVAL, 3, 1, ZIndex.level);
                case "Used":
                    return new SlicedSprite(spriteBatch, GetTexture("BlockSheet"), 33, 28, 27, 0, 1, 1, ZIndex.level);
                default:
                    //it should never hit this case, if it does there is an error somewhere
                    //else in the code
                    return null;
            }
        }

        public ISprite CreateFlagSprite(bool status)
        {
            if (status)
            {
                return new SlicedSprite(spriteBatch, GetTexture("BlockSheet"), 33, 28, 16, 8, 1, 1, ZIndex.level);
            }
            else
            {
                return new SlicedSprite(spriteBatch, GetTexture("BlockSheet"), 33, 28, 16, 9, 1, 1, ZIndex.level);
            }
        }

        public ISprite CreateBrickSprite(string status)
        {
            switch (status)
            {
                case "Bumped":
                    return new SlicedSprite(spriteBatch, GetTexture("BlockSheet"), 33, 28, 1, 0, 1, 1, ZIndex.level);
                case "Destroyed":
                    return new BrickPieceSprite(spriteBatch, GetTexture("BrickPieces"));
                case "Normal":
                    return new SlicedSprite(spriteBatch, GetTexture("BlockSheet"), 33, 28, 1, 0, 1, 1, ZIndex.level);
                case "Used":
                    return new SlicedSprite(spriteBatch, GetTexture("BlockSheet"), 33, 28, 27, 0, 1, 1, ZIndex.level);
                default:
                    //it should never hit this case, if it does there is an error somewhere
                    //else in the code
                    return null;
            }
        }

        public ISprite CreateCompressedSprite(Point fullSize, string type)
        {
            return new CompressedSprite(spriteBatch, GetTexture("Blocksheet"), new Point(), fullSize, ZIndex.level, type);
        }

        public ISprite CreateFloorSprite()
        {
            return new SlicedSprite(spriteBatch, GetTexture("BlockSheet"), 33, 28, 0, 0, 1, 1, ZIndex.level);
        }

        public ISprite CreateStairSprite()
        {
            return new SlicedSprite(spriteBatch, GetTexture("BlockSheet"), 33, 28, 0, 1, 1, 1, ZIndex.level);
        }

        public ISprite CreatePipelineSprite(string type)
        {
            switch (type)
            {
                case "LeftIn":
                    return new SlicedSprite(spriteBatch, GetTexture("BlockSheet"), 33, 28, 0, 8, 1, 1, ZIndex.level);
                case "RightIn":
                    return new SlicedSprite(spriteBatch, GetTexture("BlockSheet"), 33, 28, 1, 8, 1, 1, ZIndex.level);
                case "Left":
                    return new SlicedSprite(spriteBatch, GetTexture("BlockSheet"), 33, 28, 0, 9, 1, 1, ZIndex.level);
                case "Right":
                    return new SlicedSprite(spriteBatch, GetTexture("BlockSheet"), 33, 28, 1, 9, 1, 1, ZIndex.level);
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

        public ISprite CreateSceneSprite(string type, ZIndex zIndex)
        {
            switch (type)
            {
                case "ShortCloud":
                    return new SlicedSprite(spriteBatch, GetTexture("BlockSheet"), 33, 28, 0, 20, 3, 2, zIndex);
                case "ShortSmileCloud":
                    return new SlicedSprite(spriteBatch, GetTexture("BlockSheet"), 33, 28, 4, 21, 1, 1, zIndex);
                case "LongCloud":
                    return new SlicedSprite(spriteBatch, GetTexture("BlockSheet"), 33, 28, 8, 22, 4, 1, zIndex);
                case "LongSmileCloud":
                    return new SlicedSprite(spriteBatch, GetTexture("BlockSheet"), 33, 28, 5, 20, 3, 2, zIndex);
                default:
                    return null;
            }
        }
    }
}
