namespace MelloMario.Controls.Commands
{
    using Sounds;

    internal class Mute : ICommand
    {
        public void Execute()
        {
            SoundController.ToggleMute();
        }
    }
}
