namespace MelloMario.Sounds.Tracks
{
    #region

    using MelloMario.Factories;
    using MelloMario.Objects.Characters;
    using MelloMario.Objects.Characters.ProtectionStates;
    using Microsoft.Xna.Framework.Media;

    #endregion

    internal class SoundTrackManager
    {
        private static readonly ISoundTrack Normal = SoundFactory.Instance.CreateSoundTrack("Normal");
        private static readonly ISoundTrack Hurry = SoundFactory.Instance.CreateSoundTrack("Hurry");
        private static readonly ISoundTrack BelowGround = SoundFactory.Instance.CreateSoundTrack("BelowGround");
        private static readonly ISoundTrack Star = SoundFactory.Instance.CreateSoundTrack("Star");

        private ISoundTrack currentTrack;
        private IModel model;
        private IPlayer player;

        public SoundTrackManager()
        {
            model = null;
        }

        public void BindPlayer(IPlayer newPlayer)
        {
            player = newPlayer;
        }

        public void BindModel(IModel newModel)
        {
            model = newModel;
        }

        private void PlayMusic(ISoundTrack track)
        {
            if (track == currentTrack && MediaPlayer.State != MediaState.Stopped)
            {
                return;
            }
            MediaPlayer.Stop();
            track?.Play();
            MediaPlayer.IsRepeating = true;
            currentTrack = track;
        }

        public void ToggleMute()
        {
            MediaPlayer.IsMuted = !MediaPlayer.IsMuted;
        }

        private static void Pause()
        {
            MediaPlayer.Pause();
        }

        private static void Resume()
        {
            MediaPlayer.Resume();
        }

        private void UpdatePausing()
        {
            //Pausing state detector
            if (model.State == GameState.pause)
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

        private void UpdateBGM()
        {
            //BGM Updater
            switch (player?.Character)
            {
                case MarioCharacter mario when mario.ProtectionState is Dead:
                    MediaPlayer.Stop();
                    break;
                case MarioCharacter mario when mario.ProtectionState is Starred:
                    PlayMusic(Star);
                    break;
                case MarioCharacter mario when mario.Player.TimeRemain <= 90000:
                    PlayMusic(Hurry);
                    break;
                case MarioCharacter mario when mario.CurrentWorld.Type == WorldType.normal:
                    PlayMusic(Normal);
                    break;
                case MarioCharacter mario when mario.CurrentWorld.Type == WorldType.underground:
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
            UpdateBGM();
        }
    }
}
