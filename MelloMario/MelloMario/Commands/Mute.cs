namespace MelloMario.Commands
{
    internal class Mute : BaseCommand<IGameModel>
    {
        public Mute(IGameModel model) : base(model) { }

        public override void Execute()
        {
            Receiver.ToggleMute();
        }
    }
}