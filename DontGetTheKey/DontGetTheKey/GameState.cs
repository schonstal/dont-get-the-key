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
        
        //So we can keep previous state data if necessary
        private Stack<State> states;

        //Use the same spritebatch and content manager for all states
        private SpriteBatch spriteBatch;
        private ContentManager content;

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

        public SpriteBatch Batch 
        {
            get
            {
                return spriteBatch;
            }
        }

        public ContentManager Content
        {
            get
            {
                return content;
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

        public void Init(SpriteBatch batch, ContentManager manager)
        {
            spriteBatch = batch;
            content = manager;
        }

        public void Update()
        { 
            states.Peek().Update();
        }

        public void Draw() 
        { 
            states.Peek().Draw(); 
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
