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
        float lps = 30.0f;
        char[] delim = { ' ' };
        string[] parts;
        int part = 0;
        int length = 0;
        int index = 0;

        public TypingMessage(SpriteBatch sb, ContentManager contentManager, string message)
            : base(sb, contentManager, new Vector2(64,132), "", new Rectangle(0,0,0,0)) {
            parts = message.Split(delim);
            msg = "";
        }

        public override void Update(GameTime gameTime) {
            if (1000 / lps <= elapsed && part < parts.Length) {
                msg += Next();
                SoundBank.Instance.play("typing");
                elapsed = 0;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime) {
            spriteBatch.DrawString(ImageBank.Instance.font, msg, position, color);
        }

        public override void Celebrate() {
            cframe = 1;
            base.Celebrate();
        }

        private char Next() {
            char ret = '\n';
            if (index == 0 && parts[part].Length + length >= 24) {
                length = 0;
                ret = '\n';
            } else {
                if (index < parts[part].Length) {
                    ret = parts[part][index];
                    index++;
                } else {
                    index = 0;
                    part++;
                    ret = ' ';
                }
                length++;
            }
            return ret;
        }
    }
}
