namespace MelloMario.Sounds
{
    #region

    using MelloMario.Factories;
    using MelloMario.Objects.Characters;
    using MelloMario.Objects.Characters.MovementStates;
    using MelloMario.Objects.Characters.PowerUpStates;
    using MelloMario.Objects.Characters.ProtectionStates;
    using Microsoft.Xna.Framework.Media;

    #endregion

    internal class SoundManager
    {
        private IModel Model;

        private static readonly ISoundTrack Normal = SoundFactory.Instance.CreateSoundTrack("Normal");
        private static readonly ISoundTrack Hurry = SoundFactory.Instance.CreateSoundTrack("Hurry");
        private static readonly ISoundTrack BelowGround = SoundFactory.Instance.CreateSoundTrack("BelowGround");
        private static readonly ISoundTrack Star = SoundFactory.Instance.CreateSoundTrack("Star");

        private ISoundTrack CurrentTrack;
        private float SoundEffectVolume = Microsoft.Xna.Framework.Audio.SoundEffect.MasterVolume;

        public SoundManager(IModel model)
        {
            Model = model;
        }

        private void PlayMusic(ISoundTrack track, bool reset = false)
        {
            if (track != CurrentTrack || reset)
            {
                MediaPlayer.Stop();
                track?.Play();
                MediaPlayer.IsRepeating = true;
                CurrentTrack = track;
            }
        }

        public void ToggleMute()
        {
            MediaPlayer.IsMuted = !MediaPlayer.IsMuted;
            Microsoft.Xna.Framework.Audio.SoundEffect.MasterVolume = MediaPlayer.IsMuted ? 0 : SoundEffectVolume;
            SoundEffectVolume = MediaPlayer.IsMuted
                ? SoundEffectVolume
                : Microsoft.Xna.Framework.Audio.SoundEffect.MasterVolume;
        }

        private void Pause()
        {
            MediaPlayer.Pause();
        }

        private void Resume()
        {
            MediaPlayer.Resume();
        }

        private void UpdatePausing()
        {
            //Pausing state detector
            if (Model.IsPaused)
            {
                //Avoid repeat pausing
                if (MediaPlayer.State != MediaState.Paused)
                {
                    Pause();
                }
            }
            else if (MediaPlayer.State == MediaState.Paused)
            {
                Resume();
            }
        }

        //TODO: Finish Other Sound Effeect
        private void UpdateMarioSoundEffect()
        {
            //TODO: Finish Mario Sound Effeect
            MarioCharacter mario = Model.ActivePlayer.Character as MarioCharacter;
            if (mario?.MovementState is Jumping jumping && !jumping.Finished)
            {
                if (mario.PowerUpState is Super || mario.PowerUpState is Fire)
                {
                    SoundFactory.Instance.CreateSoundEffect("PowerBounce");
                }
                else
                {
                    SoundFactory.Instance.CreateSoundEffect("Bounce");
                }
            }
            if (mario?.ProtectionState is Dead)
            {
                SoundFactory.Instance.CreateSoundEffect("Death");
            }
        }

        private void UpdateBGM()
        {
            //BGM Updater
            switch (Model.ActivePlayer.Character)
            {
                case MarioCharacter mario when mario.ProtectionState is Dead:
                    PlayMusic(Normal);
                    break;
                case MarioCharacter mario when mario.ProtectionState is Starred:
                    PlayMusic(Star);
                    break;
                case MarioCharacter mario when mario.Player.TimeRemain <= 90000:
                    PlayMusic(Hurry);
                    break;
                case MarioCharacter mario when mario.CurrentWorld.Type == "Normal":
                    PlayMusic(Normal);
                    break;
                case MarioCharacter mario when mario.CurrentWorld.Type == "Sub":
                    PlayMusic(BelowGround);
                    break;
                default:
                    PlayMusic(Normal);
                    break;
            }
        }

        public void Update()
        {
            UpdatePausing();
            UpdateMarioSoundEffect();
            UpdateBGM();
        }
    }
}
