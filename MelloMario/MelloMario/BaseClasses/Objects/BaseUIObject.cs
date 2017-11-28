namespace MelloMario.Objects
{
    #region

    using Microsoft.Xna.Framework.Graphics;

    #endregion

    internal abstract class BaseUIObject : IObject
    {
        public void Update(int time)
        {
            OnUpdate(time);
        }

        public void Draw(int time, SpriteBatch spriteBatch)
        {
            OnDraw(time, spriteBatch);
        }
        
        protected abstract void OnUpdate(int time);
        protected abstract void OnDraw(int time, SpriteBatch spriteBatch);
    }
}
