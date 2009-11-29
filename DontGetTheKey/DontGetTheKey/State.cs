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
        protected Dictionary<string, Actor> actors;
        protected SpriteBatch spriteBatch;
        protected ContentManager content;

        public State(SpriteBatch sb, ContentManager contentManager) {
            spriteBatch = sb;
            content = contentManager;
            actors = new Dictionary<string,Actor>();
        }

        public virtual void Update(GameTime gameTime) {
            foreach(KeyValuePair<string,Actor> kvp in actors)
                kvp.Value.Update(gameTime);
        }

        public virtual void Draw(GameTime gameTime) {
            spriteBatch.Begin();
            foreach (KeyValuePair<string, Actor> kvp in actors)
                kvp.Value.Draw(gameTime);
            spriteBatch.End();
        }

        public bool Collision(Rectangle box, string actor) {
            return (actors.ContainsKey(actor) && actors[actor].HitBox.Intersects(box));
        }

        public Actor Register(string name, Actor a) {
            actors.Add(name, a);
            return a;
        }
    }
}
