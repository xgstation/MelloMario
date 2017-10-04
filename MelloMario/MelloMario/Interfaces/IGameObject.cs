using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario
{
    public interface IGameObject
    {
        Rectangle Boundary { get; }

        void Update(GameTime time, IList<IGameObject> collidable);
        void Draw(SpriteBatch spriteBatch);

        void OnSimulation(GameTime time);
        void OnCollision(IGameObject target);
    }
}
