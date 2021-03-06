﻿namespace MelloMario.Objects.Characters
{
    #region

    using System;
    using MelloMario.Objects.Characters.MovementStates;
    using MelloMario.Objects.Characters.PowerUpStates;
    using MelloMario.Objects.Characters.ProtectionStates;
    using MelloMario.Objects.Items;
    using MelloMario.Theming;
    using Microsoft.Xna.Framework;

    #endregion

    internal class MarioCharacter : Mario, ICharacter
    {
        // TODO: create AnimationStates
        private enum Animation
        {
            none,
            teleport,
            flagpole,
            dead
        }

        private Animation animation;

        private int toBeTraveled;

        private Vector2 userInput;
        

        public MarioCharacter(
            IWorld world,
            IPlayer player,
            Point location,
            IListener<IGameObject> listener,
            IListener<ISoundable> soundListener) : base(
            world,
            location,
            listener,
            soundListener)
        {
            Player = player;
        }

        public bool Active
        {
            get
            {
                return animation == Animation.none;
            }
        }

        public IWorld CurrentWorld
        {
            get
            {
                return World;
            }
        }

        public IPlayer Player { get; }

        public Rectangle Sensing
        {
            get
            {
                Point location = Boundary.Location - new Point(Const.SCREEN_WIDTH, Const.SCREEN_HEIGHT);
                Point size = new Point(
                    Const.SCREEN_WIDTH * 2,
                    Const.SCREEN_HEIGHT * 2); // notice: should be greater than viewport

                return new Rectangle(location, size);
            }
        }

        public void Left()
        {
            if (animation == Animation.none)
            {
                MovementState.Walk();

                if (Facing == FacingMode.right)
                {
                    ChangeFacing(FacingMode.left);
                }

                if (!(MovementState is Crouching))
                {
                    userInput.X -= Const.ACCEL_INPUT_X;
                }
            }
        }

        public void LeftPress()
        {
            if (animation == Animation.none)
            {
                Left();
            }
        }

        public void LeftRelease()
        {
            if (animation == Animation.none)
            {
                MovementState.Idle();
            }
        }

        public void Right()
        {
            if (animation == Animation.none)
            {
                MovementState.Walk();

                if (Facing == FacingMode.left)
                {
                    ChangeFacing(FacingMode.right);
                }

                if (!(MovementState is Crouching))
                {
                    userInput.X += Const.ACCEL_INPUT_X;
                }
            }
        }

        public void RightPress()
        {
            if (animation == Animation.none)
            {
                Right();
            }
        }

        public void RightRelease()
        {
            if (animation == Animation.none)
            {
                MovementState.Idle();
            }
        }

        public void Jump()
        {
            if (animation == Animation.none)
            {
                MovementState.Jump();

                if (MovementState is Jumping jumping && !jumping.Finished)
                {
                    SoundEventArgs.SetMethodCalled();
                    userInput.Y -= Const.ACCEL_INPUT_Y + Const.ACCEL_G;
                }
            }
        }

        public void JumpPress()
        {
            if (animation == Animation.none)
            {
                Jump();
            }
        }

        public void JumpRelease()
        {
            if (animation == Animation.none)
            {
                if (MovementState is Jumping jumping)
                {
                    jumping.Finished = true;
                }
            }
        }

        public void Crouch()
        {
            if (animation == Animation.none)
            {
                MovementState.Crouch();
            }
        }

        public void CrouchPress()
        {
            if (animation == Animation.none)
            {
                Crouch();
            }
        }

        public void CrouchRelease()
        {
            if (animation == Animation.none)
            {
                MovementState.Idle();
            }
        }

        public void FireCreate()
        {
            SoundEventArgs.SetMethodCalled();
            PowerUpState.UpgradeToFire();
        }

        public void SuperCreate()
        {
            SoundEventArgs.SetMethodCalled();
            PowerUpState.UpgradeToSuper();
        }

        public void NormalCreate()
        {
            if (PowerUpState != null || PowerUpState is Standard)
            {
                return;
            }
            SoundEventArgs.SetMethodCalled();
            PowerUpState.Downgrade();
            if (PowerUpState is Fire)
            {
                PowerUpState.Downgrade();
            }
        }

        public void KillMario()
        {
            if (PowerUpState != null || PowerUpState is Standard)
            {
                PowerUpState.Downgrade();
                return;
            }
            SoundEventArgs.SetMethodCalled();
            PowerUpState.Downgrade();
            if (PowerUpState is Fire)
            {
                PowerUpState.Downgrade();
                PowerUpState.Downgrade();
            }
        }

        public void Action()
        {
            //    if (animation != Animation.none)
            //    {
            //        return;
            //    }
            if (PowerUpState is Fire)
            {
                SoundEventArgs.SetMethodCalled();
                bool facingRight = Facing is FacingMode.right;
                World.Add(new FireBall(World, Boundary.Location, null,  facingRight));
            }
        }

        public void Move(IWorld newWorld, Point newLocation)
        {
            World.Remove(this);
            World = newWorld;
            World.Add(this);
            Relocate(newLocation);
        }

        public void Remove()
        {
            RemoveSelf();
        }

        public void FlagPole()
        {
            animation = Animation.flagpole;
        }

        //protected void Teleport()
        //{
        //    animation = Animation.teleport;
        //    toBeTraveled = Boundary.Height;
        //    // TODO: initialize
        //}

        protected override void OnSimulation(int time)
        {
            if (animation == Animation.teleport)
            {
                Move(new Point(0, 2));

                toBeTraveled -= 2;
                if (toBeTraveled <= 0)
                {
                    animation = Animation.none;
                }
            }
            base.OnSimulation(time);
        }

        protected override void OnUpdate(int time)
        {
            if (ProtectionState is Dead)
            {
                animation = Animation.dead;
            }

            if (animation == Animation.none)
            {
                ApplyAccel(userInput);
                userInput.X = 0;
                userInput.Y = 0;
            }

            base.OnUpdate(time);
        }
    }
}
