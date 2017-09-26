using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.EnemyObjects
{
    public abstract class BaseEnemy : IGameObject
    {
        protected Vector2 Location;

        public BaseEnemy(Vector2 initLocation)
        {
            Location = initLocation;
        }
        
        abstract public void Draw(SpriteBatch spriteBatch);

        abstract public void Update(GameTime gameTime);
    }
}
