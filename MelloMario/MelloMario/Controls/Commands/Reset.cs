namespace MelloMario.Controls.Commands
{
    internal class Reset : BaseCommand<IGameModel>
    {
        public Reset(IGameModel model) : base(model) { }

        public override void Execute()
        {
            Receiver.Reset();
        }
    }
}
