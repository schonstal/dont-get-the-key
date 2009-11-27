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
    class InGame : State
    {
        public InGame(SpriteBatch sb, ContentManager contentManager, 
            Dictionary<string, Actor> actors)
            : base(sb, contentManager) {
            this.actors = actors;
            ((Character)actors["main"]).PlayerControlled = true;
            actors.Remove("statcover");
            SoundBank.Instance.play("bgmusic", 1, 0, 0, true);

            Register(
                "chest",
                new Actor(
                    sb,
                    contentManager,
                    new Vector2(240, 136),
                    "chest",
                    new Rectangle(0, 0, 0, 0)
                    )
                );
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
        }
    }
}
