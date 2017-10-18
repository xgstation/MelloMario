using System;
using MelloMario.Commands;
using Microsoft.Xna.Framework;
using MelloMario.BlockObjects;
using MelloMario.EnemyObjects;
using MelloMario.ItemObjects;
using MelloMario.MarioObjects;

namespace MelloMario.Factories
{
    class GameObjectFactory : IGameObjectFactory
    {
        private const int SCALE = 32;

        private static IGameObjectFactory instance = new GameObjectFactory();

        private GameObjectFactory()
        {
        }

        public static IGameObjectFactory Instance
        {
            get
            {
                return instance;
            }
        }
        public IGameWorld CreateGameWorld(Point size)
        {
            return new GameWorld(SCALE, size);
        }

        public Tuple<IGameCharacter, IGameObject> CreateGameCharacter(string type, IGameWorld world, Point location)
        {
            switch (type)
            {
                case "Mario":
                    Mario mario = new Mario(world, location);

                    return new Tuple<IGameCharacter, IGameObject>(mario, mario);
                default:
                    return null;
            }
        }

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
                    return new Question(world, location);
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
                    return new Goomba(world, location);
                case "GreenKoopa":
                    return new Koopa(world, location, Koopa.ShellColor.green);
                case "RedKoopa":
                    return new Koopa(world, location, Koopa.ShellColor.red);

                //entities
                case "Coin":
                    return new Coin(world, location);
                case "OneUpMushroom":
                    return new OneUpMushroom(world, location);
                case "FireFlower":
                    return new FireFlower(world, location);
                case "SuperMushroom":
                    return new SuperMushroom(world, location);
                case "Star":
                    return new Star(world, location);

                default:
                    return null;
            }
        }
    }
}
