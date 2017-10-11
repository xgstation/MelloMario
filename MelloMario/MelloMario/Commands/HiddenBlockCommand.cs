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
            BlockObjects.Brick brick = null;

            foreach (IGameObject obj in Receiver)
            {
                if (obj is MarioObjects.Mario)
                {
                    mario = (MarioObjects.Mario)obj;
                }
                if (obj is BlockObjects.Brick && ((BlockObjects.Brick)obj).State is BlockObjects.BrickStates.Hidden)
                {
                    brick = (BlockObjects.Brick)obj;
                }
            }
            if (brick != null)
            {
                brick.State.Show();
            }
        }
    }
}