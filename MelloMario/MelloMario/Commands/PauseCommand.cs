namespace MelloMario.Commands
{
    class PauseCommand : BaseCommand<GameModel>
    {
        public PauseCommand(GameModel model): base(model)
        {
        }
        public override void Execute()
        {
            //do nothing for now
        }
    }
}