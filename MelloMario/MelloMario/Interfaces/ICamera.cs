using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MelloMario
{
    interface ICamera
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
