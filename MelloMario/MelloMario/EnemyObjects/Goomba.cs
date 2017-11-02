using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.MarioObjects;
using MelloMario.EnemyObjects.GoombaStates;
using MelloMario.BlockObjects;
using System.Diagnostics;

namespace MelloMario.EnemyObjects
{
    class Goomba : BasePhysicalObject
    {

        private IGoombaState state;
        private float timeFromDeath;
        private const int VELOCITY_LR = 1;
        private bool move;

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

                    }
                    else
                    {
                        Move(new Point(-VELOCITY_LR, 0));
                    }
                }
                if (!move)
                {
                    // TODO: use collision detection system to do this job
                    //       similar as GameObject.OnOut
                    move = true; // Boundary.Intersects(World.Model.Control.Viewport);
                }
                else
                {
                    move = true;
                }
            }

        }

        protected override void OnCollision(IGameObject target, CollisionMode mode, CornerMode corner, CornerMode cornerPassive)
        {
            switch (target.GetType().Name)
            {
                case "PlayerMario":
                    //TODO: Fire to be added
                    Mario mario = (Mario)target;
                    if (mode == CollisionMode.Top || mario.ProtectionState is MarioObjects.ProtectionStates.Starred)
                    {
                        Defeat();
                    }
                    break;
                case "Brick":
                    if (((Brick)target).State is BlockObjects.BrickStates.Hidden)
                        break;
                    goto case "Stair";
                case "Question":
                    if (((Question)target).State is BlockObjects.QuestionStates.Hidden)
                        break;
                    goto case "Stair";
                case "Floor":
                case "Pipeline":
                case "Stair":
                    if (mode == CollisionMode.Left)
                    {
                        Bounce(mode, new Vector2(), 1);
                        ChangeFacing(FacingMode.right);
                    }
                    else if (mode == CollisionMode.Right)
                    {
                        Bounce(mode, new Vector2(), 1);
                        ChangeFacing(FacingMode.left);
                    }
                    else if (mode == CollisionMode.Bottom)
                    {
                        Bounce(mode, new Vector2());

                    }
                    break;
            }
            if (target is Koopa koopa)
            {
                if (koopa.State is KoopaStates.MovingShell)
                {
                    Defeat();
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

        private void ChangeFacing(FacingMode facing)
        {
            Facing = facing;

            // Notice: The effect should be the same as changing state
            UpdateSprite();
        }

        public Goomba(IGameWorld world, Point location, Point marioLoc) : base(world, location, new Point(32, 32), 32)
        {
            if (marioLoc.X < location.X)
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
