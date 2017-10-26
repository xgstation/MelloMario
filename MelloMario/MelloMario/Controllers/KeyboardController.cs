using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MelloMario.Controllers
{
    class KeyboardController : BaseController<Keys>
    {
        KeyboardState previousState;

        public KeyboardController(GameModel model)
            : base(model)
        {
            previousState = Keyboard.GetState();
        }

        public override void Update()
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