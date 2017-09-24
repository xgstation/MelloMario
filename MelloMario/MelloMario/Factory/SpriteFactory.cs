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
    class SpriteFactory : ISpriteFactory
        
    {
        Dictionary<String, Texture2D> stringToTexture;
        //player textures
        //dead
        private Texture2D Dead;
        //fire
        private Texture2D FireCrouchingLeft, FireFallingLeft, FireIdleLeft, FireJumpingLeft, FireWalkingLeft;
        private Texture2D FireCrouchingRight, FireFallingRight, FireIdleRight, FireJumpingRight, FireWalkingRight;
        //super
        private Texture2D SuperCrouchingLeft, SuperFallingLeft, SuperIdleLeft, SuperJumpingLeft,SuperWalkingLeft;
        private Texture2D SuperCrouchingRight, SuperFallingRight, SuperIdleRight, SuperJumpingRight, SuperWalkingRight;
        //standard
        private Texture2D StandardCrouchingLeft, StandardFallingLeft, StandardIdleLeft, StandardJumpingLeft, StandardWalkingLeft;
        private Texture2D StandardCrouchingRight, StandardFallingRight, StandardIdleRight, StandardJumpingRight, StandardWalkingRight;
        private static SpriteFactory instance = new SpriteFactory();
        public SpriteFactory()
        {
        }
        public static SpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }
        public void LoadAllTextures(ContentManager content)
        {
            //dead
            Dead = content.Load<Texture2D>("Dead");
            //fire
            FireCrouchingLeft = content.Load<Texture2D>("FireCrouchingLeft");
            FireFallingLeft = content.Load<Texture2D>("FireFallingLeft");
            FireIdleLeft = content.Load<Texture2D>("FireIdleLeft");
            FireWalkingLeft = content.Load<Texture2D>("FireWalkingLeft");
            FireJumpingLeft = content.Load<Texture2D>("FireJumpingLeft");
            FireCrouchingRight = content.Load<Texture2D>("FireCrouchingRight");
            FireFallingRight = content.Load<Texture2D>("FireFallingRight");
            FireIdleRight = content.Load<Texture2D>("FireIdleRight");
            FireWalkingRight = content.Load<Texture2D>("FireWalkingRight");
            FireJumpingRight = content.Load<Texture2D>("FireJumpingRight");
            //standard
            StandardCrouchingLeft = content.Load<Texture2D>("StandardCrouchingLeft");
            StandardFallingLeft = content.Load<Texture2D>("StandardFallingLeft");
            StandardIdleLeft = content.Load<Texture2D>("StandardIdleLeft");
            StandardWalkingLeft = content.Load<Texture2D>("StandardWalkingLeft");
            StandardJumpingLeft = content.Load<Texture2D>("StandardJumpingLeft");
            StandardCrouchingRight = content.Load<Texture2D>("StandardCrouchingRight");
            StandardFallingRight = content.Load<Texture2D>("StandardFallingRight");
            StandardIdleRight = content.Load<Texture2D>("StandardIdleRight");
            StandardWalkingRight = content.Load<Texture2D>("StandardWalkingRight");
            StandardJumpingRight = content.Load<Texture2D>("StandardJumpingRight");
            //super
            SuperCrouchingLeft = content.Load<Texture2D>("SuperCrouchingLeft");
            SuperFallingLeft = content.Load<Texture2D>("SuperFallingLeft");
            SuperIdleLeft = content.Load<Texture2D>("SuperIdleLeft");
            SuperWalkingLeft = content.Load<Texture2D>("SuperWalkingLeft");
            SuperJumpingLeft = content.Load<Texture2D>("SuperJumpingLeft");
            SuperCrouchingRight = content.Load<Texture2D>("SuperCrouchingRight");
            SuperFallingRight = content.Load<Texture2D>("SuperFallingRight");
            SuperIdleRight = content.Load<Texture2D>("SuperIdleRight");
            SuperWalkingRight = content.Load<Texture2D>("SuperWalkingRight");
            SuperJumpingRight = content.Load<Texture2D>("SuperJumpingRight");
            //dictionary
            stringToTexture = new Dictionary<String, Texture2D>
            {
                {"FireCrouchingLeft",FireCrouchingLeft},{"FireIdleLeft",FireIdleLeft}, {"FireJumpingLeft",FireJumpingLeft},
                {"FireWalkingLeft",FireWalkingLeft},{"FireFallingLeft",FireFallingLeft},{"FireCrouchingRight",FireCrouchingRight},
                {"FireIdleRight",FireIdleRight},{"FireJumpingRight",FireJumpingRight},{"FireWalkingRight",FireWalkingRight},
                {"FireFallingRight",FireFallingRight},{"SuperCrouchingLeft",SuperCrouchingLeft},{"SuperIdleLeft",SuperIdleLeft},
                {"SuperJumpingLeft",SuperJumpingLeft},{"SuperWalkingLeft",SuperWalkingLeft},{"SuperFallingLeft",SuperFallingLeft},
                {"SuperCrouchingRight",SuperCrouchingRight},{"SuperIdleRight",SuperIdleRight},{"SuperJumpingRight",SuperJumpingRight},
                {"SuperWalkingRight",SuperWalkingRight},{"SuperFallingRight",SuperFallingRight},{"StandardCrouchingLeft",StandardCrouchingLeft},
                {"StandardIdleLeft",StandardIdleLeft},{"StandardJumpingLeft",StandardJumpingLeft},{"StandardWalkingLeft",StandardWalkingLeft},
                {"StandardFallingLeft",StandardFallingLeft},{"StandardCrouchingRight",StandardCrouchingRight},{"StandardIdleRight",StandardIdleRight},
                {"StandardJumpingRight",StandardJumpingRight},{"StandardWalkingRight",StandardWalkingRight},{"StandardFallingRight",StandardFallingRight},
            };

        }
        
    
 
        public ISprite createSprite(string textureName, bool Static)
        {
            //change
            
            ISprite sprite;
            //static
            if (Static)
            {
               sprite = new StaticSprite(stringToTexture[textureName]);
            }
            //animated
            else
            {
                //add additional parameters when motion is involved
                sprite = new AnimatedSprite(stringToTexture[textureName]);
            }
            return sprite;
        }
    }
}