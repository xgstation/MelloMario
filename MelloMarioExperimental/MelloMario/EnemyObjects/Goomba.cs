using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.MarioObjects;
using MelloMario.EnemyObjects.GoombaStates;
using MelloMario.BlockObjects;
using MelloMario.UIObjects;
using MelloMario.Theming;

namespace MelloMario.EnemyObjects
{
    class Goomba : BasePhysicalObject
    {
        private IGoombaState state;
        private const int VELOCITY_LR = 1;

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
            if (Facing == FacingMode.right)
            {
                Move(new Point(VELOCITY_LR, 0));
            }
            else
            {
                Move(new Point(-VELOCITY_LR, 0));
            }

            base.OnSimulation(time);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        protected override void OnCollision(IGameObject target, CollisionMode mode, CollisionMode modePassive, CornerMode corner, CornerMode cornerPassive)
        {
            if (state is Defeated)
            {
                return;
            }
            switch (target.GetType().Name)
            {
                case "PlayerMario":
                    //TODO: Fire to be added
                    Mario mario = (Mario) target;
                    if (mode == CollisionMode.Top || mario.ProtectionState is MarioObjects.ProtectionStates.Starred)
                    {
                        Defeat();
                    }
                    break;
                case "Brick":
                    if (((Brick) target).State is BlockObjects.BrickStates.Hidden)
                    {
                        break;
                    }
                    goto case "Stair";
                case "Question":
                    if (((Question) target).State is BlockObjects.QuestionStates.Hidden)
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
                        if (koopa.State is KoopaStates.MovingShell)
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

        protected override void OnCollideViewport(IPlayer player, CollisionMode mode, CollisionMode modePassive)
        {
        }

        protected override void OnCollideWorld(CollisionMode mode, CollisionMode modePassive)
        {
        }

        protected override void OnDraw(int time, Rectangle viewport)
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

        //This suppression exists because this constructor is inderectly used by the json parser.
        //removing this constructor will cause a runtime error when trying to read in the level.
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public Goomba(IGameWorld world, Point location, Listener listener) : this(world, location, GameDatabase.GetCharacterLocation(), listener) { }
        public Goomba(IGameWorld world, Point location, Point marioLoc, Listener listener) : base(world, location, listener, new Point(32, 32), 32)
        {

            if (marioLoc.X < location.X)
            {
                Facing = FacingMode.left;
            }
            else
            {
                Facing = FacingMode.right;
            }

            state = new Normal(this);
            UpdateSprite();
        }

        public void Defeat()
        {
            ScorePoints(GameConst.SCORE_GOOMBA);
            new PopingUpPoints(world, Boundary.Location, GameConst.SCORE_GOOMBA);
            State.Defeat();
        }
    }
}
