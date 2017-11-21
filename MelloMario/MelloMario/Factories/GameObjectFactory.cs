namespace MelloMario.Factories
{
    #region

    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using MelloMario.Objects.Blocks;
    using MelloMario.Objects.Characters;
    using MelloMario.Objects.Enemies;
    using MelloMario.Objects.Items;
    using MelloMario.Objects.Miscs;
    using Microsoft.Xna.Framework;

    #endregion

    internal class GameObjectFactory : IGameObjectFactory
    {
        private GameObjectFactory()
        {
        }

        public static IGameObjectFactory Instance { get; } = new GameObjectFactory();

        public ICharacter CreateGameCharacter(
            string type,
            IGameWorld world,
            IPlayer player,
            Point location,
            IListener<IGameObject> listener)
        {
            switch (type)
            {
                case "Mario":
                    MarioCharacter mario = new MarioCharacter(world, player, location, listener);
                    return mario;
                default:
                    return null;
            }
        }

        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        public IGameObject CreateGameObject(
            string type,
            IGameWorld world,
            Point location,
            IListener<IGameObject> listener,
            IListener<ISoundable> soundListener)
        {
            Point marioLoc = new Point(); // TODO: it is wrong

            switch (type)
            {
                //blocks
                case "Floor":
                    return new Floor(world, location, listener);
                case "Brick":
                    return new Brick(world, location, listener);
                case "HiddenBrick":
                    return new Brick(world, location, listener, true);
                case "Stair":
                    return new Stair(world, location, listener);
                case "Question":
                    return new Question(world, location, listener, false);
                case "HiddenQuestion":
                    return new Question(world, location, listener, true);
                case "PipelineLeftIn":
                    return new Pipeline(world, location, listener, "LeftIn");
                case "PipelineRightIn":
                    return new Pipeline(world, location, listener, "RightIn");
                case "PipelineLeft":
                    return new Pipeline(world, location, listener, "Left");
                case "PipelineRight":
                    return new Pipeline(world, location, listener, "Right");

                //enemy
                case "Goomba":
                    return new Goomba(world, location, marioLoc, listener);
                case "GreenKoopa":
                    return new Koopa(world, location, marioLoc, listener, "Green");
                case "RedKoopa":
                    return new Koopa(world, location, marioLoc, listener, "Red");

                //entities
                case "Coin":
                    return new Coin(world, location, listener, soundListener);
                case "CoinUnveil":
                    return new Coin(world, location, listener, soundListener, true);
                case "OneUpMushroom":
                    return new OneUpMushroom(world, location, marioLoc, listener);
                case "OneUpMushroomUnveil":
                    return new OneUpMushroom(world, location, marioLoc, listener, true);
                case "FireFlower":
                    return new FireFlower(world, location, listener);
                case "FireFlowerUnveil":
                    return new FireFlower(world, location, listener, true);
                case "SuperMushroom":
                    return new SuperMushroom(world, location, marioLoc, listener);
                case "SuperMushroomUnveil":
                    return new SuperMushroom(world, location, marioLoc, listener, true);
                case "Star":
                    return new Star(world, location, marioLoc, listener);
                case "StarUnveil":
                    return new Star(world, location, marioLoc, listener, true);

                //others
                case "ShortCloud":
                    return new Background(world, location, type, ZIndex.Background0);
                case "ShortSmileCloud":
                    return new Background(world, location, type, ZIndex.Background1);
                case "LongCloud":
                    return new Background(world, location, type, ZIndex.Background2);
                case "LongSmileCloud":
                    return new Background(world, location, type, ZIndex.Background3);

                default:
                    return null;
            }
        }

        public IEnumerable<IGameObject> CreateGameObjectGroup(
            string type,
            IGameWorld world,
            Point location,
            int count,
            IListener<IGameObject> listener)
        {
            switch (type)
            {
                case "FlagPole":
                    for (int i = 0; i < count; ++i)
                    {
                        yield return new Flag(
                            world,
                            new Point(location.X, location.Y + 32 * i),
                            listener,
                            count - i,
                            count);
                    }
                    yield break;

                default:
                    yield return null; // should never reach
                    yield break;
            }
        }
    }
}
