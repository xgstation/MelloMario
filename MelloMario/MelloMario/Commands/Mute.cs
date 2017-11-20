using MelloMario.Sounds;

namespace MelloMario.Commands
{
    internal class Mute : ICommand
    {
        public void Execute()
        {
            SoundController.ToggleMute();
        }
    }
}
