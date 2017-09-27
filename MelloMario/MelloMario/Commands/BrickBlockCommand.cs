using System.Collections.Generic;

namespace MelloMario.Commands
{
    internal class BrickBlockCommand : BaseCommand<List<IGameObject>>
    {
        public BrickBlockCommand(List<IGameObject> obj) : base(obj)
        {
        }

        public override void Execute()
        {
            // temporary code for demo in sprint 1
            MarioObjects.Mario mario = null;
            BlockObjects.BrickBlock brick = null;

            foreach (IGameObject obj in Receiver)
            {
                if (obj is MarioObjects.Mario)
                {
                    mario = (MarioObjects.Mario)obj;
                }
                if (obj is BlockObjects.BrickBlock)
                {
                    brick = (BlockObjects.BrickBlock)obj;
                }
            }

            brick.State.ChangeToBumped();
        }
    }
}