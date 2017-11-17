using Microsoft.Xna.Framework;
using MelloMario.Theming;

namespace MelloMario.MarioObjects
{
    class Player : IPlayer
    {
        private IGameSession session;
        private ICharacter character;

        public IGameSession Session
        {
            get
            {
                return session;
            }
        }

        public IGameWorld World
        {
            get
            {
                return character.CurrentWorld;
            }
        }

        public ICharacter Character
        {
            get
            {
                return character;
            }
        }

        public Player(IGameSession session, ICharacter character)
        {
            this.session = session;
            this.session.Add(this);
            this.character = character;
        }

        public void Spawn(IGameWorld newWorld, Point newLocation)
        {
            character.Move(newWorld, newLocation);
            Session.Move(this);
        }

        public void Reset(ICharacter newCharacter)
        {
            character.Remove();
            character = newCharacter;
        }
    }
}
