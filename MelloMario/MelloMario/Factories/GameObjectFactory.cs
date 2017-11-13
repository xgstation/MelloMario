using System;
using Microsoft.Xna.Framework;
using MelloMario.BlockObjects;
using MelloMario.EnemyObjects;
using MelloMario.ItemObjects;
using MelloMario.MarioObjects;
using MelloMario.MiscObjects;
using MelloMario.Theming;

namespace MelloMario.Factories
{
    class GameObjectFactory : IGameObjectFactory
    {
        // TODO: remove this later and use the collision between the camera and objects to "activate" objects' movement
        private Point marioLoc;

        private static IGameObjectFactory instance = new GameObjectFactory();

        private GameObjectFactory()
        {
            marioLoc = new Point(0, 0);
        }

        public static IGameObjectFactory Instance
        {
            get
            {
                return instance;
            }
        }

        public Tuple<ICharacter, IGameObject> CreateGameCharacter(string type, IGameSession session, IGameWorld world, Point location, Listener listener)
        {
            switch (type)
            {
                case "Mario":
                    marioLoc = location;
                    PlayerMario mario = new PlayerMario(session, world, marioLoc, listener);
                    return new Tuple<ICharacter, IGameObject>(mario, mario);
                default:
                    return null;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        public IGameObject CreateGameObject(string type, IGameWorld world, Point location, Listener listener)
        {
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
                case "EndFlag":
                    return new Flag(world, location, listener, 0, 1);
                case "EndFlagTop":
                    return new Flag(world, location, listener, 1, 1);
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
                    return new Koopa(world, location, marioLoc, listener, Koopa.ShellColor.green);
                case "RedKoopa":
                    return new Koopa(world, location, marioLoc, listener, Koopa.ShellColor.red);

                //entities
                case "Coin":
                    return new Coin(world, location, listener);
                case "CoinUnveil":
                    return new Coin(world, location, listener, true);
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
                    return new Background(world, location, type, ZIndex.background0);
                case "ShortSmileCloud":
                    return new Background(world, location, type, ZIndex.background1);
                case "LongCloud":
                    return new Background(world, location, type, ZIndex.background2);
                case "LongSmileCloud":
                    return new Background(world, location, type, ZIndex.background3);

                default:
                    return null;
            }
        }
    }
}
