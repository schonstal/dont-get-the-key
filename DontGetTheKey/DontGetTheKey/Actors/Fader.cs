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
    //For some reason, it has to be a constant or it doesn't work!?!?
    //int alpha = 50;
    //new Color(255,255,255,alpha)
    //Does not work, but
    //new Color(255,255,255,50)
    //Does
    class Fader : Actor
    {
        //Rate for fade-in/out
        int rate;

        int opacity = 4;

        public Fader(SpriteBatch sb, ContentManager contentManager, int rate)
            : base(sb, contentManager, new Vector2(0,0), "black", new Rectangle(0,0,0,0)) {
                this.rate = rate;
        }

        public override void Update(GameTime gameTime)
        {
            elapsed += gameTime.ElapsedGameTime.Milliseconds;
            if (elapsed > rate)
            {
                opacity -= 1;
                elapsed = 0;
            }
            //SO STUPID
            switch (opacity)
            {
                case 4:
                    color = new Color(255, 255, 255, 255);
                    break;
                case 3:
                    color = new Color(255, 255, 255, 191);
                    break;
                case 2:
                    color = new Color(255, 255, 255, 127);
                    break;
                case 1:
                    color = new Color(255, 255, 255, 63);
                    break;
                default:
                    color = new Color(255, 255, 255, 0);
                    break;
            }
        }

    }
}
