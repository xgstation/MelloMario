﻿using System;
using MelloMario.Commands;
using Microsoft.Xna.Framework;
using MelloMario.BlockObjects;
using MelloMario.EnemyObjects;
using MelloMario.ItemObjects;
using MelloMario.MarioObjects;
using System.Collections.Generic;

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
                    PlayerMario mario = new PlayerMario(world, location);

                    return new Tuple<IGameCharacter, IGameObject>(mario, mario);
                default:
                    return null;
            }
        }

        public IGameObject CreateGameObject(string type, IGameWorld world, Point location)
        {
            IEnumerable<IGameObject> items;

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
                    items = new List<IGameObject>()
                    {
                        CreateGameObject("FireFlowerUnveil", world, location),
                    };
                    return new Question(world, location, items, false);
                case "HiddenQuestion":
                    items = new List<IGameObject>()
                    {
                        CreateGameObject("CoinUnveil", world, location),
                        CreateGameObject("CoinUnveil", world, location),
                        CreateGameObject("CoinUnveil", world, location),
                        CreateGameObject("CoinUnveil", world, location),
                    };
                    return new Question(world, location, items, true);
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
                case "CoinUnveil":
                    return new Coin(world, location, true);
                case "OneUpMushroom":
                    return new OneUpMushroom(world, location);
                case "OneUpMushroomUnveil":
                    return new OneUpMushroom(world, location, true);
                case "FireFlower":
                    return new FireFlower(world, location);
                case "FireFlowerUnveil":
                    return new FireFlower(world, location, true);
                case "SuperMushroom":
                    return new SuperMushroom(world, location);
                case "SuperMushroomUnveil":
                    return new SuperMushroom(world, location, true);
                case "Star":
                    return new Star(world, location);
                case "StarUnveil":
                    return new Star(world, location, true);

                default:
                    return null;
            }
        }
    }
}
