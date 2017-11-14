using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using MelloMario.MarioObjects.MovementStates;
using MelloMario.Theming;
using MelloMario.Sounds;

namespace MelloMario.MarioObjects
{
    class PlayerMario : DollMario, IPlayer // TODO: split
    {
        protected IGameSession Session;

        public IGameWorld World
        {
            get
            {
                return base.World;
            }
        }

        public ICharacter Character
        {
            get
            {
                return this;
            }
        }

        public PlayerMario(IGameSession session, IGameWorld world, Point location, Listener listener) : base(world, location, listener)
        {
            Session = session;
            Session.Add(this);
        }

        public void Spawn(IGameWorld world, Point location)
        {
            World.Remove(this);
            base.World = world;
            World.Add(this);

            Session.Move(this);

            Relocate(location);
        }

        public void Reset()
        {
            RemoveSelf();
            Session.Remove(this);

            // note: Boundary.Location or Boundary.Center? sometimes confusing
            // note: listener is passed as null so score points will not do anything
            new PlayerMario(Session, World, World.GetRespawnPoint(Boundary.Location), null);
        }
    }
}
