using System;
using MelloMario.Theming;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static System.Single;

namespace MelloMario.MiscObjects
{
    internal class Camera : ICamera
    {
        public Rectangle Viewport { get; private set; }

        public Point Offset
        {
            get
            {
                return new Point(GameConst.FOCUS_Y, GameConst.FOCUS_Y);
            }
        }

        public Camera()
        {
            Viewport = new Rectangle(0, 0, GameConst.SCREEN_WIDTH, GameConst.SCREEN_HEIGHT);
        }

        public Matrix GetViewMatrix(Vector2 parallax)
        {
            return Matrix.CreateTranslation(new Vector3(-Viewport.Location.ToVector2() * parallax, 0.0f));
        }

        public void LookAt(Point target, Rectangle boundary)
        {
            Viewport = new Rectangle(
                MathHelper.Clamp(target.X - Offset.X, boundary.Left, boundary.Right - GameConst.SCREEN_WIDTH),
                MathHelper.Clamp(target.Y - Offset.Y, boundary.Top, boundary.Bottom - GameConst.SCREEN_HEIGHT),
                GameConst.SCREEN_WIDTH,
                GameConst.SCREEN_HEIGHT
            );
        }
    }
}
