using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.Objects.Blocks
{
    using MelloMario.Factories;
    using MelloMario.Sounds.Effects;
    using Microsoft.Xna.Framework;

    internal class Flag : BaseGameObject, ISoundable
    {
        private int height;

        private bool pulling;

        public Flag(IWorld world, Point location, Point size, int height) : base(world, location, size)
        {
            this.height = height;
            pulling = false;
            SoundEventArgs = new SoundArgs();
            ShowSprite(SpriteFactory.Instance.CreateFlagSprite(false));
        }

        protected override void OnUpdate(int time)
        {
        }


        protected override void OnSimulation(int time)
        {
            if (!pulling)
            {
                return;
            }
            while (height > 0)
            {
                height--;
                Location = new Point(Location.X, Location.Y + 32);
                World.Move(this);
            }
        }

        public void PullDown()
        {
            pulling = true;
            SoundEventArgs.SetMethodCalled();
            SoundEvent?.Invoke(this, SoundEventArgs);
        }

        public ISoundArgs SoundEventArgs { get; }
        public event SoundHandler SoundEvent;
    }
}
