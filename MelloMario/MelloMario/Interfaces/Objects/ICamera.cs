namespace MelloMario
{
    #region

    using Microsoft.Xna.Framework;

    #endregion

    internal interface ICamera
    {
        Rectangle Viewport { get; }

        Point Offset { get; }

        Matrix GetViewMatrix(Vector2 parallax);

        void LookAt(Point target, Rectangle boundary);
    }
}
