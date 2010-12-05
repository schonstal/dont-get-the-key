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
    class DiedOf : State
    {
        public DiedOf(SpriteBatch sb, ContentManager contentManager, 
            Dictionary<string, Actor> actors)
            : base(sb, contentManager) {
            this.actors = actors;
            SoundBank.Instance.stop("congrats");

            foreach (String key in new List<String>() { "main", "background", "chest", "door", "grats", "stats", "key", "key_shadow" })
                actors.Remove(key);

            Register("gravestone", new Actor(spriteBatch, content, new Vector2(144, 120), "gravestone", new Rectangle(0, 0, 0, 0)));
            Register("died", new Message(sb, contentManager, "YOU HAVE DIED OF"));
            Register("reason", new Message(sb, contentManager, reason()));
            actors["reason"].Move(new Vector2(0, 8));
        }

        public override void Update(GameTime gameTime) {
            if (InputHandler.Instance.pressed("Any")) {
                GameState.Instance.Enter(new Restart(spriteBatch, content, actors));
            }
            base.Update(gameTime);
        }

        private string reason() {
            Random rand = new Random();
            List<String> reasons = new List<String>() { "DYSENTERY", "DIABETES", "AIDS", "POLIO" };
            return reasons[rand.Next(reasons.Count)];
        }
    }
}
