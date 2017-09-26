using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.BlockObjects
{
    public abstract class BaseBlock : IGameObject
    {
        public IBlockState state { get; set; }
        //Using Rectangle to record hitting boundary
        public Rectangle boundary { get; set; }
        public void SetBoundaryBasedOnState()
        {
            boundary = state.GetBoundary();
        }
        public void Update(GameTime gameTime)
        {
            state.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            state.Draw(spriteBatch, boundary.Location.ToVector2());
        }
    }
}
