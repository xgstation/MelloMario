﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MelloMario
{
   class Mario
    {
        IMarioState standardIdleRight;
        public ContentManager content;
        IMarioState marioState;
        Vector2 location;
      
        public Mario(Vector2 newLocation,ContentManager newContent)
        {
            content = newContent;
            standardIdleRight = new StandardIdleRight(this, content);
            location = newLocation;
            marioState = standardIdleRight;
        }
        
        public void setMarioState(IMarioState newMarioState)
        {
            marioState = newMarioState;
        }
        //make another method that returns the current state of the object
        public void down() { marioState.down();}
        public void idle() { marioState.idle();}
        public void fall() { marioState.fall(); }
        public void up() { marioState.up(); }
        public void right() { marioState.right(); }
        public void left() { marioState.left(); }
        public void die() { marioState.die(); }
        public void changeToStandardState() { marioState.changeToStandardState();}
        public void changeToFireState() { marioState.changeToFireState();}
        public void changeToSuperState() { marioState.changeToSuperState();}
        public void Update(){ marioState.Update();}
        public void Draw(SpriteBatch spriteBatch)
        { marioState.Draw(spriteBatch,location);}
  
       

    }
}
