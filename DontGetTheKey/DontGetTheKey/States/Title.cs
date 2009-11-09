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
    public class Title : State
    {
        SoundEffectInstance menu;
        SoundEffectInstance title;

        public Title(SpriteBatch sb, ContentManager content, Dictionary<string, Actor> actors)
            : base(sb, content) {
            this.actors = actors;
            Register(
                "title",
                new Actor(
                    sb,
                    content,
                    new Vector2(32, 8),
                    content.Load<Texture2D>("title"),
                    new Rectangle(0, 0, 0, 0)
                    )
                );
        }

        public override void Update(GameTime gameTime) {
            if (menu == null && title == null) {
                menu = content.Load<SoundEffect>("menu").Play();
                title = content.Load<SoundEffect>("titlemusic_main").Play(1.0f, 0, 0, true);
            }
            pokeInput(PlayerIndex.One);
            pokeInput(PlayerIndex.Two);
            pokeInput(PlayerIndex.Three);
            pokeInput(PlayerIndex.Four);
            base.Update(gameTime);
        }

        private void pokeInput(PlayerIndex p) {
            InputHandler.Instance.Player = p;
            if (InputHandler.Instance.pressed("Start")) {
                SoundEffectInstance start = content.Load<SoundEffect>("start").Play();
                menu.Dispose();
                title.Dispose();
                GameState.Instance.Enter(new StartPressed(spriteBatch, content, actors, start));
                return;
            }
        }
    }
}
