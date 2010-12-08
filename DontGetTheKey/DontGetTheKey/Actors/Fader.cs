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
        //Percent opacity increase
        float percent;

        public Fader(SpriteBatch sb, ContentManager contentManager, int rate, float percent)
            : base(sb, contentManager, new Vector2(56,-136), "black", new Rectangle(0,0,0,0)) {
                this.rate = rate;
                this.percent = percent;
        }

        public override void Update(GameTime gameTime)
        {
            elapsed += gameTime.ElapsedGameTime.Milliseconds;
        }
    }
}
