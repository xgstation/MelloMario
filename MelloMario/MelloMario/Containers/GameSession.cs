using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MelloMario.Containers
{
    class GameSession : BaseContainer<IGameWorld, IPlayer>
    {
        protected override IGameWorld GetKey(IPlayer value)
        {
            return value.CurrentWorld;
        }
    }
}
