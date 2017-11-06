using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelloMario.MarioObjects;

namespace MelloMario.Containers
{
    //Work in progress
    class GameCollisionModel
    {
        public enum CollisionResponse { Attack, Bounce, Obstacle, Squeeze}
        private ISet<ICharacter> characterSet;
        private GameWorld world;

        void UpdateCharacterCollision(ICharacter character)
        {
            var mario = character.GetType().IsAssignableFrom(typeof(PlayerMario)) ? character as PlayerMario : null;
            if (mario == null) return;;
            var nearByObjects = world.ScanNearby(mario.Viewport);
            foreach (var nearByObj in nearByObjects)
            {
                if (nearByObj.Boundary.Intersects(mario.Boundary))
                {
                    
                }
            }
        }

        void UpdateEnemyCollision()
        {
            
        }

        void UpdateBlockCollision()
        {
            
        }

        void UpdateItemCollision()
        {
            
        }
    }
}
