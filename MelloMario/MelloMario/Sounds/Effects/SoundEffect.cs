namespace MelloMario.Sounds.Effects
{
    #region

    using Microsoft.Xna.Framework.Audio;

    #endregion

    internal class SoundEffect : ISoundEffect
    {
        public SoundEffect(SoundEffectInstance instance)
        {
            instance.Play();
        }
    }
}
