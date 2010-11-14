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
    class Door : Actor
    {
        float fps = 15.0f;

        Rectangle target;

        public Door(SpriteBatch sb, ContentManager contentManager,
            Vector2 pos, string texture, Rectangle box)
            : base(sb, contentManager, pos, texture, box) {
            target = new Rectangle(0, 0, 16, 32);
            this.priority = 1;
        }

        public override void Update(GameTime gameTime) {
            if (elapsed >= 1000 / fps) {
                target.X = 16; //sets every frame.  Oh well.
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime) {
            spriteBatch.Draw(ImageBank.Instance.texture(sprite), position, target, color);
        }
    }
}
