namespace MelloMario
{
    #region

    using Microsoft.Xna.Framework.Graphics;

    #endregion

    internal interface IUserInterface
    {
        void Update(int time);
        void Draw(int time, SpriteBatch spriteBatch);
    }
}
