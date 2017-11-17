using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MelloMario.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static System.Single;

namespace MelloMario.MiscObjects
{
    class GameCamera : IGameCamera
    {
        public GameCamera(Viewport viewport)
        {
            this.viewport = viewport;
            Origin = new Vector2(this.viewport.Width / 2.0f, this.viewport.Height / 2.0f);
            Zoom = 1.0f;
        }

        public Vector2 Location
        {
            get => location;
            set
            {
                location = value;

                if (Limit == null || !(Math.Abs(Zoom - 1.0f) < Epsilon) || !(Math.Abs(Rotation) < Epsilon)) return;
                location.X = MathHelper.Clamp(location.X, Limit.Value.X, Limit.Value.X + Limit.Value.Width - viewport.Width);
                location.Y = MathHelper.Clamp(location.Y, Limit.Value.Y, Limit.Value.Y + Limit.Value.Height - viewport.Height);
            }
        }

        public Vector2 Origin { get; set; }

        public float Zoom { get; set; }

        public float Rotation { get; set; }


        public Rectangle? Limit
        {
            get => limit;
            set
            {
                if (value != null)
                {
                    // Assign limit but make sure it's always bigger than the viewport
                    limit = new Rectangle
                    {
                        X = value.Value.X,
                        Y = value.Value.Y,
                        Width = System.Math.Max(viewport.Width, value.Value.Width),
                        Height = System.Math.Max(viewport.Height, value.Value.Height)
                    };

                    // Validate gameCamera newLocation with new limit
                    Location = Location;
                }
                else
                {
                    limit = null;
                }
            }
        }

        public Matrix GetViewMatrix(Vector2 parallax)
        {
            return Matrix.CreateTranslation(new Vector3(-Location * parallax, 0.0f)) *
                   Matrix.CreateTranslation(new Vector3(-Origin, 0.0f)) *
                   Matrix.CreateRotationZ(Rotation) *
                   Matrix.CreateScale(Zoom, Zoom, 1.0f) *
                   Matrix.CreateTranslation(new Vector3(Origin, 0.0f));
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

        private readonly Viewport viewport;
        private Vector2 location;
        private Rectangle? limit;
    }
}
