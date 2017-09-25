using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Collections.Generic;
using MelloMario.Controllers;
using MelloMario.Commands;

namespace MelloMario
{
    public class Script
    {
        public void Initialize(GameModel model)
        {
            foreach(IController controller in model.controllers)
            {
                if (controller is KeyboardController)
                {
                    controller.AddCommand((int)Keys.Escape, new PauseCommand(model));
                    controller.AddCommand((int)Keys.A, new JumpCommand(model));
                    controller.AddCommand((int)Keys.Down, new CrouchCommand(model));
                    controller.AddCommand((int)Keys.Left, new LeftCommand(model));
                    controller.AddCommand((int)Keys.Right, new RightCommand(model));
                    controller.AddCommand((int)Keys.Space, new ActionCommand(model));

                    //commands for changing block/mario state
                    controller.AddCommand((int)Keys.X, new UsedBlockCommand(model));
                    controller.AddCommand((int)Keys.OemQuestion, new QuestionBlockCommand(model));
                    controller.AddCommand((int)Keys.B, new BrickBlockCommand(model));
                    controller.AddCommand((int)Keys.H, new HiddenBlockCommand(model));

                    controller.AddCommand((int)Keys.Y, new StdStateCommand(model));
                    controller.AddCommand((int)Keys.U, new SuperStateCommand(model));
                    controller.AddCommand((int)Keys.I, new FireStateCommand(model));
                    controller.AddCommand((int)Keys.O, new DeadStateCommand(model));
                }
                else
                if (controller is GamepadController)
                {
                    controller.AddCommand((int)Buttons.Start, new PauseCommand(model));
                    controller.AddCommand((int)Buttons.A, new JumpCommand(model));
                    controller.AddCommand((int)Buttons.DPadDown, new CrouchCommand(model));
                    controller.AddCommand((int)Buttons.DPadLeft, new LeftCommand(model));
                    controller.AddCommand((int)Buttons.DPadRight, new RightCommand(model));
                    controller.AddCommand((int)Buttons.B, new ActionCommand(model));
                }
            }
        }
    }
}
