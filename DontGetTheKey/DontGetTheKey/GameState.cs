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
    public class GameState
    {
        private static GameState instance;
        private Stack<State> states;

        private GameState() {
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

        public static State Current
        {
            get
            {
                return instance.states.Peek();
            }
        }

        public void Update() 
        { 
            states.Peek().Update();
        }

        public void Draw() 
        { 
            states.Peek().Draw(); 
        }

        public void Previous() 
        { 
            states.Pop(); 
        }

        public void New(State state) 
        { 
            states.Push(state); 
        }
    }
}
