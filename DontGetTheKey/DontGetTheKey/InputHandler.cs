using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace DontGetTheKey
{
    //Singleton
    class InputHandler
    {
        private static InputHandler instance;
        public static InputHandler Instance {
            get {
                if (instance == null)
                    instance = new InputHandler();
                return instance;
            }
        }
        private InputHandler() {
        }

        private PlayerIndex player;
        private GamePadState prev;

        public Vector2 RightStick {
            get { return GamePad.GetState(player).ThumbSticks.Right; }
        }

        public Vector2 LeftStick {
            get { return GamePad.GetState(player).ThumbSticks.Left; }
        }

        public PlayerIndex Player {
            set { player = value; }
            get { return player; }
        }

        //We needn't worry about multibutton
        public bool pressed(string button) {
            if ((buttonMap(button, GamePad.GetState(player)) == ButtonState.Pressed) && (buttonMap(button, prev) == ButtonState.Released)) {
                prev = GamePad.GetState(player);
                return true;
            }
            prev = GamePad.GetState(player);
            return false;
        }


        public bool held(string button) {
            if (buttonMap(button, GamePad.GetState(player)) == ButtonState.Pressed)
                return true;
            return false;
        }

        //I'll have some dynamic programming, please. No? :(
        private ButtonState buttonMap(string button, GamePadState state) {
            switch (button) {
                case "A":
                    return state.Buttons.A;
                case "B":
                    return state.Buttons.B;
                case "X":
                    return state.Buttons.X;
                case "Y":
                    return state.Buttons.Y;
                case "Back":
                    return state.Buttons.Back;
                case "BigButton":
                    return state.Buttons.BigButton;
                case "LeftShoulder":
                    return state.Buttons.LeftShoulder;
                case "RightShoulder":
                    return state.Buttons.RightShoulder;
                case "LeftStick":
                    return state.Buttons.LeftStick;
                case "RightStick":
                    return state.Buttons.RightStick;
                case "Start":
                    return state.Buttons.Start;
                case "Up":
                    return state.DPad.Up;
                case "Down":
                    return state.DPad.Down;
                case "Left":
                    return state.DPad.Left;
                case "Right":
                    return state.DPad.Right;
                default:
                    return ButtonState.Released;
            }
        }
    }
}
