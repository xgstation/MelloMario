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

        public ISprite CreateMarioSprite(string status, string protectionStatus, bool isStatic)
        {
            switch (protectionStatus)
            {
                case "Protected":
                    return new FlashingAnimatedSprite(spriteBatch, GetTexture(status), isStatic ? 1 : 3, 1, ZIndex.item, 250);
                case "Starred":
                    return new FlickingAnimatedSprite(spriteBatch, GetTexture(status), isStatic ? 1 : 3, 1, ZIndex.item, 250);
                case "Normal":
                case "Dead":
                    return new AnimatedSprite(spriteBatch, GetTexture(status), isStatic ? 1 : 3, 1, ZIndex.item, 250);
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
                    return new AnimatedSprite(spriteBatch, GetTexture("GreenKoopaLeft"), 2, 1);
                case "NormalRight":
                    return new AnimatedSprite(spriteBatch, GetTexture("GreenKoopaRight"), 2, 1);
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
                    return new AnimatedSprite(spriteBatch, GetTexture("RedKoopaLeft"), 2, 1);
                case "NormalRight":
                    return new AnimatedSprite(spriteBatch, GetTexture("RedKoopaRight"), 2, 1);
                default:
                    //it should never hit this case, if it does there is an error somewhere
                    //else in the code
                    return null;
            }
        }

        public ISprite CreateStarSprite()
        {
            return new AnimatedSprite(spriteBatch, GetTexture("Star"), 4, 1);
        }

        public ISprite CreateCoinSprite()
        {
            return new AnimatedSprite(spriteBatch, GetTexture("Coin"), 4, 1);
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
                    return new AnimatedSprite(spriteBatch, GetTexture("Question"), 3, 1, ZIndex.level);
                case "Normal":
                    return new AnimatedSprite(spriteBatch, GetTexture("Question"), 3, 1, ZIndex.level);
                case "Used":
                    return new SlicedSprite(spriteBatch, GetTexture("BlockSheet"), 33, 28, 27, 0, 1, 1, ZIndex.level);
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
