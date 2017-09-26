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
    public class Mario
    {
        public IMarioState State;
        Vector2 location;
      
        public Mario(Vector2 initLocation)
        {
            location = initLocation;
            State = new StandardIdleRight(this);
        }
        
        //make another method that returns the current state of the object
        public void down() { State.down(); }
        public void idle() { State.idle(); }
        public void up() { State.up(); }
        public void right() { State.right(); }
        public void left() { State.left(); }
        public void die() { State.die(); }
        public void changeToStandardState() { State.changeToStandardState();}
        public void changeToFireState() { State.changeToFireState();}
        public void changeToSuperState() { State.changeToSuperState();}
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
