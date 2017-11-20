namespace MelloMario.Objects.Miscs
{
    #region

    using Microsoft.Xna.Framework;
    using Theming;

    #endregion

    internal class Camera : ICamera
    {
        public Rectangle Viewport { get; private set; }

        public Point Offset
        {
            get
            {
                return new Point(Const.FOCUS_Y, Const.FOCUS_Y);
            }
        }

        public Camera()
        {
            Viewport = new Rectangle(0, 0, Const.SCREEN_WIDTH, Const.SCREEN_HEIGHT);
        }

        public Matrix GetViewMatrix(Vector2 parallax)
        {
            return Matrix.CreateTranslation(new Vector3(-Viewport.Location.ToVector2() * parallax, 0.0f));
        }

        public void LookAt(Point target, Rectangle boundary)
        {
            Viewport = new Rectangle(MathHelper.Clamp(target.X - Offset.X, boundary.Left, boundary.Right - Const.SCREEN_WIDTH), MathHelper.Clamp(target.Y - Offset.Y, boundary.Top, boundary.Bottom - Const.SCREEN_HEIGHT), Const.SCREEN_WIDTH, Const.SCREEN_HEIGHT);
        }
    }
}
