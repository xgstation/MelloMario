namespace MelloMario
{
    #region

    using System;

    #endregion

    internal delegate void SoundHandler(ISoundable s, ISoundArgs e);

    internal interface ISoundable
    {
        event SoundHandler SoundEvent;
        ISoundArgs SoundEventArgs { get; }
    }

    internal interface ISoundArgs
    {
        bool HasArgs { get; }
        string MethodCalled { get; }
        void SetMethodCalled();
    }

}
