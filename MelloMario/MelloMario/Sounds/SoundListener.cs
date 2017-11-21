using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelloMario.Factories;
using MelloMario.Objects.Items;

namespace MelloMario.Sounds
{
    internal class SoundListener : IListener<ISoundable>
    {
        public void Subscribe(ISoundable soundObject)
        {
            soundObject.SoundEvent += Sound;
        }

        private static void Sound(ISoundable c, EventArgs e)
        {
            switch (c)
            {
                case Coin coin:
                    SoundFactory.Instance.CreateSoundEffect("Coin");
                    break;
            }
        }
    }
}
