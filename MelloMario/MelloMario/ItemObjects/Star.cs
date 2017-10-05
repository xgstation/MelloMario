using MelloMario.ItemObjects.StarStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.ItemObjects
{
    class Star : BaseItem
    {
        public IItemState State;

        public Star(Vector2 initLocation) : base(initLocation)
        {
            State = new StarNormal(this);
        }

        public void TransNormal()
        {
            State.Show();
        }
        public void TransDefeated()
        {
            State.Collect();
        }

        public override void Update(GameTime gameTime)
        {
            State.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            State.Draw(spriteBatch, Location);
        }
    }
}
