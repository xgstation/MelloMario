using System;
using MelloMario.Commands;
using Microsoft.Xna.Framework;
using MelloMario.BlockObjects;
using MelloMario.EnemyObjects;
using MelloMario.ItemObjects;
using MelloMario.MarioObjects;
using System.Collections.Generic;
using MelloMario.MiscObjects;

namespace MelloMario.Factories
{
    class GameObjectFactory : IGameObjectFactory
    {
        private const int SCALE = 32;
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
        public Tuple<IGameControl, IGameObject> CreateGameCharacter(string type, IGameWorld world, Point location)
        {
            switch (type)
            {
                case "Mario":
                    marioLoc = location;
                    PlayerMario mario = new PlayerMario(world, marioLoc);
                    return new Tuple<IGameControl, IGameObject>(mario, mario);
                default:
                    return null;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        public IGameObject CreateGameObject(string type, IGameWorld world, Point location)
        {
            switch (type)
            {
                //blocks
                case "Floor":
                    return new Floor(world, location);
                case "Brick":
                    return new Brick(world, location);
                case "HiddenBrick":
                    return new Brick(world, location, true);
                case "Stair":
                    return new Stair(world, location);
                case "Question":
                    return new Question(world, location, false);
                case "HiddenQuestion":
                    return new Question(world, location, true);
                case "PipelineLeftIn":
                    return new Pipeline(world, location, "LeftIn");
                case "PipelineRightIn":
                    return new Pipeline(world, location, "RightIn");
                case "PipelineLeft":
                    return new Pipeline(world, location, "Left");
                case "PipelineRight":
                    return new Pipeline(world, location, "Right");

                //enemy
                case "Goomba":
                    return new Goomba(world, location, marioLoc);
                case "GreenKoopa":
                    return new Koopa(world, location, marioLoc, Koopa.ShellColor.green);
                case "RedKoopa":
                    return new Koopa(world, location, marioLoc, Koopa.ShellColor.red);

                //entities
                case "Coin":
                    return new Coin(world, location);
                case "CoinUnveil":
                    return new Coin(world, location, true);
                case "OneUpMushroom":
                    return new OneUpMushroom(world, location, marioLoc);
                case "OneUpMushroomUnveil":
                    return new OneUpMushroom(world, location, marioLoc, true);
                case "FireFlower":
                    return new FireFlower(world, location);
                case "FireFlowerUnveil":
                    return new FireFlower(world, location, true);
                case "SuperMushroom":
                    return new SuperMushroom(world, location, marioLoc);
                case "SuperMushroomUnveil":
                    return new SuperMushroom(world, location, marioLoc, true);
                case "Star":
                    return new Star(world, location, marioLoc);
                case "StarUnveil":
                    return new Star(world, location, marioLoc, true);

                //others
                case "ShortCloud":
                    return new Background(world, location, type, ZIndex.background);
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
