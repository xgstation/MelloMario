using MelloMario.Objects.Characters;

namespace MelloMario.Sounds
{
    #region

    using System;
    using MelloMario.Factories;
    using MelloMario.Objects.Items;

    #endregion

    internal class MarioSoundArgs : EventArgs
    {
        public Action ActionCalled { get; set; }
    }
    internal class SoundEffectListener : IListener<ISoundable>
    {
        private float soundEffectVolume;

        public SoundEffectListener()
        {
            soundEffectVolume = Microsoft.Xna.Framework.Audio.SoundEffect.MasterVolume;
        }
        public void ToggleMute()
        {
            float currentVolume = Microsoft.Xna.Framework.Audio.SoundEffect.MasterVolume;
            Microsoft.Xna.Framework.Audio.SoundEffect.MasterVolume = Math.Abs(currentVolume) < float.Epsilon ? soundEffectVolume : 0;
            soundEffectVolume = Math.Abs(currentVolume) < float.Epsilon ? soundEffectVolume : currentVolume;
        }
        public void Subscribe(ISoundable soundObject)
        {
            soundObject.SoundEvent += Sound;
        }

        private static void Sound(ISoundable c, ref EventArgs e)
        {
            switch (c)
            {
                case Coin _:
                    SoundFactory.Instance.CreateSoundEffect("Coin");
                    break;
                case Mario mario:
                    MarioSoundEffect(mario, e as MarioSoundArgs);
                    break;
            }
        }

        private static void MarioSoundEffect(Mario mario, MarioSoundArgs e)
        {
            if (e?.ActionCalled == null)
            {
                return;
            }
            switch (e.ActionCalled.Method.Name)
            {
                case "OnDeath":
                    PlayEffect("Death");
                    break;
                case "Action":
                    //TODO: Add Fireball Shotting Sound
                    break;
                case "UpgradeToSuper":
                    PlayEffect("PowerUp");
                    break;
                case "UpgradeToFire":
                    PlayEffect("PowerUp");
                    break;
                case "SuperCreate":
                    PlayEffect("PowerUp");
                    break;
                case "FireCreate":
                    PlayEffect("PowerUp");
                    break;
                case "NormalCreate":
                    PlayEffect("Pipe");
                    break;
                case "Jump":
                    if (mario.PowerUpState is Objects.Characters.PowerUpStates.Standard)
                    {
                        PlayEffect("Bounce");
                    }
                    else
                    {
                        PlayEffect("PowerBounce");
                    }
                    break;
                default:
                    break;
            }
            e.ActionCalled = null;
        }

        private static void PlayEffect(string s)
        {
            SoundFactory.Instance.CreateSoundEffect(s);
        }
    }
}
