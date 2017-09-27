using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.BlockObjects.QuestionStates
{
    class Bumped : Normal
    {
        private Vector2 origin;
        private float elapsed;
        private float speedY = -7f;
        private float accelerationY = 1.5f;
        private float offset;
        public Bumped(QuestionBlock questionBlock, Vector2 location) : base(questionBlock, location)
        {
            origin = location;
        }
        public Bumped(QuestionBlock questionBlock, Vector2 location, Boolean isVisible) : base(questionBlock, location, isVisible)
        {
            origin = location;
        }
        public void changeToNomral()
        {
            questionBlock.state = new Normal(questionBlock, boundary.Location.ToVector2());
        }
        public override void Update(GameTime gameTime)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            offset = 0.5f * accelerationY * (float)Math.Pow(elapsed, 2.0f) + speedY * elapsed;
            boundary.Offset(0f, offset);
            if (boundary.Location.Y <= origin.Y)
            {
                changeToNomral();
            }
        }
    }
}
