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
    class PushStart : Actor
    {
        bool drawText;
        float fps = 2;
        public PushStart(SpriteBatch sb, ContentManager contentManager,
            Vector2 pos, string texture, Rectangle box)
            : base(sb, contentManager, pos, texture, box) {
        }

        public float Rate {
            get { return fps; }
            set { fps = value; }
        }

        public override void Update(GameTime gameTime) {
            elapsed += gameTime.ElapsedGameTime.Milliseconds;
            if (1000 / fps <= elapsed) {
                drawText = !drawText;
                elapsed = 0;
            }
        }

        public override void Draw(GameTime gameTime) {
            if (drawText)
                spriteBatch.DrawString(ImageBank.Instance.font,
                    "PUSH START BUTTON",
                    position,
                    Color.White);
        }
    }
}
