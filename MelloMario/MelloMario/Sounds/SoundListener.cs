namespace MelloMario.Sounds
{
    #region

    using System;
    using MelloMario.Factories;
    using MelloMario.Objects.Items;

    #endregion

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
