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
        private double frametime = (1000/60);

        protected SpriteBatch spriteBatch;
        protected ContentManager content;
        protected string sprite;
        protected Vector2 position;
        protected Rectangle hitBox;

        protected Vector2 destination;
        protected int frames;
        //Difference in location for one frame
        protected Vector2 dv;

        protected double elapsed;

        public Actor(SpriteBatch sb, ContentManager contentManager,
            Vector2 pos, string texture, Rectangle box) {
            spriteBatch = sb;
            content = contentManager;
            position = pos;
            sprite = texture;
            hitBox = box;
        }

        //only really need to call this if you're moving the sprite.
        public virtual void Update(GameTime gameTime) {
            elapsed += gameTime.ElapsedGameTime.Milliseconds;
            if (frames > 0 && elapsed >= frametime) {
                position += dv;
                elapsed = 0;
                frames--;
            }
        }

        public virtual void Draw(GameTime gameTime) {
            spriteBatch.Draw(ImageBank.Instance.texture(sprite), position, Color.White);
        }

        public virtual void LoadContent() {
        }

        public virtual void Move(Vector2 amount) {
            position += amount;
        }

        public virtual void Transpose(Vector2 destination) {
            position = destination;
        }

        public virtual void Tween(Vector2 destination, double duration) {
            frames = (int)Math.Floor(60*duration);
            dv = (destination - position)/frames;
        }
    }
}
