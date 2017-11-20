using MelloMario.Factories;
using MelloMario.Objects.Characters;
using MelloMario.Objects.Characters.PowerUpStates;
using MelloMario.Objects.Characters.ProtectionStates;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace MelloMario.Sounds
{
    internal static class SoundController
    {
        private static IModel Model;

        public static void Initialize(IModel model)
        {
            Model = model;
        }
        private enum Songs
        {
            Idle,
            Normal,
            BelowGround,
            Hurry,
            Title,
            GameOver,
            Star
        }

        private static Songs CurrentBGM = Songs.Idle;
        private static float SoundEffectVolume = Microsoft.Xna.Framework.Audio.SoundEffect.MasterVolume;

        private static bool PlayFinished = false;

        private static readonly Song Normal = SoundFactory.Instance.CreateSong("01-main-theme-overworld");
        private static readonly Song BelowGround = SoundFactory.Instance.CreateSong("02-underworld");
        private static readonly Song Star = SoundFactory.Instance.CreateSong("05-starman");
        private static readonly Song Hurry = SoundFactory.Instance.CreateSong("18-hurry-overworld-");

        //public static Song GameOver = SoundFactory.Instance.CreateSong("09-game-over");

        private static void PlayMusic(Songs song, bool reset = false)
        {
            if (CurrentBGM != song || reset)
            {
                MediaPlayer.Stop();
                switch (song)
                {
                    case Songs.Normal:
                        MediaPlayer.Play(Normal);
                        break;
                    case Songs.Hurry:
                        MediaPlayer.Play(Hurry);
                        break;
                    case Songs.BelowGround:
                        MediaPlayer.Play(BelowGround);
                        break;
                    case Songs.Star:
                        MediaPlayer.Play(Star);
                        break;
                    default:
                        MediaPlayer.Stop();
                        break;
                }
                MediaPlayer.IsRepeating = true;
                CurrentBGM = song;
            }
        }

        public static void ToggleMute()
        {
            MediaPlayer.IsMuted = !MediaPlayer.IsMuted;
            Microsoft.Xna.Framework.Audio.SoundEffect.MasterVolume = MediaPlayer.IsMuted ? 0 : SoundEffectVolume;
            SoundEffectVolume = MediaPlayer.IsMuted ? SoundEffectVolume : Microsoft.Xna.Framework.Audio.SoundEffect.MasterVolume;
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

        //TODO: Finish Other Sound Effeect
        private static void UpdateMarioSoundEffect()
        {
            //TODO: Finish Mario Sound Effeect
            MarioCharacter mario = Model.ActivePlayer.Character as MarioCharacter;
            if (mario?.MovementState is Objects.Characters.MovementStates.Jumping jumping && !jumping.Finished)
            {
                if (mario.PowerUpState is Super || mario.PowerUpState is Fire)
                {
                    PowerBounce.Play();
                }
                else
                {
                    Bounce.Play();
                }
            }
            if (mario?.ProtectionState is Dead)
            {
                Death.Play();
            }
        }

        private static void UpdateBGM()
        {
            //BGM Updater
            switch (Model.ActivePlayer.Character)
            {
                case MarioCharacter mario when mario.ProtectionState is Dead:
                    PlayMusic(Songs.Normal);
                    break;
                case MarioCharacter mario when mario.ProtectionState is Starred:
                    PlayMusic(Songs.Star);
                    break;
                case MarioCharacter mario when mario.Player.TimeRemain <= 90000:
                    PlayMusic(Songs.Hurry);
                    break;
                case MarioCharacter mario when mario.CurrentWorld.Type == "Normal":
                    PlayMusic(Songs.Normal);
                    break;
                case MarioCharacter mario when mario.CurrentWorld.Type == "Sub":
                    PlayMusic(Songs.BelowGround);
                    break;
                default:
                    PlayMusic(Songs.Normal);
                    break;
            }
        }
        public static void Update()
        {
            UpdatePausing();
            UpdateMarioSoundEffect();
            UpdateBGM();
        }
    }
}
