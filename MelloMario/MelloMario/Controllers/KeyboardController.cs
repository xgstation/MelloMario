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
            var currentState = Keyboard.GetState();

            foreach (var key in currentState.GetPressedKeys())
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
            foreach (var key in previousState.GetPressedKeys())
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