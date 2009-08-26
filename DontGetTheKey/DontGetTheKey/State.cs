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
    public class State
    {
        private List<Actor> actors;
        private SpriteBatch spriteBatch;
        private ContentManager content;

        public State(SpriteBatch sb, ContentManager c)
        {
            actors = new List<Actor>();
            spriteBatch = sb;
            content = c;
        }

        public void Update()
        {
            actors.ForEach(a => a.Update());
        }

        public void Draw()
        {
            actors.ForEach(a => a.Draw());
        }

        public void Register(string name)
        {
            Actor a = new Actor();
            a.Init(name, spriteBatch, content);
            actors.Add(a);
        }
    }
}
