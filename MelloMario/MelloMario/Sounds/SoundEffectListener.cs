namespace MelloMario.Sounds
{
    #region

    using System;
    using System.Diagnostics;
    using System.Reflection;
    using MelloMario.Factories;
    using MelloMario.Objects.Blocks;
    using MelloMario.Objects.Items;

    #endregion

    internal class SoundArgs : EventArgs
    {
        public MethodBase MethodCalled { get; set; }

        public void SetMethodCalled()
        {
            StackTrace stackTrace = new StackTrace();
            MethodCalled = stackTrace.GetFrame(1).GetMethod();
        }
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
            Microsoft.Xna.Framework.Audio.SoundEffect.MasterVolume =
                Math.Abs(currentVolume) < float.Epsilon ? soundEffectVolume : 0;
            soundEffectVolume = Math.Abs(currentVolume) < float.Epsilon ? soundEffectVolume : currentVolume;
        }

        public void Subscribe(ISoundable soundObject)
        {
            soundObject.SoundEvent += Sound;
        }

        private static void Sound(ISoundable c, ref SoundArgs e)
        {
            switch (c)
            {
                case Coin _:
                    PlayEffect("Coin");
                    break;
                case Brick _:
                    BlockSoundEffect(c,e);
                    break;
                case Mario mario:
                    MarioSoundEffect(mario, e);
                    break;
            }
            e?.SetMethodCalled();
        }

        private static void MarioSoundEffect(Mario mario, SoundArgs e)
        {
            if (e?.MethodCalled == null)
            {
                return;
            }
            switch (e.MethodCalled.Name)
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
                    if (mario.PowerUpState is Standard)
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
        }

        private static void BlockSoundEffect(ISoundable s, SoundArgs e)
        {
            switch (s)
            {
                case Brick b:
                    if (e?.MethodCalled.Name == "Bump")
                    {
                        PlayEffect("BumpBlock");
                    }
                    else if (e.MethodCalled.Name == "OnDestroy")
                    {
                        PlayEffect("BreakBlock");
                    }
                    break;
                case Question q:
                    break;
                case Pipeline p:
                    break;
            }
        }
        private static void PlayEffect(string s)
        {
            SoundFactory.Instance.CreateSoundEffect(s);
        }
    }
}
