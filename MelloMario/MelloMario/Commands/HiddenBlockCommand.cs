namespace MelloMario.Commands
{
    class HiddenBlockCommand : BaseCommand<IGameObject[,]>
    {
        public HiddenBlockCommand(IGameObject[,] obj) : base(obj)
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
                if (obj is BlockObjects.BrickBlock && ((BlockObjects.BrickBlock)obj).State is BlockObjects.BrickStates.BrickHidden)
                {
                    brick = (BlockObjects.BrickBlock)obj;
                }
            }
            if (brick != null)
            {
                brick.State.ChangeToSilent();
            }
        }
    }
}