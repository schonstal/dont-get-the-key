﻿using System;
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

        public Stats(SpriteBatch sb, ContentManager contentManager,
            Vector2 pos, string texture, Rectangle box)
            : base(sb, contentManager, pos, texture, box) {
                if (GameState.Instance.Easy)
                    remaining = 30000;
                else
                    remaining = 90000;
        }

        public float Rate {
            get { return fps; }
            set { fps = value; }
        }

        public double Remaining {
            get { return remaining; }
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

            if (GameState.Instance.Current.GetType().Name == "InGame") {
                remaining -= gameTime.ElapsedGameTime.Milliseconds;

                if (1000 / fps <= elapsed) {
                    if (remaining < 5000 && (color == Color.White) || remaining < 100)
                        color = Color.Red;
                    else
                        color = Color.White;
                    elapsed = 0;
                    fps = (remaining>0?(float)(10000/remaining):1);
                }
            } else {
                if(!celebrate)
                    color = Color.White;
            }
        }

        public override void Draw(GameTime gameTime) {
            spriteBatch.DrawString(ImageBank.Instance.font, 
                (Math.Ceiling(remaining/1000)).ToString("00"), position, color);
            spriteBatch.DrawString(ImageBank.Instance.font, "01", 
                new Vector2(position.X - 112, position.Y), (celebrate?color:Color.White));
            spriteBatch.DrawString(ImageBank.Instance.font, 
                (GameState.Instance.Current.Collision("main", "key")?"01":"00"),
                new Vector2(position.X - 112, position.Y - 16), (celebrate?color:Color.White));
        }
    }
}
