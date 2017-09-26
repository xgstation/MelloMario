﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Collections.Generic;
using MelloMario.Controllers;
using MelloMario.Commands;
using MelloMario.MarioObjects;

namespace MelloMario
{
    public class GameScript
    {
        public GameScript()
        {
        }
            
        public void Bind(List<IController> controllers, GameModel model, Mario mario)
        {
            ICommandFactory factory = CommandFactory.Instance;

            foreach (IController controller in controllers)
            {
                if (controller is KeyboardController)
                {
                    controller.AddCommand((int)Keys.Escape, factory.CreateGameModelCommand("Pause", model));
                    controller.AddCommand((int)Keys.A, factory.CreateMarioCommand("Jump", mario));
                    controller.AddCommand((int)Keys.Down, factory.CreateMarioCommand("Crouch", mario));
                    controller.AddCommand((int)Keys.Left, factory.CreateMarioCommand("Left", mario));
                    controller.AddCommand((int)Keys.Right, factory.CreateMarioCommand("Right", mario));
                    controller.AddCommand((int)Keys.Space, factory.CreateMarioCommand("Action", mario));

                    //commands for changing block/mario state
                    controller.AddCommand((int)Keys.X, factory.CreateGameObjectCommand("UsedBlock", null)); // TODO
                    controller.AddCommand((int)Keys.OemQuestion, factory.CreateGameObjectCommand("QuestionBlock", null)); // TODO
                    controller.AddCommand((int)Keys.B, factory.CreateGameObjectCommand("BrickBlock", null)); // TODO
                    controller.AddCommand((int)Keys.H, factory.CreateGameObjectCommand("HiddenBlock", null)); // TODO

                    controller.AddCommand((int)Keys.Y, factory.CreateMarioCommand("StandardState", mario));
                    controller.AddCommand((int)Keys.U, factory.CreateMarioCommand("SuperState", mario));
                    controller.AddCommand((int)Keys.I, factory.CreateMarioCommand("FireState", mario));
                    controller.AddCommand((int)Keys.O, factory.CreateMarioCommand("DeadState", mario));
                }
                else
                if (controller is GamepadController)
                {
                    controller.AddCommand((int)Buttons.Start, factory.CreateGameModelCommand("Pause", model));
                    controller.AddCommand((int)Buttons.A, factory.CreateMarioCommand("Jump", mario));
                    controller.AddCommand((int)Buttons.DPadDown, factory.CreateMarioCommand("Crouch", mario));
                    controller.AddCommand((int)Buttons.DPadLeft, factory.CreateMarioCommand("Left", mario));
                    controller.AddCommand((int)Buttons.DPadRight, factory.CreateMarioCommand("Right", mario));
                    controller.AddCommand((int)Buttons.B, factory.CreateMarioCommand("Action", mario));
                }
            }
        }
    }
}