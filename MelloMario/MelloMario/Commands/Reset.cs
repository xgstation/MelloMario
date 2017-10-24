namespace MelloMario.Commands
{
    class Reset : BaseCommand<GameModel>
    {
        public Reset(GameModel model) : base(model)
        {
        }
        public override void Execute()
        {
            Receiver.Reset();
        }
    }
}