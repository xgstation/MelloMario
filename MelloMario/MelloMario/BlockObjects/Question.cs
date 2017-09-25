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
    public class Question : BaseUseableBlock
    {
        public Question(Vector2 location, Boolean isUsed): base(location, isUsed)
        {
        }
    }
}
