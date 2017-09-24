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
    class BrickBlock : BaseBlock
    {
        private Boolean isUsed = false;

        public BrickBlock(Vector2 location, Boolean isUsed): base(location)
        {
            this.isUsed = isUsed;
            if (!isUsed)
            {
                state = new Silent(this);
            }
            else
            {
                state = new Used(this);
            }
        }
    }
}
