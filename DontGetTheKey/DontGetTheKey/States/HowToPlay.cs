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
    public class HowToPlay : State
    {
        float elapsed;
        int timeout = 15000;

        public HowToPlay(SpriteBatch sb, ContentManager contentManager)
            : base(sb, contentManager) {

            Register(
                "controls",
                new Actor(
                    sb,
                    content,
                    new Vector2(0, 0),
                    "controls",
                    new Rectangle(0, 0, 0, 0)
                    )
                );

            Register("fader", new Fader(sb, content, 250, 0.25f));
        }

        public override void Update(GameTime gameTime) {
            elapsed += gameTime.ElapsedGameTime.Milliseconds;
            if (elapsed >= timeout)
            {
                ((Fader)actors["fader"]).FadeIn = false;

                if (((Fader)actors["fader"]).Finished)
                {
                    GameState.Instance.Enter(new Intro(spriteBatch, content));
                }
            }

            //Can't loop over enums because no IEnumerable?
            List<PlayerIndex> players = new List<PlayerIndex>() { PlayerIndex.One, PlayerIndex.Two, PlayerIndex.Three, PlayerIndex.Four };
            foreach (PlayerIndex p in players)
            {
                InputHandler.Instance.Player = p;
                if (InputHandler.Instance.pressed("Any"))
                {
                    GameState.Instance.Enter(new Intro(spriteBatch, content));
                }
            }


            base.Update(gameTime);
        }
    }
}
