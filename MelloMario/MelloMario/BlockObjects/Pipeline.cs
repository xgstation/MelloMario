using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.MarioObjects;
using MelloMario.MarioObjects.MovementStates;
using MelloMario.Theming;

namespace MelloMario.BlockObjects
{
    class Pipeline : BaseCollidableObject
    {
        private string type;
        private GameModel model;
        private void UpdateSprite()
        {
            ShowSprite(SpriteFactory.Instance.CreatePipelineSprite(type));
        }

        protected override void OnUpdate(GameTime time)
        {
        }

        protected override void OnCollision(IGameObject target, CollisionMode mode, CornerMode corner, CornerMode cornerPassive)
        {
            if (target is PlayerMario mario && mode is CollisionMode.Top)
            {
                if (mario.MovementState is Crouching && GameDataBase.IsEntrance(this))
                {
                    string str = GameDataBase.GetEntranceIndex(this);
                    model.SwitchWorld(str);
                }
            }
        }

        protected override void OnCollideViewport(IPlayer player, CollisionMode mode)
        {
        }

        protected override void OnCollideCamera(CollisionMode mode)
        {
        }

        protected override void OnDraw(GameTime time, Rectangle viewport, ZIndex zIndex)
        {
        }

        public Pipeline(IGameWorld world, Point location, string type, GameModel model) : this (world, location, type)
        {
            this.model = model;
        }
        public Pipeline(IGameWorld world, Point location, string type) : base(world, location, new Point(32, 32))
        {
            this.type = type;
            UpdateSprite();
        }
    }
}
