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
    //Disgusting
    public class Key : Actor
    {
        float fps = 2;
        int frame = 0;

        public Key(SpriteBatch sb, ContentManager contentManager,
            Vector2 pos, string texture, Rectangle box)
            : base(sb, contentManager, pos, texture, box) {
            return;
        }

        public override void Update(GameTime gameTime) {
            if (!GameState.Instance.Current.Collision(HitBox, "main")) {
                Animate(gameTime);
            }
        }

        public override void Draw(GameTime gameTime) {
            spriteBatch.Draw(ImageBank.Instance.texture(sprite), position, Color.White);          
        }

        public void Animate(GameTime gameTime) {
            base.Update(gameTime);
            if (1000 / fps <= elapsed) {
                if (frame <= 1)
                    position.Y++;
                else if (frame <= 3)
                    position.Y--;

                frame = (frame + 1) % 4;
                elapsed = 0;
            }
        }
    }
}
