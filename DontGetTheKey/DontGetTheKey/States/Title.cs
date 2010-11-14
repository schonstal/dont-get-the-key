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
        bool startPressed = false;

        public Title(SpriteBatch sb, ContentManager content, Dictionary<string, Actor> actors)
            : base(sb, content) {
            this.actors = actors;
            Register(
                "title",
                new TitleBar(
                    sb,
                    content,
                    new Vector2(32, 8),
                    "title",
                    new Rectangle(0, 0, 0, 0)
                    )
                );

            Register(
                "push_start",
                new PushStart(
                    sb,
                    content,
                    new Vector2(92, 183),
                    "",
                    new Rectangle(0, 0, 0, 0)
                    )
                );
            //Hack :(
            startPressed = true;
        }

        public override void Update(GameTime gameTime) {

            if (SoundBank.Instance.effect("menu") == null &&
                SoundBank.Instance.effect("titlemusic_main") == null) {
                SoundBank.Instance.play("menu");
                SoundBank.Instance.play("titlemusic_main", 1.0f, 0, 0, true);
            }
            
            if(!pokeInput(PlayerIndex.One))
                if(!pokeInput(PlayerIndex.Two))
                    if(!pokeInput(PlayerIndex.Three))
                        pokeInput(PlayerIndex.Four);

            base.Update(gameTime);
        }

        private bool pokeInput(PlayerIndex p) {
            InputHandler.Instance.Player = p;
            if (InputHandler.Instance.pressed("Start"))
            {
                if (startPressed == false)
                {
                    SoundBank.Instance.play("start");
                    SoundBank.Instance.stop("menu");
                    SoundBank.Instance.stop("titlemusic_main");
                    GameState.Instance.Enter(new StartPressed(spriteBatch, content, actors));
                    return true;
                }
                startPressed = true;
            }
            else
            {
                startPressed = false;
            }

            return false;
        }
    }
}
