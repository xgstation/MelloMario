namespace MelloMario.Commands
{
    class Quit : BaseCommand<IGameModel>
    {
        public Quit(IGameModel model) : base(model)
        {
        }

        public override void Execute()
        {
            Receiver.Quit();
        }
    }
}
