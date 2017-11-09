using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.MarioObjects;
using MelloMario.MarioObjects.MovementStates;
using MelloMario.Theming;

namespace MelloMario.BlockObjects
{
    class Pipeline : BaseCollidableObject
    {
        private static GameModel model;
        private string type;
        private bool isSwitching = false;
        private int elapsed;

        public string Type
        {
            get
            {
                return type;
            }
        }

        private static void SetModel(GameModel newModel)
        {
            model = newModel;
        }
        private void UpdateSprite()
        {
            ShowSprite(SpriteFactory.Instance.CreatePipelineSprite(type));
        }

        protected override void OnUpdate(int time)
        {
            if (isSwitching)
            {
                elapsed += time;
            }
            if (isSwitching && elapsed > 500)
            {
                model.SwitchWorld(GameDatabase.GetEntranceIndex(this));
                elapsed = 0;
                isSwitching = false;
            }
        }

        protected override void OnCollision(IGameObject target, CollisionMode mode, CornerMode corner, CornerMode cornerPassive)
        {
            if (target is PlayerMario mario && mode is CollisionMode.Top)
            {
                if (mario.MovementState is Crouching && GameDatabase.IsEntrance(this))
                {
                    switch (type)
                    {
                        case "LeftIn":
                            if (mario.Boundary.Center.X > Boundary.Center.X)
                            {
                                isSwitching = true;
                            }
                            break;
                        case "RightIn":
                            if (mario.Boundary.Center.X < Boundary.Center.X)
                            {
                                isSwitching = true;
                            }
                            break;
                    }

                    mario.CrouchRelease();
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

        protected override void OnDraw(int time, Rectangle viewport, ZIndex zIndex)
        {
        }

        public Pipeline(IGameWorld world, Point location, string type, GameModel model) : this(world, location, type)
        {
            SetModel(model);
        }

        public Pipeline(IGameWorld world, Point location, string type) : base(world, location, new Point(32, 32))
        {
            this.type = type;
            UpdateSprite();
        }
    }
}
