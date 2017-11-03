using Microsoft.Xna.Framework;

namespace MelloMario
{
    interface IGameSession : IContainer<IPlayer>
    {
        Point GetRespawnPoint(Point location);
    }
}
