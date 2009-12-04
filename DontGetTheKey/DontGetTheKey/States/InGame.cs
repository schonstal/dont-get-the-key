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
        Vector2 lives;
        Vector2 keys;

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
                    new Rectangle(0, 0, 16, 16)
                    )
                );

            Register(
                "stats",
                new Stats(
                    sb,
                    contentManager,
                    new Vector2(256, 40),
                    "stats",
                    new Rectangle(0, 0, 0, 0)
                    )
                );

            Register(
                "key_shadow",
                new Actor(
                    sb,
                    contentManager,
                    new Vector2(152, 136),
                    "key_shadow",
                    new Rectangle(0, 0, 0, 0)
                    )
                );

            Register(
                "key",
                new Key(
                    sb,
                    contentManager,
                    new Vector2(152, 128),
                    "key",
                    new Rectangle(6, 10, 6, 6)
                    )
                );

        }

        public override void Update(GameTime gameTime) {
            if (GameState.Instance.Current.Collision("key", "main")) {
                actors["key"].Transpose(new Vector2(actors["main"].Position.X, actors["main"].Position.Y - 15));
                GameState.Instance.Enter(new GotKey(spriteBatch, content, actors));
            }
            if(InputHandler.Instance.pressed("Start"))
                GameState.Instance.Enter(new InventoryUp(spriteBatch, content, actors));
            if(((Stats)actors["stats"]).Remaining <= 0)
                GameState.Instance.Enter(new Congrats(spriteBatch, content, actors));
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime) {
            base.Draw(gameTime);
        }
    }
}
