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
        float wait = 1.0f;
        float sweep = 1.5f;

        float elapsed = 0;

        bool tweened = false;

        public GotKey(SpriteBatch sb, ContentManager contentManager, 
            Dictionary<string, Actor> actors)
            : base(sb, contentManager) {
            this.actors = actors;
            actors.Remove("key_shadow");

            Register(
                "clear_left",
                new Actor(
                    sb,
                    contentManager,
                    new Vector2(-128, 48),
                    "swipe",
                    new Rectangle(0, 0, 0, 0)
                    )
                );

            Register(
                "clear_right",
                new Actor(
                    sb,
                    contentManager,
                    new Vector2(320, 48),
                    "swipe",
                    new Rectangle(0, 0, 0, 0)
                    )
                );

            Register("got_key", new Message(sb, contentManager, "YOU GOT THE KEY!"));
            SoundBank.Instance.stop("bgmusic");
            SoundBank.Instance.stop("bgmusic_fast");


        }

        public override void Update(GameTime gameTime) {
            if (tweened == false) {
                elapsed += gameTime.ElapsedGameTime.Milliseconds;
                if (wait * 1000 <= elapsed) {
                    actors["clear_left"].Tween(new Vector2(32, 48), sweep);
                    actors["clear_right"].Tween(new Vector2(160, 48), sweep);
                    tweened = true;
                }
            }
            if (actors["clear_left"].Position.X > 31 && actors["clear_right"].Position.X < 161) {
                    GameState.Instance.Enter(new GameOver(spriteBatch, content, actors));
            }
            base.Update(gameTime);
        }
    }
}
