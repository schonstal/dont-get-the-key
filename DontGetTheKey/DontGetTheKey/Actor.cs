using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

using System.Text;

namespace DontGetTheKey
{
    public class Actor
    {
        protected String name;
        protected SpriteBatch spriteBatch;
        protected ContentManager content;
        protected Texture2D sprite;
        protected Vector2 position;
        protected Rectangle hitBox;

        public Actor(String actorName, SpriteBatch sb, ContentManager contentManager,
            Vector2 pos, Texture2D texture, Rectangle box) {
            name = actorName;
            spriteBatch = sb;
            content = contentManager;
            position = pos;
            sprite = texture;
            hitBox = box;
        }

        public virtual void Update(GameTime gameTime) {
        }

        public virtual void Draw(GameTime gameTime) {
            spriteBatch.Draw(sprite, position, Color.White);
        }

        public virtual void LoadContent() {
        }
    }
}
