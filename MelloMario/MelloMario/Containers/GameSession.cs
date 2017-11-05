using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MelloMario.Containers
{
    class GameSession : BaseContainer<IGameWorld, IPlayer>, IGameSession
    {
        private ISet<Point> respawnPoints;

        protected override IGameWorld GetKey(IPlayer value)
        {
            return null; //return value.World;//TODO
        }

        public GameSession()
        {
        }

        public Point GetRespawnPoint(Point location)
        {
            Point target = location;

            foreach (Point p in respawnPoints)
            {
                if (p.X <= location.X && p.X > target.X)
                {
                    target = p;
                }
            }

            return target;
        }
    }
}
