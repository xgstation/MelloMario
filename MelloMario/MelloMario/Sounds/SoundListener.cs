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
    internal class SoundListener : IListener<ISoundable>
    {
        public void Subscribe(ISoundable soundObject)
        {
            soundObject.SoundEvent += Sound;
        }

        private void Sound(ISoundable c, EventArgs e)
        {
            switch (c)
            {
                case Coin _:
                    SoundFactory.Instance.CreateSoundEffect("Coin");
                    break;
                case MarioCharacter mario:
                    MarioSoundEffect(mario, e as MarioSoundArgs);
                    break;
            }
        }

        private void MarioSoundEffect(MarioCharacter mario, MarioSoundArgs e)
        {
            if (e.ActionCalled == null)
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
                case "UpgradeToFire":
                case "SuperCreate":
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
        }

        private void PlayEffect(string s)
        {
            SoundFactory.Instance.CreateSoundEffect(s);
        }
    }
}
