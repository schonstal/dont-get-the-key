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
    public class Intro : State
    {
        SoundEffectInstance intro;

        public Intro(SpriteBatch sb, ContentManager contentManager)
            : base(sb, contentManager) {
            //Background
            Register(
                "background",
                new Actor(
                    sb,
                    content,
                    new Vector2(150, 56),
                    content.Load<Texture2D>("background"),
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
                    content.Load<Texture2D>("character"),
                    new Rectangle(152, 136, 16, 16)
                    )
                );
        }

        public override void Update(GameTime gameTime) {
            if (intro == null) {
                intro = content.Load<SoundEffect>("titlemusic_intro").Play();
            } else if (intro.State == SoundState.Stopped) {
                intro.Dispose();
                Character main = (Character)actors["main"];
                main.Playing = false;
                GameState.Instance.Enter(new Title(spriteBatch, content, actors));
            }
            base.Update(gameTime);
        }
    }
}
