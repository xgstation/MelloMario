
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MelloMario.Commands;
using System.Diagnostics;

namespace MelloMario.Controllers
{
    class GamepadController : BaseController
    {
        Game game;
        GamePadState previousGamePadState,
                     emptyInput;

        public GamepadController(Game game)
            : base()
        {
            this.game = game;
            previousGamePadState = GamePad.GetState(PlayerIndex.One);
            emptyInput = new GamePadState(Vector2.Zero, Vector2.Zero, 0, 0, new Buttons());
        }

        public override void UpdateInput()
        {
            // Get the current gamepad state.
            GamePadState currentState = GamePad.GetState(PlayerIndex.One);

            // Process input only if connected.
            if (currentState.IsConnected)
            {
                if (currentState != emptyInput) // Button Pressed
                {

                    Buttons[] buttonList = (Buttons[])Enum.GetValues(typeof(Buttons));

                    foreach (Buttons button in buttonList)
                    {
                        if (currentState.IsButtonDown(button) &&
                            !previousGamePadState.IsButtonDown(button))
                            if (commands.ContainsKey((int)button))
                                commands[(int)button].Execute();
                    }
                }

                // Update previous gamepad state.
                previousGamePadState = currentState;
            }
        }
    }
}