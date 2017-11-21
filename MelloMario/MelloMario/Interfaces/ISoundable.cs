namespace MelloMario
{
    #region

    using System;
    using MelloMario.Sounds;

    #endregion

    internal delegate void SoundHandler(ISoundable s, ref SoundArgs e);

    internal interface ISoundable
    {
        event SoundHandler SoundEvent;
    }
}
