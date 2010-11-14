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
using System.Text.RegularExpressions;

namespace DontGetTheKey
{
    class TypingMessage : Actor
    {
        string message;
        string msg;
        float lps = 30.0f;
        char[] delim = { ' ' };
        string[] parts;
        int part = 0;
        int length = 0;
        int index = 0;

        public bool Finished
        {
            get 
            {
                string msgNoNew = Regex.Replace(msg, @"\n", "");
                return message == msgNoNew;
            }
        }

        public TypingMessage(SpriteBatch sb, ContentManager contentManager, string message)
            : base(sb, contentManager, new Vector2(64, 132), "", new Rectangle(0, 0, 0, 0))
        {
            this.message = message;
            parts = message.Split(delim);
            msg = "";
        }

        public override void Update(GameTime gameTime) {
            if (1000 / lps <= elapsed && !Finished) {
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

        public void Finish()
        {
            while (!Finished)
                msg += Next();
        }

        private char Next() {
            char ret = '\n';
            if (index == 0 && parts[part].Length + length >= 24) {
                length = 0;
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
