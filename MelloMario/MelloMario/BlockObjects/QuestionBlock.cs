﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MelloMario.BlockObjects.States;

namespace MelloMario.BlockObjects
{
    class QuestionBlock : BaseBlock
    {
        private Boolean isUsed;

        public QuestionBlock(Vector2 location, Boolean isUsed): base(location)
        {
            this.isUsed = isUsed;
            if (!isUsed)
            {
                this.state = new Silent(this);
            }
            else
            {
                this.state = new Used(this);
            }
        }
    }
}
