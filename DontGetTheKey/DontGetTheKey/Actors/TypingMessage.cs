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
    class TypingMessage : Actor
    {
        string msg;
        float lps = 15.0f;

        public TypingMessage(SpriteBatch sb, ContentManager contentManager, string message)
            : base(sb, contentManager, new Vector2(0,0), "", new Rectangle(0,0,0,0)) {
            msg = message;
            position.X = 48;
            position.Y = 140;
        }

        public override void Update(GameTime gameTime) {
        }

        public override void Draw(GameTime gameTime) {
            spriteBatch.DrawString(ImageBank.Instance.font, msg, position, color);
        }

        public override void Celebrate() {
            cframe = 1;
            base.Celebrate();
        }
    }
}
