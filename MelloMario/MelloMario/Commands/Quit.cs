namespace MelloMario.Commands
{
    class Quit : BaseCommand<GameModel>
    {
        public Quit(GameModel model) : base(model)
        {
        }
        public override void Execute()
        {
            Receiver.Quit();
        }
    }
}