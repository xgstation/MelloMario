namespace MelloMario
{
    #region

    // TODO: interface should not depend on implementation
    using MelloMario.Sounds;

    #endregion

    internal delegate void SoundHandler(ISoundable s, ref SoundArgs e);

    internal interface ISoundable
    {
        event SoundHandler SoundEvent;
    }
}
