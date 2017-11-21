namespace MelloMario.Controls.Commands
{
    #region

    using MelloMario.Sounds;

    #endregion

    internal class Mute : ICommand
    {
        public void Execute()
        {
            SoundManager.ToggleMute();
        }
    }
}
