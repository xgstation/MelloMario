using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.ItemObjects
{
    public abstract class BaseItem : IGameObject
    {
        public Vector2 Location { get; set; }

        public BaseItem(Vector2 initLocation)
        {
            Location = initLocation;
        }

        abstract public void Draw(SpriteBatch spriteBatch);

        abstract public void Update(GameTime gameTime);
    }
}
