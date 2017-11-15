using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using MelloMario.MarioObjects.MovementStates;
using MelloMario.MarioObjects.PowerUpStates;
using MelloMario.MarioObjects.ProtectionStates;
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
                return world;
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
            this.world = world;
            World.Add(this);

            Session.Move(this);

            Relocate(location);
        }

        public void Reset()
        {
            // note: Boundary.Location or Boundary.Center? sometimes confusing
            // new PlayerMario(Session, World, World.GetRespawnPoint(Boundary.Location), this.GetListener);
            // new PlayerMario(Session, World, World.GetInitialPoint(), this.GetListener);

            Relocate(World.GetInitialPoint() + new Point(32, 32));

            PowerUpState = new Standard(this);
            ProtectionState = new Protected(this);

            ResetA();

            World.Move(this);
            Session.Move(this);
        }
    }
}
