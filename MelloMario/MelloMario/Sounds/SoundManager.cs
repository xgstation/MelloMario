namespace MelloMario.Sounds
{
    #region

    #endregion

    internal class SoundManager
    {
        public readonly IListener<ISoundable> SoundEffectListener;
        private readonly SoundTrackManager sounds;

        public SoundManager()
        {
            SoundEffectListener = new SoundEffectListener();
            sounds = new SoundTrackManager();
        }

        public void ToggleMute()
        {
            sounds.ToggleMute();
            (SoundEffectListener as SoundEffectListener).ToggleMute();
        }

        public void Update(int time)
        {
        }

        public void Initialize()
        {
        }
    }
}
