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
            // Previous input state
            prev = new Dictionary<string, bool>();

            // Map from string to actual keyboard key
            keyboard_map = new Dictionary<string, Keys>();
            keyboard_map["A"] = Keys.Z;
            keyboard_map["B"] = Keys.X;
            keyboard_map["X"] = Keys.C;
            keyboard_map["Y"] = Keys.V;
            keyboard_map["Back"] = Keys.Q;
            keyboard_map["BigButton"] = Keys.W;
            keyboard_map["LeftShoulder"] = Keys.E;
            keyboard_map["RightShoulder"] = Keys.R;
            keyboard_map["LeftStick"] = Keys.S;
            keyboard_map["RightStick"] = Keys.D;
            keyboard_map["Start"] = Keys.A;
            keyboard_map["Up"] = Keys.Up;
            keyboard_map["Down"] = Keys.Down;
            keyboard_map["Left"] = Keys.Left;
            keyboard_map["Right"] = Keys.Right;
           
            // Map from string to actual gamepad button
            gamepad_map = new Dictionary<string, Buttons>();
            gamepad_map["A"] = Buttons.A;
            gamepad_map["B"] = Buttons.B;
            gamepad_map["X"] = Buttons.X;
            gamepad_map["Y"] = Buttons.Y;
            gamepad_map["Back"] = Buttons.Back;
            gamepad_map["BigButton"] = Buttons.BigButton;
            gamepad_map["LeftShoulder"] = Buttons.LeftShoulder;
            gamepad_map["RightShoulder"] = Buttons.RightShoulder;
            gamepad_map["LeftStick"] = Buttons.LeftStick;
            gamepad_map["RightStick"] = Buttons.RightStick;
            gamepad_map["Start"] = Buttons.Start;
            gamepad_map["Up"] = Buttons.DPadUp;
            gamepad_map["Down"] = Buttons.DPadDown;
            gamepad_map["Left"] = Buttons.DPadLeft;
            gamepad_map["Right"] = Buttons.DPadRight;
        }

        private PlayerIndex player;
        private Dictionary<string, bool> prev;
        private Dictionary<string, Keys> keyboard_map;
        private Dictionary<string, Buttons> gamepad_map;

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
            if ((buttonMap(button, GamePad.GetState(player), Keyboard.GetState(player))) && prev != null && prev.ContainsKey(button) && (!prev[button]))
            {
                prev[button] = buttonMap(button, GamePad.GetState(player), Keyboard.GetState(player));
                return true;
            }
            prev[button] = buttonMap(button, GamePad.GetState(player), Keyboard.GetState(player));
            return false;
        }


        public bool held(string button) {
            if (buttonMap(button, GamePad.GetState(player), Keyboard.GetState(player)))
                return true;
            return false;
        }

        //I'll have some dynamic programming, please. No? :(
        private bool buttonMap(string button, GamePadState state, KeyboardState kbstate) {
            // Check for any key press
            if (button.Equals("Any"))
            {
                if (kbstate.GetPressedKeys().Length > 0)
                    return true;
                else
                {
                    foreach (KeyValuePair<String, Buttons> kvp in gamepad_map)
                    {
                        if (state.IsButtonDown(kvp.Value))
                            return true;
                    }
                }
            }
            else // Check the gamepad and keyboard
            {
                if (state.IsButtonDown(gamepad_map[button]) || kbstate.IsKeyDown(keyboard_map[button]))
                    return true;
            }
            return false;
        }
    }
}
