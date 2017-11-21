namespace MelloMario.Sounds
{
    #region

    using Microsoft.Xna.Framework.Media;

    #endregion

    internal class SoundTrack : ISoundTrack
    {
        private readonly Song instance;

        public SoundTrack(Song instance)
        {
            this.instance = instance;
        }

        public void Play()
        {
            MediaPlayer.Play(instance);
        }
    }
}
