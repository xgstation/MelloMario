using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MelloMario.Controllers
{
    internal class GamepadController : BaseController<Buttons>
    {
        private GamePadState previousState;

        public GamepadController()
        {
            previousState = GamePad.GetState(PlayerIndex.One);
        }

        protected override void OnUpdate()
        {
            // Get the current gamepad state.
            var currentState = GamePad.GetState(PlayerIndex.One);

            // Process input only if connected.
            // Note: KeyBehavior.release should not be responsible for any "states", use KeyBehavior.hold instead
            if (currentState.IsConnected)
            {
                foreach (Buttons button in Enum.GetValues(typeof(Buttons)))
                {
                    if (currentState.IsButtonDown(button) && !previousState.IsButtonDown(button))
                    {
                        RunCommand(button, KeyBehavior.press);
                    }
                    else if (!currentState.IsButtonDown(button) && previousState.IsButtonDown(button))
                    {
                        RunCommand(button, KeyBehavior.release);
                    }
                    else if (currentState.IsButtonDown(button) && previousState.IsButtonDown(button))
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