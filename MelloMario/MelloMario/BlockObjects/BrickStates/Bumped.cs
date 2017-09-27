using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.BlockObjects.BrickStates
{
    class Bumped : Normal
    {
        private Vector2 origin;
        private float elapsed;
        private float speedY = -7f;
        private float accelerationY = 1.5f;
        private float offset;
        public Bumped(BrickBlock brickBlock, Vector2 location) : base (brickBlock, location)
        {
            origin = location;
        }
        public Bumped(BrickBlock brickBlock, Vector2 location, Boolean isVisible) : base (brickBlock, location, isVisible)
        {
            origin = location;
        }
        public void changeToNomral()
        {
            brickBlock.state = new Normal(brickBlock, boundary.Location.ToVector2());
        }
        public override void Update(GameTime gameTime)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            offset = 0.5f * accelerationY * (float)Math.Pow(elapsed,2.0f) + speedY * elapsed;
            boundary.Offset(0f, offset);
            if (boundary.Location.Y <= origin.Y)
            {
                changeToNomral();
            }
        }
    }
}
