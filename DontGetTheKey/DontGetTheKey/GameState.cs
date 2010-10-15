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
    public sealed class GameState
    {
        private static GameState instance;
        //For Deterministic Finite Autobots
        private Stack<State> states;
        private bool terminate = false;
        private bool easy = true;

        private GameState() {
            states = new Stack<State>();
        }

        public static GameState Instance {
            get {
                if (instance == null)
                    instance = new GameState();
                return instance;
            }
        }

        //Quick access to the current state (can be avoided?)
        public State Current {
            get { return states.Peek(); }
        }

        public bool Terminate {
            get { return terminate; }
        }
        
        public bool Easy
        {
            get { return this.easy; }
            set { this.easy = value; }
        }

        public void Update(GameTime gameTime) {
            //Handle pausing
            states.Peek().Update(gameTime);
        }

        public void Draw(GameTime gameTime, Matrix spriteScale) {
            states.Peek().Draw(gameTime, spriteScale);
        }

        //Return to the previous state.
        public void Previous() {
            states.Pop();
        }

        //Enter a new state
        public void Enter(State state) {
            states.Push(state);
        }

        public void Restart(State state) {
            //could just do new, but scared of relying on GC
            while (states.Count != 0) {
                states.Pop();
            }
            states.Push(state);
        }

        public void Exit() {
            terminate = true;
        }
    }
}
