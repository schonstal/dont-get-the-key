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
        protected SpriteBatch spriteBatch;
        protected ContentManager content;
        protected string sprite;
        protected Vector2 position;
        protected Rectangle hitBox;

        public Actor(SpriteBatch sb, ContentManager contentManager,
            Vector2 pos, string texture, Rectangle box) {
            spriteBatch = sb;
            content = contentManager;
            position = pos;
            sprite = texture;
            hitBox = box;
        }

        public virtual void Update(GameTime gameTime) {
        }

        public virtual void Draw(GameTime gameTime) {
            spriteBatch.Draw(ImageBank.Instance.texture(sprite), position, Color.White);
        }

        public virtual void LoadContent() {
        }

        public virtual void Move(Vector2 amount) {
            position += amount;
        }

        public virtual void Transpose(Vector2 location) {
            position = location;
        }
    }
}
