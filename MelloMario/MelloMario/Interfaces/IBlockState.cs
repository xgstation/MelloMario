using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario
{
    public interface IBlockState
    {
        void ChangeToBumped();
        void ChangeToSilent();
        void ChangeToDestroyed();
        void ChangeToHidden();
        void ChangeToUsed();
        void Draw(SpriteBatch spriteBatch, Vector2 location);
        void Update(GameTime gameTime);
    }
}
