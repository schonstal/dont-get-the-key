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
    class Congrats : State
    {
        public Congrats(SpriteBatch sb, ContentManager contentManager, 
            Dictionary<string, Actor> actors)
            : base(sb, contentManager) {
            this.actors = actors;
            SoundBank.Instance.stop("bgmusic");
            SoundBank.Instance.play("congrats", 1, 0, 0, true);
            Register("grats", new Message(sb, contentManager, "CONGRATULATIONS!!!"));

            foreach (String key in new List<String>() { "main", "background", "chest", "door", "grats" })
                actors[key].Celebrate();
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
        }
    }
}
