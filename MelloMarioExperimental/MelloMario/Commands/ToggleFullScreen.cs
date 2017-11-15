namespace MelloMario.Commands
{
    class ToggleFullScreen : BaseCommand<IGameModel>
    {
        public ToggleFullScreen(IGameModel receiver) : base(receiver)
        {
        }

        public override void Execute()
        {
            Receiver.ToggleFullScreen();
        }
    }
}
