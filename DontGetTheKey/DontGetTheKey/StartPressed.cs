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
    class StartPressed : State
    {
        public StartPressed(SpriteBatch sb, ContentManager contentManager, Dictionary<string, Actor> actors)
            : base(sb, contentManager) {
            this.actors = actors;
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
        }
    }
}
