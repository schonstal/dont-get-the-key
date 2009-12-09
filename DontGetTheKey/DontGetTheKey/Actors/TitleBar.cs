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
    class TitleBar : Actor
    {
        //Config
        float shineDelay = 5.0f;
        float fps = 15.0f;

        float animElapsed = 0;
        float shineElapsed = 0;
        int frame;
        int row;

        Rectangle target;

        public TitleBar(SpriteBatch sb, ContentManager contentManager,
            Vector2 pos, string texture, Rectangle box)
            : base(sb, contentManager, pos, texture, box) {
            target = new Rectangle(0, 0, 256, 128);
        }

        public override void Update(GameTime gameTime) {
            animElapsed += gameTime.ElapsedGameTime.Milliseconds;

            if (frame == 0 && row == 0 && shineElapsed < (1000 * shineDelay)) {
                shineElapsed += gameTime.ElapsedGameTime.Milliseconds;
            } else if (1000 / fps <= animElapsed) {
                frame = (frame + 1) % 7;
                if (frame == 0)
                    row = (row + 1) % 2;
                animElapsed = 0;
            }

            if (frame == 6 && row == 1)
                shineElapsed = 0;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime) {
            target.X = 256 * frame;
            target.Y = 128 * row;
            spriteBatch.Draw(ImageBank.Instance.texture(sprite), position, target, color);
        }
    }
}
