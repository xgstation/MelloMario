using MelloMario.ItemObjects.SuperMushroomStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.ItemObjects
{
    public class SuperMushroom : BaseItem
    {
        public IItemState State;

        public SuperMushroom(Vector2 initLocation) : base(initLocation)
        {
            State = new SuperMushroomNormalState(this);
        }

        public void TransNormal()
        {
            State.transNormal();
        }

        public void TransDefeated()
        {
            State.transDefeated();
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
