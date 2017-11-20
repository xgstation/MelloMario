using Microsoft.Xna.Framework;

namespace MelloMario
{
    internal interface ICamera
    {
        Vector2 Location { get; set; }

        Vector2 Origin { get; set; }

        float Zoom { get; set; }

        float Rotation { get; set; }

        Rectangle? Limit { get; set; }

        Matrix GetViewMatrix(Vector2 parallax);

        void LookAt(Vector2 newLocation);

        void Move(Vector2 displacement, bool respectRotation = false);
    }
}
