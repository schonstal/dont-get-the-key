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
        protected List<Actor> actors;
        protected SpriteBatch spriteBatch;
        protected ContentManager content;

        public State(SpriteBatch sb, ContentManager contentManager) {
            spriteBatch = sb;
            content = contentManager;
            actors = new List<Actor>();
        }

        public void Update(GameTime gameTime) {
            actors.ForEach(a => a.Update(gameTime));
        }

        public void Draw(GameTime gameTime) {
            spriteBatch.Begin();
            actors.ForEach(a => a.Draw(gameTime));
            spriteBatch.End();
        }

        public Actor Register(Actor a) {
            actors.Add(a);
            return a;
        }
    }
}
