namespace MelloMario
{
    #region

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    #endregion

    internal interface IGameObject
    {
        Rectangle Boundary { get; }

        void Update(int time);
        void Draw(int time, SpriteBatch spriteBatch);
    }
}
