using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MelloMario
{
    internal interface ILevelGenerator
    {
        IEnumerable<IGameObject> Request(Rectangle range);
    }
}
