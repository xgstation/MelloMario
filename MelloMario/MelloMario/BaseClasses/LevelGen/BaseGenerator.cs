namespace MelloMario.LevelGen.Generators
{
    #region

    using System;
    using Microsoft.Xna.Framework;

    #endregion

    internal abstract class BaseGenerator : IGenerator
    {
        protected IListener<IGameObject> ScoreListener;
        protected IListener<ISoundable> SoundListener;

        protected BaseGenerator(
            IListener<IGameObject> scoreListener,
            IListener<ISoundable> soundListener)
        {
            ScoreListener = scoreListener;
            SoundListener = soundListener;
        }

        public void Request(IWorld world, Rectangle range)
        {
            OnRequest(world, range);
        }

        protected abstract void OnRequest(IWorld world, Rectangle range);
    }
}
