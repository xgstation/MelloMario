using Microsoft.Xna.Framework.Input;

namespace MelloMario.Controllers
{
    internal class KeyboardController : BaseController<Keys>
    {
        private KeyboardState previousState;

        public KeyboardController()
        {
            previousState = Keyboard.GetState();
        }

        protected override void OnUpdate()
        {
            // Get the current Keyboard state.
            KeyboardState currentState = Keyboard.GetState();

            foreach (Keys key in currentState.GetPressedKeys())
            {
                if (!previousState.IsKeyDown(key))
                {
                    RunCommand(key, KeyBehavior.press);
                }
                else
                {
                    RunCommand(key, KeyBehavior.hold);
                }
            }
            foreach (Keys key in previousState.GetPressedKeys())
            {
                if (!currentState.IsKeyDown(key))
                {
                    RunCommand(key, KeyBehavior.release);
                }
            }

            // Update previous Keyboard state.
            previousState = currentState;
        }
    }
}