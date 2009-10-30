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
    public class Character : Actor
    {
        Rectangle target;
        float velocity = 30;
        int frame = 0;

        bool playerControlled = true;
        bool walking = true;

        enum State {
            left,
            right,
            up,
            down,
            key
        }

        State state = State.right;

        public Character(string actorName, SpriteBatch sb, ContentManager contentManager,
            Vector2 pos, Texture2D texture, Rectangle box)
            : base(actorName, sb, contentManager, pos, texture, box)
        {
            target = new Rectangle(0, 0, 16, 16);
            return;
        }

        public override void Update(GameTime gameTime)
        {
            //I hate MSVS's curly brace on next line enforcement
            if (walking == true)
            {
                //Next walking frame, then move the guy :smugdog:
                frame = (frame + 1) % 2;
                switch (state)
                {
                    case State.right:
                        position.X += (velocity * (float)gameTime.ElapsedGameTime.Milliseconds)/1000;
                        break;
                    case State.left:
                        position.X -= velocity * (float)gameTime.ElapsedGameTime.Seconds;
                        break;
                    case State.up:
                        position.Y -= velocity * (float)gameTime.ElapsedGameTime.Seconds;
                        break;
                    case State.down:
                        position.Y += velocity * (float)gameTime.ElapsedGameTime.Seconds;
                        break;
                }
            }
            else if (state != State.up && state != State.down)
            {
                frame = 0;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(sprite, position, target, Color.White);
        }

    }
}
