
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MelloMario.Commands;
using System.Diagnostics;

namespace MelloMario
{
    public abstract class Controller : IController
    {
        protected Dictionary<int, ICommand> commands;

        public Controller()
        {
            commands = new Dictionary<int, ICommand>();
        }

        public void AddCommand(int key, ICommand value)
        {
            commands.Add(key, value);
        }

        public abstract void UpdateInput();
    }

    class KeyboardController : Controller
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
                    if (commands.ContainsKey((int)key))
                        commands[(int)key].Execute();

            // Update previous Keyboard state.
            previousKeyboardState = currentState;
        }
    }

    class GamepadController : Controller
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

                    var buttonList = (Buttons[])Enum.GetValues(typeof(Buttons));

                    foreach (var button in buttonList)
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