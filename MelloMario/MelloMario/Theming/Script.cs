using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Collections.Generic;

namespace MelloMario
{

    public class Script
    {

        public void Initialize(List<Controller> controllers)
        {
            foreach(Controller controller in controllers)
            {
                if (controller is KeyboardController)
                {
                    controller.AddCommand((int)Keys.Escape, new PauseCommand(this));
                    controller.AddCommand((int)Keys.A, new JumpCommand(this));
                    controller.AddCommand((int)Keys.Down, new CrouchCommand(this));
                    controller.AddCommand((int)Keys.Left, new LeftCommand(this));
                    controller.AddCommand((int)Keys.Right, new RightCommand(this));
                    controller.AddCommand((int)Keys.Space, new ActionCommand(this));
                }
                else
                if (controller is GamepadController)
                {
                    controller.AddCommand((int)Buttons.Start, new PauseCommand(this));
                    controller.AddCommand((int)Buttons.A, new JumpCommand(this));
                    controller.AddCommand((int)Buttons.DPadDown, new CrouchCommand(this));
                    controller.AddCommand((int)Buttons.DPadLeft, new LeftCommand(this));
                    controller.AddCommand((int)Buttons.DPadRight, new RightCommand(this));
                    controller.AddCommand((int)Buttons.B, new ActionCommand(this));
                }
            }
        }

    }
}
