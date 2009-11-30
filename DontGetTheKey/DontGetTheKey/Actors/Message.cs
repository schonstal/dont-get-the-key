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
    class Message : Actor
    {
        string msg;

        public Message(SpriteBatch sb, ContentManager contentManager,
            Vector2 pos, string message)
            : base(sb, contentManager, pos, "", new Rectangle(0,0,0,0)) {
            msg = message;
        }

        public override void Update(GameTime gameTime) {
        }

        public override void Draw(GameTime gameTime) {
            spriteBatch.DrawString(ImageBank.Instance.font, msg,
                new Vector2(position.X + 1, position.Y + 1), Color.Black);
            spriteBatch.DrawString(ImageBank.Instance.font, msg, position, Color.White);
        }
    }
}
