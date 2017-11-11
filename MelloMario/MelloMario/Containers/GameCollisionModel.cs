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

        private void UpdateCharacterCollision(ICharacter character)
        {
            PlayerMario mario = character.GetType().IsAssignableFrom(typeof(PlayerMario)) ? character as PlayerMario : null;
            if (mario == null)
            {
                return;
            }
            ;
            IEnumerable<IGameObject> nearByObjects = world.ScanNearby(mario.Viewport);
            foreach (IGameObject nearByObj in nearByObjects)
            {
                if (nearByObj.Boundary.Intersects(mario.Boundary))
                {
                    
                }
            }
        }

        private void UpdateEnemyCollision()
        {
            
        }

        private void UpdateBlockCollision()
        {
            
        }

        private void UpdateItemCollision()
        {
            
        }
    }
}
