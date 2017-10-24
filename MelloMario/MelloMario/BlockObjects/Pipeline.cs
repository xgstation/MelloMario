using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.BlockObjects.PipelineStates;
using MelloMario.MarioObjects;
using System;

namespace MelloMario.BlockObjects
{
    class Pipeline : BaseGameObject
    {
        private IBlockState state;
        private bool isPortal;
        private string portalIndex;

        private void UpdateSprite()
        {
            ShowSprite(SpriteFactory.Instance.CreatePipelineSprite(portalIndex));
        }

        protected override void OnSimulation(GameTime time)
        {
        }

        protected override void OnCollision(IGameObject target, CollisionMode mode, CornerMode corner, CornerMode cornerPassive)
        {
        }

        protected override void OnOut(CollisionMode mode)
        {
        }

        protected override void OnDraw(GameTime time, ZIndex zIndex)
        {
        }

        public IBlockState State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
                UpdateSprite();
            }
        }

        public Pipeline(IGameWorld world, Point location, string type) : base(world, location, new Point(32, 32))
        {
            state = new Normal(this);
            this.portalIndex = type;
            UpdateSprite();
        }
        public Pipeline(IGameWorld world, Point location, Tuple<bool,string> property) : base(world, location, new Point(32, 32))
        {
            isPortal = property.Item1;
            portalIndex = property.Item2;
        }

        public void Show()
        {
            State.Show();
        }
        public void Hide()
        {
            State.Hide();
        }
        public void Bump(Mario mario)
        {
            State.Bump(mario);
        }
    }
}
