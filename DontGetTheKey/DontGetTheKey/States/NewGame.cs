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
    public class NewGame : State
    {
        float elapsed;
        public NewGame(SpriteBatch sb, ContentManager contentManager)
            : base(sb, contentManager) {
            //Background
            Register(
              "background",
              new Background(
                  sb,
                  content,
                  new Vector2(32, -184),
                  "background",
                  new Rectangle(0, 0, 0, 0)
                  )
              );

            //Main PC
            Register(
                "main",
                new Character(
                    sb,
                    content,
                    new Vector2(152, 136),
                    "character",
                    new Rectangle(0, 0, 16, 16)
                    )
                );

            ((Character)actors["main"]).Playing = false;

        }

        public override void Update(GameTime gameTime) {
            GameState.Instance.Enter(new Title(spriteBatch, content, actors));
            base.Update(gameTime);
        }
    }
}
