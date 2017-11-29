namespace MelloMario
{
    #region

    #endregion

    internal delegate void SoundHandler(ISoundable s, ISoundArgs e);

    internal interface ISoundable
    {
        ISoundArgs SoundEventArgs { get; }
        event SoundHandler SoundEvent;
    }

    internal interface ISoundArgs
    {
        bool HasArgs { get; }
        string MethodCalled { get; }
        void SetMethodCalled();
    }
}
