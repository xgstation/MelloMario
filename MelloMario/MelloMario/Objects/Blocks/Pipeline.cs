﻿namespace MelloMario.Objects.Blocks
{
    #region

    using System;
    using MelloMario.Factories;
    using MelloMario.Objects.Characters;
    using MelloMario.Objects.Characters.MovementStates;
    using MelloMario.Sounds.Effects;
    using MelloMario.Theming;
    using Microsoft.Xna.Framework;

    #endregion

    internal class Pipeline : BaseCollidableObject
    {
        private IModel model;
        private int elapsed;
        private IPlayer switchingPlayer;
        public Pipeline(IWorld world, Point location, IListener<IGameObject> listener, string type) : base(
            world,
            location,
            listener,
            new Point(32, 32))
        {
            Type = type;

            SoundEventArgs = new SoundArgs();
            UpdateSprite();
        }

        public ISoundArgs SoundEventArgs { get; }

        public string Type { get; }

        protected override void OnUpdate(int time)
        {
            if (switchingPlayer != null)
            {
                elapsed += time;

                if (elapsed > 500)
                {
                    IWorld world = model.LoadLevel(Database.GetEntranceIndex(this));

                    if (Database.IsPortal(this))
                    {
                        switchingPlayer.Spawn(world, Database.GetPortal(this).Boundary.Location);
                    }

                    elapsed = 0;
                    switchingPlayer = null;
                }
            }
        }

        protected override void OnCollision(
            IGameObject target,
            CollisionMode mode,
            CollisionMode modePassive,
            CornerMode corner,
            CornerMode cornerPassive)
        {
            if (target is Mario mario && mode is CollisionMode.Top)
            {
                if (mario.MovementState is Crouching && Database.IsEntrance(this))
                {
                    //TODO:Add warping state to model to allow sound controller play warping sound
                    //SoundManager.Pipe.Play();
                    switch (Type)
                    {
                        case "LeftIn":
                            if (mario.Boundary.Center.X > Boundary.Center.X)
                            {
                                // TODO
                                // switchingPlayer = mario;
                            }
                            break;
                        case "RightIn":
                            if (mario.Boundary.Center.X < Boundary.Center.X)
                            {
                                // TODO
                                // switchingPlayer = mario;
                            }
                            break;
                    }

                    // TODO: mario.freeze
                }
            }
        }

        protected override void OnCollideWorld(CollisionMode mode, CollisionMode modePassive)
        {
        }

        // TODO: use events instead of operating game model directly
        private void SetModel(IModel newModel)
        {
            model = newModel;
        }

        private void UpdateSprite()
        {
            ShowSprite(SpriteFactory.Instance.CreatePipelineSprite(Type));
        }
    }
}
