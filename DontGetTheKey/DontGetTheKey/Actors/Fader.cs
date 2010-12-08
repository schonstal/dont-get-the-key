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
    class Fader : Actor
    {
        //Rate for fade-in/out
        int rate;
        //Amount to change the alpha per tick
        float amount;

        float alpha = 1.0f;

        Boolean fadeIn = true;

        public Fader(SpriteBatch sb, ContentManager contentManager, int rate, float amount)
            : base(sb, contentManager, new Vector2(0,0), "black", new Rectangle(0,0,0,0)) {
                this.rate = rate;
                this.amount = amount;
        }

        public Boolean FadeIn {
            get { return fadeIn; }
            set { fadeIn = value; }
        }

        public Boolean Finished
        {
            get { return (fadeIn && alpha <= 0) || (!fadeIn && alpha >= 1.0f); }
        }

        public override void Update(GameTime gameTime)
        {
            elapsed += gameTime.ElapsedGameTime.Milliseconds;
            if (elapsed > rate)
            {
                if (fadeIn && alpha > 0)
                    alpha -= amount;
                else if (!fadeIn && alpha < 1.0f)
                    alpha += amount;
                elapsed = 0;
            }

            color = new Color(255, 255, 255, alpha);
        }

    }
}
