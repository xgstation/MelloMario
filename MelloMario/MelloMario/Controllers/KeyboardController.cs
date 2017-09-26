
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MelloMario.Commands;
using System.Diagnostics;

namespace MelloMario.Controllers
{
    public class KeyboardController : BaseController
    {
        Game game;
        KeyboardState previousKeyboardState;

        public KeyboardController(Game game)
            : base()
        {
            this.game = game;
            previousKeyboardState = Keyboard.GetState();
        }

        public override void UpdateInput()
        {
            // Get the current Keyboard state.
            KeyboardState currentState = Keyboard.GetState();

            Keys[] keysPressed = currentState.GetPressedKeys();
            foreach (Keys key in keysPressed)
                if (!previousKeyboardState.IsKeyDown(key))
                {
                    RunCommand((int)key);
                }

            // Update previous Keyboard state.
            previousKeyboardState = currentState;
        }
    }
}