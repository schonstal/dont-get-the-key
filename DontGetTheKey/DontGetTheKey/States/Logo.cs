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
    public class Logo : State
    {
        float elapsed;
        //Duration
        int timeout = 3000;

        public Logo(SpriteBatch sb, ContentManager contentManager)
            : base(sb, contentManager) {

            Register(
                "logo",
                new Actor(
                    sb,
                    content,
                    new Vector2(0, 0),
                    "logo",
                    new Rectangle(0, 0, 0, 0)
                    )
                );

            Register("fader", new Fader(sb, content, 150));
        }

        public override void Update(GameTime gameTime)
        {
            elapsed += gameTime.ElapsedGameTime.Milliseconds;
            if (elapsed >= timeout)
            {
                if (Guide.IsTrialMode)
                    GameState.Instance.Enter(new HowToPlay(spriteBatch, content));
                else
                    GameState.Instance.Enter(new Intro(spriteBatch, content));
            }

            base.Update(gameTime);
        }
    }
}
