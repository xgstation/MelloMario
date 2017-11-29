namespace MelloMario.Sounds.Tracks
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

        public override bool Equals(object obj)
        {
            return instance.Equals((obj as SoundTrack)?.instance);
        }

        protected bool Equals(SoundTrack other)
        {
            return Equals(instance, other.instance);
        }

        public override int GetHashCode()
        {
            return (instance != null ? instance.GetHashCode() : 0);
        }
    }
}
