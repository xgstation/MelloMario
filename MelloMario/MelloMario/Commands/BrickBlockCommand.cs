using System.Collections.Generic;

namespace MelloMario.Commands
{
    class BrickBlockCommand : BaseCommand<IGameObject[,]>
    {
        public BrickBlockCommand(IGameObject[,] obj) : base(obj)
        {
        }

        public override void Execute()
        {
            // temporary code for demo in sprint 1
            MarioObjects.Mario mario = null;
            BlockObjects.Brick brick = null;

            foreach (IGameObject obj in Receiver)
            {
                if (obj is MarioObjects.Mario)
                {
                    mario = (MarioObjects.Mario)obj;
                }
                if (obj is BlockObjects.Brick && ((BlockObjects.Brick)obj).State is BlockObjects.BrickStates.BrickNormal)
                {
                    brick = (BlockObjects.Brick)obj;
                }
            }
            if (brick != null)
            {
                /* if (mario.State is MarioObjects.States.StandardIdleLeft ||
                    mario.State is MarioObjects.States.StandardIdleRight ||
                    mario.State is MarioObjects.States.StandardJumpingLeft ||
                    mario.State is MarioObjects.States.StandardJumpingRight ||
                    mario.State is MarioObjects.States.StandardWalkingLeft ||
                    mario.State is MarioObjects.States.StandardWalkingRight ||
                    mario.State is MarioObjects.States.Dead)
                {
                    brick.State.Bump();
                }
                else*/
                {
                    brick.State.Destroy();
                }
            }
        }
    }
}