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
    class GameOver : State
    {
        public GameOver(SpriteBatch sb, ContentManager contentManager, 
            Dictionary<string, Actor> actors)
            : base(sb, contentManager) {
            this.actors = actors;
            actors.Remove("background");
            actors.Remove("chest");
            actors.Remove("door");
            actors.Remove("got_key");
            actors.Remove("stats");
            Register("game_over", new Message(sb, contentManager, "GAME OVER"));
            SoundBank.Instance.play("game_over");
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
        }
    }
}
