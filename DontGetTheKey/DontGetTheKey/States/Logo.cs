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
        }

        public override void Update(GameTime gameTime) {
            elapsed += gameTime.ElapsedGameTime.Milliseconds;
            if (elapsed >= 2000 || InputHandler.Instance.pressed("Any"))
            {
                //if(Guide.IsTrialMode)
                //GameState.Instance.Enter(new HowToPlay(spriteBatch, content));
                GameState.Instance.Enter(new Intro(spriteBatch, content));
            }

            base.Update(gameTime);
        }
    }
}
