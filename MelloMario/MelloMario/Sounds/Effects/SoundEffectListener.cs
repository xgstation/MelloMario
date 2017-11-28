namespace MelloMario.Sounds.Effects
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

    internal class SoundArgs : EventArgs, ISoundArgs
    {
        private MethodBase method;

        public bool HasArgs
        {
            get
            {
                return method != null;
            }
        }

        public string MethodCalled
        {
            get
            {
                string result = method.Name;
                method = null;
                return result;
            }
        }

        public void SetMethodCalled()
        {
            StackTrace stackTrace = new StackTrace();
            method = stackTrace.GetFrame(1).GetMethod();
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

        private static void Sound(ISoundable c, ISoundArgs e)
        {
            switch (c)
            {
                case Objects.Enemies.Thwomp _:
                    BlockSoundEffect(c, e);
                    break;
                case FireFlower _:
                case SuperMushroom _:
                case Star _:
                case OneUpMushroom _:
                case Coin _:
                    ItemSoundEffect(c, e);
                    break;
                case Question _:
                case Brick _:
                    BlockSoundEffect(c, e);
                    break;
                case Mario mario:
                    MarioSoundEffect(mario, e);
                    break;
            }
            e?.SetMethodCalled();
        }

        private static void MarioSoundEffect(Mario mario, ISoundArgs e)
        {
            if (!e.HasArgs)
            {
                return;
            }
            switch (e.MethodCalled)
            {
                case "OnDeath":
                    PlayEffect("Death");
                    break;
                case "Action":
                    //TODO: Add Fireball Shotting Sound
                    break;
                case "UpgradeToSuper":
                case "UpgradeToFire":
                case "SuperCreate":
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

        private static void BlockSoundEffect(ISoundable s, ISoundArgs e)
        {
            if (!e.HasArgs)
            {
                return;
            }
            string methodName = e.MethodCalled;
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
                case Objects.Enemies.Thwomp _:
                    PlayEffect("Thwomp");
                    break;
            }
        }

        private static void ItemSoundEffect(ISoundable s, ISoundArgs e)
        {
            if (s.GetType().GetProperty("State")?.GetValue(s, null).GetType().Name == "Unveil")
            {
                PlayEffect(s is Coin ? "Coin" : "Unveil");
            }
            else if (e.MethodCalled == "Collect")
            {
                PlayEffect("Coin");
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
