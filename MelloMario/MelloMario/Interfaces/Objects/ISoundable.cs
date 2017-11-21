namespace MelloMario
{
    #region

    using System;

    #endregion

    internal delegate void SoundHandler(ISoundable s, EventArgs e);

    internal interface ISoundable
    {
        event SoundHandler SoundEvent;
    }
}
