﻿using System;
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
        SoundEffectInstance title;

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

            Register(
                "title",
                new Actor(
                    sb,
                    content,
                    new Vector2(-400, -400),
                    content.Load<Texture2D>("title"),
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
            } else if (intro.State == SoundState.Stopped && title == null) {
                content.Load<SoundEffect>("menu").Play();
                actors["title"].Transpose(new Vector2(32, 8));
                title = content.Load<SoundEffect>("titlemusic_main").Play(1.0f, 0, 0, true);
            } else {

            }
            base.Update(gameTime);
        }
    }
}
