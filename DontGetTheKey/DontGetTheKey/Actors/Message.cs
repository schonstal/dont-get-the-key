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

        public Message(SpriteBatch sb, ContentManager contentManager, string message)
            : base(sb, contentManager, new Vector2(0,0), "", new Rectangle(0,0,0,0)) {
                priority = 17;
                msg = message;
                position.X = 160 - msg.Length * 4;
                position.Y = 100;
        }

        public Message(SpriteBatch sb, ContentManager contentManager, string message, Color color)
            : base(sb, contentManager, new Vector2(0, 0), "", new Rectangle(0, 0, 0, 0))
        {
            priority = 1;
            msg = message;
            position.X = 160 - msg.Length * 4;
            position.Y = 100;
            this.color = color;
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime) {
            spriteBatch.DrawString(ImageBank.Instance.font, msg,
                new Vector2(position.X + 1, position.Y + 1), Color.Black);
            spriteBatch.DrawString(ImageBank.Instance.font, msg, position, color);
        }

        public override void Celebrate() {
            cframe = 1;
            base.Celebrate();
        }
    }
}
