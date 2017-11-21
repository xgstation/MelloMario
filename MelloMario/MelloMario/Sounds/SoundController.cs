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

    internal static class SoundController
    {
        private static IModel Model;

        public static void Initialize(IModel model)
        {
            Model = model;
        }

        private static readonly ISoundTrack Normal = SoundFactory.Instance.CreateSoundTrack("Normal");
        private static readonly ISoundTrack Hurry = SoundFactory.Instance.CreateSoundTrack("Hurry");
        private static readonly ISoundTrack BelowGround = SoundFactory.Instance.CreateSoundTrack("BelowGround");
        private static readonly ISoundTrack Star = SoundFactory.Instance.CreateSoundTrack("Star");

        private static ISoundTrack CurrentTrack;
        private static float SoundEffectVolume = Microsoft.Xna.Framework.Audio.SoundEffect.MasterVolume;

        private static void PlayMusic(ISoundTrack track, bool reset = false)
        {
            if (track != CurrentTrack || reset)
            {
                MediaPlayer.Stop();
                track?.Play();
                MediaPlayer.IsRepeating = true;
                CurrentTrack = track;
            }
        }

        public static void ToggleMute()
        {
            MediaPlayer.IsMuted = !MediaPlayer.IsMuted;
            Microsoft.Xna.Framework.Audio.SoundEffect.MasterVolume = MediaPlayer.IsMuted ? 0 : SoundEffectVolume;
            SoundEffectVolume = MediaPlayer.IsMuted
                ? SoundEffectVolume
                : Microsoft.Xna.Framework.Audio.SoundEffect.MasterVolume;
        }

        private static void Pause()
        {
            MediaPlayer.Pause();
        }

        private static void Resume()
        {
            MediaPlayer.Resume();
        }

        private static void UpdatePausing()
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

        private static void UpdateBGM()
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

        public static void Update()
        {
            UpdatePausing();
            UpdateBGM();
        }
    }
}
