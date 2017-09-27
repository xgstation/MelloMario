using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.BlockObjects
{
    public class BrickBlock : BaseBlock
    {
        public BrickBlock(Vector2 location)
        {
            state = new BrickStates.Normal(this, location);
            SetBoundaryBasedOnState();
        }
        public BrickBlock(Vector2 location, Boolean isHidden) : this(location)
        {
            state = new BrickStates.Normal(this, location, isHidden);
        }
    }
}
