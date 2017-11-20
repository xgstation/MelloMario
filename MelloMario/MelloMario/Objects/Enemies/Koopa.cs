using System.Diagnostics.CodeAnalysis;
using MelloMario.Objects.Blocks;
using MelloMario.Objects.Blocks.BrickStates;
using MelloMario.Objects.Enemies.KoopaStates;
using MelloMario.Factories;
using MelloMario.Objects.Characters;
using MelloMario.Objects.Characters.ProtectionStates;
using MelloMario.Theming;
using MelloMario.Objects.UserInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Normal = MelloMario.Objects.Enemies.KoopaStates.Normal;

namespace MelloMario.Objects.Enemies
{
    internal class Koopa : BasePhysicalObject
    {
        private readonly string color;
        private IKoopaState state;

        //This suppression exists because this constructor is inderectly used by the json parser.
        //removing this constructor will cause a runtime error when trying to read in the level.
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public Koopa(IGameWorld world, Point location, IListener listener, string color) : this(world, location,
            Database.GetCharacterLocation(), listener, color) { }

        public Koopa(IGameWorld world, Point location, Point marioLoc, IListener listener, string color) : base(world,
            location, listener, new Point(32, 32), 32)
        {
            if (marioLoc.X < location.X)
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

        public IKoopaState State
        {
            get { return state; }
            set
            {
                state = value;
                UpdateSprite();
            }
        }

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
            ShowSprite(SpriteFactory.Instance.CreateKoopaSprite(color, state.GetType().Name + facingString));
        }

        private void ChangeFacing(FacingMode facing)
        {
            Facing = facing;

            // Notice: The effect should be the same as changing state
            UpdateSprite();
        }

        protected override void OnUpdate(int time)
        {
            state.Update(time);
        }

        protected override void OnSimulation(int time)
        {
            ApplyGravity();

            if (state is MovingShell)
            {
                if (Facing == FacingMode.left)
                {
                    SetHorizontalVelocity(-Const.VELOCITY_KOOPA_SHELL);
                }
                else
                {
                    SetHorizontalVelocity(Const.VELOCITY_KOOPA_SHELL);
                }
            }
            else
            {
                if (Facing == FacingMode.left)
                {
                    SetHorizontalVelocity(-Const.VELOCITY_KOOPA);
                }
                else
                {
                    SetHorizontalVelocity(Const.VELOCITY_KOOPA);
                }
            }


            base.OnSimulation(time);
        }

        protected override void OnCollision(IGameObject target, CollisionMode mode, CollisionMode modePassive,
            CornerMode corner, CornerMode cornerPassive)
        {
            switch (target.GetType().Name)
            {
                case "MarioCharacter":
                    //TODO: Fire to be added
                    Mario mario = (Mario) target; //TODO: fire as target to be added
                    if (mario.ProtectionState is Starred)
                    {
                        Defeat();
                    }
                    else
                    {
                        if (state is Normal || state is MovingShell)
                        {
                            if (mode == CollisionMode.Top)
                            {
                                JumpOn();
                            }
                            else
                            {
                                mario.Downgrade();
                            }
                        }
                        else if (state is Defeated)
                        {
                            if (mode == CollisionMode.Left)
                            {
                                ChangeFacing(FacingMode.right);
                                Pushed();
                            }
                            else if (mode == CollisionMode.Right)
                            {
                                ChangeFacing(FacingMode.left);
                                Pushed();
                            }
                            else if (mode == CollisionMode.Top && corner == CornerMode.Left)
                            {
                                ChangeFacing(FacingMode.right);
                                Pushed();
                            }
                            else if (mode == CollisionMode.Top && corner == CornerMode.Right)
                            {
                                ChangeFacing(FacingMode.left);
                                Pushed();
                            }
                        }
                    }

                    break;
                case "Brick":
                    if (((Brick) target).State is Hidden)
                    {
                        break;
                    }
                    goto case "Stair";
                case "Question":
                    if (((Question) target).State is Objects.Blocks.QuestionStates.Hidden)
                    {
                        break;
                    }
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
                case "Fire":
                    Defeat();
                    break;
            }
            if (target is Koopa koopa)
            {
                if (koopa.State is MovingShell)
                {
                    Defeat();
                }
            }
        }

        protected override void OnCollideViewport(IPlayer player, CollisionMode mode, CollisionMode modePassive) { }

        protected override void OnCollideWorld(CollisionMode mode, CollisionMode modePassive) { }

        protected override void OnDraw(int time, SpriteBatch spriteBatch) { }

        public void JumpOn()
        {
            ScorePoints(Const.SCORE_KOOPA);
            State.JumpOn();
        }

        public void Pushed()
        {
            // TODO: temporary fix
            if (Facing == FacingMode.right)
            {
                Move(new Point(1, 0));
            }
            else
            {
                Move(new Point(-1, 0));
            }

            State.Pushed();
        }

        public void Defeat()
        {
            ScorePoints(Const.SCORE_KOOPA);
            new PopingUpPoints(World, Boundary.Location, Const.SCORE_KOOPA);
            RemoveSelf();
        }
    }
}