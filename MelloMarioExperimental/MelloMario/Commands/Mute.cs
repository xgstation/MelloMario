namespace MelloMario.Commands
{
    class Mute : BaseCommand<IGameModel>
    {
        public Mute(IGameModel model) : base(model)
        {
        }

        public override void Execute()
        {
            Receiver.ToggleMute();
        }
    }
}
