using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.MarioObjects;
using MelloMario.EnemyObjects.KoopaStates;
using MelloMario.BlockObjects;

namespace MelloMario.EnemyObjects
{
    class Koopa : BasePhysicalObject
    {
        private ShellColor color;
        private IKoopaState state;
        private const int VELOCITY_LR = 1;

        private void UpdateSprite()
        {
            string facingString;
            if (Facing == FacingMode.left)
            {
                facingString = "Left";
            }
            else
            {
                facingString = "Right";
            }
            switch (color)
            {
                case ShellColor.green:
                    ShowSprite(SpriteFactory.Instance.CreateGreenKoopaSprite(state.GetType().Name + facingString));
                    break;
                case ShellColor.red:
                    ShowSprite(SpriteFactory.Instance.CreateRedKoopaSprite(state.GetType().Name + facingString));
                    break;
            }
        }

        private void ChangeFacing(FacingMode facing)
        {
            Facing = facing;

            // Notice: The effect should be the same as changing state
            UpdateSprite();
        }

        protected override void OnUpdate(GameTime time)
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

        protected override void OnCollision(IGameObject target, CollisionMode mode, CornerMode corner, CornerMode cornerPassive)
        {
            if (target is Mario mario)
            {
                //TODO: fire as target to be added
                if (mario.ProtectionState is MarioObjects.ProtectionStates.Starred)
                {
                    Defeat();
                }
                else
                {
                    if (state is Normal)
                    {
                        if (mode == CollisionMode.Top)
                            JumpOn();
                    }
                    else if (state is Shell)
                    {
                        if (mode == CollisionMode.Left || mode == CollisionMode.Top)
                        {
                            Pushed();
                        }
                        else
                        {
                            Pushed();
                        }

                    }
                }
            }
            else if(target is Brick || target is Question || target is Floor || target is Stair || target is Pipeline)
            {
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
            }
        }

        protected override void OnOut(CollisionMode mode)
        {
        }

        protected override void OnDraw(GameTime time, Rectangle viewport, ZIndex zIndex)
        {
        }

        public enum ShellColor { green, red };
        public IKoopaState State
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

        public Koopa(IGameWorld world, Point location, Point marioLocation,ShellColor color) : base(world, location, new Point(32, 32), 32)
        {
            if (marioLocation.X < location.X)
            {
                Facing = FacingMode.left;
            }
            else
            {
                Facing = FacingMode.right;
            }
            this.color = color;

            state = new Normal(this);
            UpdateSprite();
        }
        
        public void JumpOn()
        {
            State.JumpOn();
        }
        public void Pushed()
        {
            State.Pushed();
        }
        public void Defeat()
        {
            State.Defeat();
        }
    }
}
