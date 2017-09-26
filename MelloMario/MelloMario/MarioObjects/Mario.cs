using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using MelloMario.MarioObjects.States;

namespace MelloMario.MarioObjects
{
    public class Mario : IGameObject
    {
        public IMarioState State;
        Vector2 location;
      
        public Mario(Vector2 initLocation)
        {
            location = initLocation;
            State = new StandardIdleRight(this);
        }
        
        //make another method that returns the current state of the object
        public void Down() { State.Down(); }
        public void Idle() { State.Idle(); }
        public void Up() { State.Up(); }
        public void Right() { State.Right(); }
        public void Left() { State.Left(); }
        public void Die() { State.Die(); }
        public void ChangeToStandardState() { State.ChangeToStandardState();}
        public void ChangeToFireState() { State.ChangeToFireState();}
        public void ChangeToSuperState() { State.ChangeToSuperState();}
        public void Update(GameTime game)
        {
            // TODO: calculate the location
            State.Update(game);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            State.Draw(spriteBatch, location);
        }
    }
}
