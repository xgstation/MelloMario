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
    public abstract class BaseUseableBlock : BaseBlock
    {   
        private Boolean isUsed = false;

        public BaseUseableBlock(Vector2 location, Boolean isUsed): base(location)
        {
            this.isUsed = isUsed;
            if (isUsed)
            {
                State = new Used(this);
            }
        }
    }
}
