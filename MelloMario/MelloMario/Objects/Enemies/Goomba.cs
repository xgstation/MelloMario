using System.Diagnostics.CodeAnalysis;
using MelloMario.Objects.Blocks;
using MelloMario.Objects.Enemies.KoopaStates;
using MelloMario.Factories;
using MelloMario.Objects.Characters;
using MelloMario.Objects.Characters.ProtectionStates;
using MelloMario.Theming;
using MelloMario.Objects.UserInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Defeated = MelloMario.Objects.Enemies.GoombaStates.Defeated;

namespace MelloMario.Objects.Enemies
{
    using Blocks.QuestionStates;
    using Characters.MovementStates;

    internal class Goomba : BasePhysicalObject
    {
        private IGoombaState state;

        //This suppression exists because this constructor is inderectly used by the json parser.
        //removing this constructor will cause a runtime error when trying to read in the level.
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public Goomba(IGameWorld world, Point location, IListener listener) : this(world, location, Database.GetCharacterLocation(), listener) { }

        public Goomba(IGameWorld world, Point location, Point marioLoc, IListener listener) : base(world, location, listener, new Point(32, 32), 32)
        {
            if (marioLoc.X < location.X)
            {
                Facing = FacingMode.left;
            }
            else
            {
                Facing = FacingMode.right;
            }

            state = new GoombaStates.Normal(this);
            UpdateSprite();
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

        private void UpdateSprite()
        {
            ShowSprite(SpriteFactory.Instance.CreateGoombaSprite(state.GetType().Name));
        }

        protected override void OnUpdate(int time)
        {
            state.Update(time);
        }

        protected override void OnSimulation(int time)
        {
            ApplyGravity();

            if (Facing == FacingMode.left)
            {
                SetHorizontalVelocity(-Const.VELOCITY_GOOMBA);
            }
            else
            {
                SetHorizontalVelocity(Const.VELOCITY_GOOMBA);
            }

            base.OnSimulation(time);
        }

        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        protected override void OnCollision(IGameObject target, CollisionMode mode, CollisionMode modePassive, CornerMode corner, CornerMode cornerPassive)
        {
            if (state is Defeated)
            {
                return;
            }
            switch (target.GetType().Name)
            {
                case "MarioCharacter":
                    //TODO: Fire to be added
                    Mario mario = (Mario) target;
                    if (mode == CollisionMode.Top && mario.MovementState is Jumping || mario.ProtectionState is Starred)
                    {
                        Defeat();
                    }
                    break;
                case "Brick":
                    if (((Brick) target).State is Blocks.BrickStates.Hidden)
                    {
                        break;
                    }
                    goto case "Stair";
                case "Question":
                    if (((Question) target).State is Hidden)
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
                case "Koopa":
                    if (target is Koopa koopa)
                    {
                        if (koopa.State is MovingShell)
                        {
                            Defeat();
                        }
                    }
                    break;
                case "Fire":
                    Defeat();
                    break;
            }
        }

        protected override void OnCollideViewport(IPlayer player, CollisionMode mode, CollisionMode modePassive) { }

        protected override void OnCollideWorld(CollisionMode mode, CollisionMode modePassive) { }

        protected override void OnDraw(int time, SpriteBatch spriteBatch) { }

        private void ChangeFacing(FacingMode facing)
        {
            Facing = facing;

            // Notice: The effect should be the same as changing state
            UpdateSprite();
        }

        public void Defeat()
        {
            ScorePoints(Const.SCORE_GOOMBA);
            new PopingUpPoints(World, Boundary.Location, Const.SCORE_GOOMBA);
            State.Defeat();
        }
    }
}
