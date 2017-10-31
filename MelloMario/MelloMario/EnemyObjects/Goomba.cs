using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.MarioObjects;
using MelloMario.EnemyObjects.GoombaStates;
using MelloMario.BlockObjects;

namespace MelloMario.EnemyObjects
{
    class Goomba : BasePhysicalObject
    {

        private IGoombaState state;
        private float timeFromDeath;
        private const int VELOCITY_LR = 1;
        private bool move;
        private Rectangle marioCurrentLoc;
        private Point goombaCurrentLoc;
        private void UpdateSprite()
        {
         
            ShowSprite(SpriteFactory.Instance.CreateGoombaSprite(state.GetType().Name)); 
        }



        protected override void OnUpdate(GameTime time)
        {
            
            state.Update(time);
            if (state is Defeated)
            {
                timeFromDeath += (float)time.ElapsedGameTime.TotalMilliseconds;
                if (timeFromDeath > 1000f)
                {
                    RemoveSelf();
                }
            }
            else
            {
                if (move)
                {
                    ApplyGravity();
                    if (Facing == FacingMode.right)
                    {
                        Move(new Point(VELOCITY_LR, 0));
                        goombaCurrentLoc += new Point(VELOCITY_LR, 0);
                    }
                    else
                    {
                        Move(new Point(-VELOCITY_LR, 0));
                        goombaCurrentLoc -= new Point(VELOCITY_LR, 0);
                    }
                }
                if ((marioCurrentLoc.Location.X - goombaCurrentLoc.X) < marioCurrentLoc.Size.X || (marioCurrentLoc.Location.X - goombaCurrentLoc.X) > -marioCurrentLoc.Size.X)
                {
                    move = true;
                }
                else
                {
                    move = false;
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
            else if (target is Brick || target is Question ||target is Floor || target is Stair || target is Pipeline) {
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
                else if(mode == CollisionMode.Bottom)
                {
                    Bounce(mode, new Vector2());
                
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

        public Goomba(IGameWorld world, Point location, Rectangle marioViewport) : base(world, location, new Point(32, 32), 32)
        {
            marioCurrentLoc = marioViewport;
            goombaCurrentLoc = location;
            if (marioViewport.Location.X < location.X)
            {
                Facing = FacingMode.left;
            }
            else
            {
                Facing = FacingMode.right;
            }
            move = false;
            timeFromDeath = 0;
            state = new Normal(this);
            UpdateSprite();
        }

        public void Defeat()
        {
            State.Defeat();
        }
    }
}
