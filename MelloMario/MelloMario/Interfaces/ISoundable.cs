namespace MelloMario
{
    #region
    
    using System;
    using System.Reflection;

    #endregion

    internal delegate void SoundHandler(ISoundable s, SoundArgsBase e);

    internal interface ISoundable
    {
        event SoundHandler SoundEvent;
        SoundArgsBase SoundEventArgs { get; }
    }

    internal abstract class SoundArgsBase : EventArgs
    {
        protected MethodBase Method;
        public abstract bool HasArgs { get; }
        public abstract MethodBase MethodCalled { get; protected set; }
        public abstract void SetMethodCalled();
    }

}
