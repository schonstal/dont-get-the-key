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

        private GameState()
        {
            states = new Stack<State>();
        }
        
        public static GameState Instance
        {
            get
            {
                if (instance == null)
                    instance = new GameState();
                return instance;
            }
        }
        
        //Quick access to the current state (can be avoided?)
        public State Current
        {
            get
            {
                return states.Peek();
            }
        }

        public void Update(GameTime gameTime)
        { 
            //Handle pausing
            states.Peek().Update(gameTime);
        }

        public void Draw(GameTime gameTime) 
        { 
            states.Peek().Draw(gameTime); 
        }

        //Return to the previous state.
        public void Previous() 
        { 
            states.Pop(); 
        }

        //Enter a new state
        public void Enter(State state) 
        { 
            states.Push(state); 
        }
    }
}
