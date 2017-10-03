using System.Collections.Generic;

namespace MelloMario.Commands
{
    internal class BrickBlockCommand : BaseCommand<IGameObject[,]>
    {
        public BrickBlockCommand(IGameObject[,] obj) : base(obj)
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
                if (obj is BlockObjects.BrickBlock && ((BlockObjects.BrickBlock)obj).State is BlockObjects.BrickStates.BrickSilent)
                {
                    brick = (BlockObjects.BrickBlock)obj;
                }
            }
            if (brick != null)
            if (mario.currentPowerState == MarioObjects.Mario.PowerState.Standard)
                brick.State.ChangeToBumped();
            else
                brick.State.ChangeToDestroyed();
        }
    }
}