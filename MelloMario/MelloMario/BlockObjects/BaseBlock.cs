using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MelloMario.BlockObjects.States;

namespace MelloMario.BlockObjects
{
    public abstract class BaseBlock : IGameObject
    {
        protected ISprite Sprite;
        public IBlockState State;

        //Using Rectangle to record location and hitting boundary
        public Rectangle Boundary { get; set; }

        public BaseBlock(Vector2 location)
        {
            State = new Silent(this);
            Boundary = new Rectangle(location.ToPoint(), new Point(16, 16));
            // TODO: add sprites?
        }

        public void Update(GameTime gameTime)
        {
            Sprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, Boundary.Location.ToVector2());
        }
    }
}
