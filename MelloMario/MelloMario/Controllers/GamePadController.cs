using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MelloMario.Controllers
{
    class GamepadController : BaseController<Buttons>
    {
        GamePadState previousState;

        public GamepadController(GameModel model)
            : base(model)
        {
            previousState = GamePad.GetState(PlayerIndex.One);
        }

        public override void Update()
        {
            // Get the current gamepad state.
            GamePadState currentState = GamePad.GetState(PlayerIndex.One);

            // Process input only if connected.
            // Note: KeyBehavior.release should not be responsible for any "states", use KeyBehavior.hold instead
            if (currentState.IsConnected)
            {
                foreach (Buttons button in Enum.GetValues(typeof(Buttons)))
                {
                    if (currentState.IsButtonDown(button) &&
                        !previousState.IsButtonDown(button))
                    {
                        RunCommand(button, KeyBehavior.press);
                    }
                    else if (!currentState.IsButtonDown(button) &&
                        previousState.IsButtonDown(button))
                    {
                        RunCommand(button, KeyBehavior.release);
                    }
                    else if (currentState.IsButtonDown(button) &&
                        previousState.IsButtonDown(button))
                    {
                        RunCommand(button, KeyBehavior.hold);
                    }
                }
            }

            // Update previous gamepad state.
            previousState = currentState;
        }
    }
}