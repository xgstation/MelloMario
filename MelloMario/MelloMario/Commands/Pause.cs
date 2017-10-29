namespace MelloMario.Commands
{
    class Pause : BaseCommand<IGameModel>
    {
        public Pause(IGameModel model) : base(model)
        {
        }
        public override void Execute()
        {
            Receiver.Pause();
        }
    }
}