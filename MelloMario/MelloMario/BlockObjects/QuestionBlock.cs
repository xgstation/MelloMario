using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MelloMario.BlockObjects.QuestionStates;

namespace MelloMario.BlockObjects
{
    public class QuestionBlock : BaseBlock
    {
        public IBlockState State;
        public List<IGameObject> objects;
        public QuestionBlock(Vector2 location) : base(location)
        {
            State = new QuestionSilent(this);
            objects = null;
        }
        public QuestionBlock(Vector2 location, List<IGameObject> objects) : this (location)
        {
            this.objects = objects;
        } 

        public override void Draw(SpriteBatch spriteBatch)
        {
            State.Draw(spriteBatch, location);
        }
        
        public override void Update(GameTime gameTime)
        {
            State.Update(gameTime);
        }
    }
}
