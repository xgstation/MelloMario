namespace MelloMario.Commands
{
    class Resume : BaseCommand<IGameModel>
    {
        public Resume(IGameModel model) : base(model)
        {
        }

        public override void Execute()
        {
            Receiver.Resume();
        }
    }
}
