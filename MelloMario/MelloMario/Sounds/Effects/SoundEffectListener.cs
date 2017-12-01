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
    using MelloMario.Objects.Enemies;
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

        private void Sound(ISoundable c, ISoundArgs e)
        {
            switch (c)
            {
                case Thwomp _:
                    BlockSoundEffect(c, e);
                    break;
                case Flag _:
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

        private void MarioSoundEffect(Mario mario, ISoundArgs e)
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
                case "KillMario":
                    PlayEffect("Death");
                    break;
                case "Jump":
                    PlayEffect(mario.PowerUpState is Standard ? "Bounce" : "PowerBounce");
                    break;
                default:
                    break;
            }
        }

        private void BlockSoundEffect(ISoundable s, ISoundArgs e)
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
                case Flag _:
                    PlayEffect(methodName == "OnCollision" ? "GameWon" : null);
                    break;
                case Thwomp _:
                    PlayEffect(methodName == "OnSimulation" ? "Thwomp" : null);
                    break;
                case Goomba _:
                    PlayEffect(methodName == "Defeat" ? "EnemyKill" : null);
                    break;
            }
        }

        private void ItemSoundEffect(ISoundable s, ISoundArgs e)
        {
            if (s is Star ss)
            {
                Debug.WriteLine(ss.State.GetType().Name);
            }
            switch (s)
            {
                case Brick _:
                case Question _:
                case MarioCharacter _:
                case Mario _:
                    break;
                case Coin coin when coin.State.GetType().Name == "Unveil":
                    //PlayEffect(coin.State.GetType().Name == "Unveil" ? "Unveil" : "Coin");
                    PlayEffect(s is Coin ? "Coin" : "Unveil");
                    break;
                case Coin _ when e.MethodCalled == "Collect":
                    //PlayEffect("Coin");
                    PlayEffect(s is Coin ? "Coin" : "Unveil");
                    break;
                case FireFlower fireFlower when fireFlower.State.GetType().Name == "Unveil":
                case OneUpMushroom oneUpMushroom when oneUpMushroom.State.GetType().Name == "Unveil":
                case Star star when star.State.GetType().Name == "Unveil":
                case SuperMushroom superMushroom when superMushroom.State.GetType().Name == "Unveil":
                    PlayEffect("Unveil");
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
