namespace MelloMario.Controls.Commands
{
    #region

    using Sounds;

    #endregion

    internal class Mute : ICommand
    {
        public void Execute()
        {
            SoundController.ToggleMute();
        }
    }
}
