using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static System.Single;

namespace MelloMario.MiscObjects
{
    internal class Camera : ICamera
    {
        private readonly Viewport viewport;
        private Rectangle? limit;
        private Vector2 location;

        public Camera(Viewport viewport)
        {
            this.viewport = viewport;
            Origin = new Vector2(this.viewport.Width / 2.0f, this.viewport.Height / 2.0f);
            Zoom = 1.0f;
        }

        public Vector2 Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;

                if (Limit == null || !(Math.Abs(Zoom - 1.0f) < Epsilon) || !(Math.Abs(Rotation) < Epsilon))
                {
                    return;
                }
                location.X = MathHelper.Clamp(location.X, Limit.Value.Left, Limit.Value.Right - viewport.Width);
                location.Y = MathHelper.Clamp(location.Y, Limit.Value.Top - viewport.Height, Limit.Value.Bottom);
            }
        }

        public Vector2 Origin { get; set; }

        public float Zoom { get; set; }

        public float Rotation { get; set; }

        public Rectangle? Limit
        {
            get
            {
                return limit;
            }
            set
            {
                if (value != null)
                {
                    // make sure it's always bigger than the viewport
                    limit = new Rectangle
                    {
                        X = value.Value.X,
                        Y = value.Value.Y,
                        Width = Math.Max(viewport.Width, value.Value.Width),
                        Height = Math.Max(viewport.Height, value.Value.Height)
                    };
                }
                else
                {
                    limit = null;
                }
            }
        }

        public Matrix GetViewMatrix(Vector2 parallax)
        {
            return Matrix.CreateTranslation(new Vector3(-Location * parallax, 0.0f)) * Matrix.CreateTranslation(new Vector3(-Origin, 0.0f)) * Matrix.CreateRotationZ(Rotation) * Matrix.CreateScale(Zoom, Zoom, 1.0f) * Matrix.CreateTranslation(new Vector3(Origin, 0.0f));
        }

        public void LookAt(Vector2 newLocation)
        {
            Location = newLocation - new Vector2(viewport.Width / 2.0f, viewport.Height / 2.0f);
        }

        public void Move(Vector2 displacement, bool respectRotation = false)
        {
            if (respectRotation)
            {
                displacement = Vector2.Transform(displacement, Matrix.CreateRotationZ(-Rotation));
            }

            Location += displacement;
        }
    }
}
