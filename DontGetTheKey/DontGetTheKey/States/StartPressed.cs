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

        public StartPressed(SpriteBatch sb, ContentManager contentManager, 
            Dictionary<string, Actor> actors)
            : base(sb, contentManager) {
            this.actors = actors;
            ((PushStart)this.actors["push_start"]).Rate = 60;
        }

        public override void Update(GameTime gameTime) {
            if (SoundBank.Instance.effect("start").State == SoundState.Stopped) {
                SoundBank.Instance.stop("start");
                GameState.Instance.Enter(new WalkingOver(spriteBatch, content, actors));
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime) {
            base.Draw(gameTime);

        }
    }
}
