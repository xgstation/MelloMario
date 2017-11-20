namespace MelloMario.Controls.Commands
{
    internal class Quit : BaseCommand<IModel>
    {
        public Quit(IModel model) : base(model) { }

        public override void Execute()
        {
            Receiver.Quit();
        }
    }
}
