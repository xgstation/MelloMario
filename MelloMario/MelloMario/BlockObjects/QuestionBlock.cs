using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.BlockObjects
{
    public class QuestionBlock : BaseBlock
    {
        public QuestionBlock(Vector2 location)
        {
            state = new QuestionStates.Normal(this, location);
            SetBoundaryBasedOnState();
        }
        public QuestionBlock(Vector2 location, Boolean isHidden) : this(location)
        {
            state = new QuestionStates.Normal(this, location, isHidden);
        }
    }
}
