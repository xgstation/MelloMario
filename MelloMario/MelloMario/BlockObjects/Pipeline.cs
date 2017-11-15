﻿using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.MarioObjects;
using MelloMario.MarioObjects.MovementStates;
using MelloMario.Theming;
using Microsoft.Xna.Framework.Audio;
using MelloMario.Sounds;

namespace MelloMario.BlockObjects
{
    class Pipeline : BaseCollidableObject
    {
        private GameModel model;
        private IPlayer switchingPlayer;
        private string type;
        private int elapsed;
        //private SoundController soundControl;

        public string Type
        {
            get
            {
                return type;
            }
        }

        private void SetModel(GameModel newModel)
        {
            model = newModel;
        }

        private void UpdateSprite()
        {
            ShowSprite(SpriteFactory.Instance.CreatePipelineSprite(type));
        }

        protected override void OnUpdate(int time)
        {
            if (switchingPlayer != null)
            {
                elapsed += time;

                if (elapsed > 500)
                {
                    IGameWorld world = model.LoadLevel(GameDatabase.GetEntranceIndex(this));

                    if (GameDatabase.IsPortal(this))
                    {
                        switchingPlayer.Spawn(world, GameDatabase.GetPortal(this).Boundary.Location);
                    }
                    else
                    {
                        switchingPlayer.Spawn(world, world.GetInitialPoint());
                    }

                    elapsed = 0;
                    switchingPlayer = null;
                }
            }
        }

        protected override void OnCollision(IGameObject target, CollisionMode mode, CornerMode corner, CornerMode cornerPassive)
        {
            if (target is PlayerMario mario && mode is CollisionMode.Top)
            {
                if (mario.MovementState is Crouching && GameDatabase.IsEntrance(this))
                {
                    SoundController.pipe.CreateInstance().Play();
                    switch (type)
                    {
                        case "LeftIn":
                            if (mario.Boundary.Center.X > Boundary.Center.X)
                            {
                                switchingPlayer = mario;
                            }
                            break;
                        case "RightIn":
                            if (mario.Boundary.Center.X < Boundary.Center.X)
                            {
                                switchingPlayer = mario;
                            }
                            break;
                    }

                    // TODO: mario.freeze
                }
            }
        }

        protected override void OnCollideViewport(IPlayer player, CollisionMode mode)
        {
        }

        protected override void OnCollideWorld(CollisionMode mode)
        {
        }

        protected override void OnDraw(int time, Rectangle viewport)
        {
        }

        public Pipeline(IGameWorld world, Point location, Listener listener, string type, GameModel model) : this(world, location, listener, type)
        {
            SetModel(model);
        }

        public Pipeline(IGameWorld world, Point location, Listener listener, string type) : base(world, location, listener, new Point(32, 32))
        {
            this.type = type;

            UpdateSprite();
        }
    }
}
