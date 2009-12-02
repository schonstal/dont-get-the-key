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
    //Formerly "TimeLeft"
    class Stats : Actor
    {
        float fps = 2;
        double remaining = 30000;
        Color color = Color.White;

        public Stats(SpriteBatch sb, ContentManager contentManager,
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
                if (remaining < 5000 && (color == Color.White))
                    color = Color.Red;
                else
                    color = Color.White;
                elapsed = 0;
            }
            if(GameState.Instance.Current.GetType().Name == "InGame")
                remaining -= gameTime.ElapsedGameTime.Milliseconds;
        }

        public override void Draw(GameTime gameTime) {
            spriteBatch.DrawString(ImageBank.Instance.font, 
                ((int)remaining/1000).ToString("00"), position, color);
            spriteBatch.DrawString(ImageBank.Instance.font, "01", new Vector2(144, 40), Color.White);
            spriteBatch.DrawString(ImageBank.Instance.font, 
                (GameState.Instance.Current.Collision("main", "key")?"01":"00"),
                new Vector2(144, 24), Color.White);
        }
    }
}
