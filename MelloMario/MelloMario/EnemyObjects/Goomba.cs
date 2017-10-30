using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.MarioObjects;
using MelloMario.EnemyObjects.GoombaStates;
using MelloMario.BlockObjects;

namespace MelloMario.EnemyObjects
{
    class Goomba : BasePhysicalObject
    {
        bool Fall;
        private IGoombaState state;
        private float timeFromDeath;
        private const int VELOCITY_LR = 1;
        private void UpdateSprite()
        {
         
            ShowSprite(SpriteFactory.Instance.CreateGoombaSprite(state.GetType().Name)); 
        }



        protected override void OnUpdate(GameTime time)
        {
            //ApplyGravity();
            state.Update(time);
            if(state is Defeated)
            {
                timeFromDeath += (float) time.ElapsedGameTime.TotalMilliseconds;
                if (timeFromDeath > 1000f)
                {
                    RemoveSelf();
                }
            }
            else
            {
                if (!Fall)
                {
                    if (Facing == FacingMode.right)
                    {
                        Move(new Point(VELOCITY_LR, 0));
                    }
                    else
                    {
                        Move(new Point(-VELOCITY_LR, 0));
                    }
                }

                   

            }
        }

        protected override void OnCollision(IGameObject target, CollisionMode mode, CornerMode corner, CornerMode cornerPassive)
        {
            if (target is Mario mario)
            {
                //TODO: Fire to be added
                if (mode == CollisionMode.Top || mario.ProtectionState is MarioObjects.ProtectionStates.Starred)
                {
                    Defeat();
                }
            }
            else if (target is Koopa koopa)
            {
                if (koopa.State is KoopaStates.MovingShell)
               {
                    Defeat();
                }
            }
            else if (target is Brick brick || target is Question question||target is Floor floor || target is Stair stair || target is Pipeline pipeline) {
                if (mode == CollisionMode.Left)
                {
                    Bounce(mode, new Vector2(), 1);
                    Facing = FacingMode.right;
                }
                else if (mode == CollisionMode.Right)
                {
                    Bounce(mode, new Vector2(), 1);
                    Facing = FacingMode.left;
                }
            }

        }

        protected override void OnOut(CollisionMode mode)
        {
        }

        protected override void OnDraw(GameTime time, Rectangle viewport, ZIndex zIndex)
        {


        }

        public IGoombaState State
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

        public Goomba(IGameWorld world, Point location, Point marioLocation) : base(world, location, new Point(32, 32), 32)
        {
            if (marioLocation.X < location.X)
            {
                Facing = FacingMode.left;
            }
            else
            {
                Facing = FacingMode.right;
            }
            timeFromDeath = 0;
            state = new Normal(this);
            UpdateSprite();
             Fall = false;
        }

        public void Show()
        {
            State.Show();
        }
        public void Defeat()
        {
            State.Defeat();
        }
    }
}
