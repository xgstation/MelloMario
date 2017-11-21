namespace MelloMario.Objects.Blocks.BrickStates
{
    #region

    using System;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.Remoting.Messaging;
    using MelloMario.Objects.Characters;
    using MelloMario.Objects.Characters.PowerUpStates;
    using MelloMario.Sounds;

    #endregion

    internal class Normal : BaseState<Brick>, IBlockState
    {
        public Normal(Brick owner) : base(owner)
        {
        }

        public void Show()
        {
            //do nothing
        }

        public void Hide()
        {
            Owner.State = new Hidden(Owner);
        }

        public void Bump(Mario mario)
        {
            if (mario.PowerUpState is Standard || Owner.HasInitialItem)
            {
                Owner.SoundEventArgs.SetMethodCalled();
                Owner.State = new Bumped(Owner);
            }
            else
            {
                Owner.State = new Destroyed(Owner);
            }

        }

        public override void Update(int time)
        {
        }
    }
}
