namespace MelloMario.Sounds
{
    #region

    using System;
    using System.Diagnostics;
    using System.Reflection;
    using MelloMario.Factories;
    using MelloMario.Objects.Blocks;
    using MelloMario.Objects.Characters;
    using MelloMario.Objects.Characters.PowerUpStates;
    using MelloMario.Objects.Items;

    #endregion

    internal class SoundArgs : SoundArgsBase
    {
        public override bool HasArgs { get { return Method != null; } }

        public override MethodBase MethodCalled
        {
            get
            {
                try
                {
                    return Method;
                }
                finally
                {
                    Method = null;
                }
            }
            protected set
            {
                Method = value;
            }
        }

        public override void SetMethodCalled()
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

        private static void Sound(ISoundable c, SoundArgsBase e)
        {
            switch (c)
            {
                case Coin _:
                    PlayEffect("Coin");
                    break;
                case Brick _:
                    BlockSoundEffect(c, e);
                    break;
                case Mario mario:
                    MarioSoundEffect(mario, e);
                    break;
            }
            e?.SetMethodCalled();
        }

        private static void MarioSoundEffect(Mario mario, SoundArgsBase e)
        {
            if (!e.HasArgs)
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
                    PlayEffect(mario.PowerUpState is Standard ? "Bounce" : "PowerBounce");
                    break;
                default:
                    break;
            }
        }

        private static void BlockSoundEffect(ISoundable s, SoundArgsBase e)
        {
            if (!e.HasArgs)
            {
                return;
            }
            string methodName = e.MethodCalled.Name;
            switch (s)
            {
                case Brick _:
                    PlayEffect(methodName == "Bump" ? "BumpBlock" : (methodName == "OnDestroy" ? "BreakBlock" : null));
                    break;
                case Question _:
                    PlayEffect(methodName == "Bump" ? "BumpBlock" : null);
                    break;
                case Pipeline _:
                    break;
            }
        }
        private static void PlayEffect(string s)
        {
            if (s == null)
            {
                return;
            }
            SoundFactory.Instance.CreateSoundEffect(s);
        }
    }
}
