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
    class GotKey : State
    {
        public GotKey(SpriteBatch sb, ContentManager contentManager, 
            Dictionary<string, Actor> actors)
            : base(sb, contentManager) {
            this.actors = actors;
            actors.Remove("key_shadow");
            Register("got_key", new Message(sb, contentManager, "YOU GOT THE KEY!"));
            SoundBank.Instance.stop("bgmusic");
        }

        public override void Update(GameTime gameTime) {
            if(InputHandler.Instance.pressed("A") || InputHandler.Instance.pressed("Start"))
                GameState.Instance.Enter(new GameOver(spriteBatch, content, actors));
            base.Update(gameTime);
        }
    }
}
