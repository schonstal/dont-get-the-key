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
    //So cool
    public class Character : Actor
    {
        Rectangle target;
        float velocity = 30;
        float sensitivity = 0.3f;

        float rootbeer = 0;
        float fps = 8;
        int frame = 0;
        int offset = 0;

        //He would pick you up if I asked him to
        InputHandler input;

        enum State
        {
            left,
            right,
            up,
            down,
            key
        }

        State state = State.right;
        bool playerControlled = true;
        bool walking = true;

        public Character(string actorName, SpriteBatch sb, ContentManager contentManager,
            Vector2 pos, Texture2D texture, Rectangle box)
            : base(actorName, sb, contentManager, pos, texture, box) {
            input = new InputHandler(PlayerIndex.One);
            target = new Rectangle(0, 0, 16, 16);
            return;
        }

        public override void Update(GameTime gameTime) {
            //Check inputs
            if (playerControlled) {
                if (input.held("Right") || input.LeftStick.X > sensitivity) {
                    walking = true;
                    state = State.right;
                } else if (input.held("Left") || input.LeftStick.X < -sensitivity) {
                    walking = true;
                    state = State.left;
                } else if (input.held("Up") || input.LeftStick.Y > sensitivity) {
                    walking = true;
                    state = State.up;
                } else if (input.held("Down") || input.LeftStick.Y < -sensitivity) {
                    walking = true;
                    state = State.down;
                } else {
                    walking = false;
                }
            }

            //Perform actions based on state (primitive is nice sometimes)
            if (walking == true) {
                Animate(gameTime);
                switch (state) {
                    case State.right:
                        position.X += (velocity * (float)gameTime.ElapsedGameTime.Milliseconds) / 1000;
                        break;
                    case State.left:
                        position.X -= (velocity * (float)gameTime.ElapsedGameTime.Milliseconds) / 1000;
                        break;
                    case State.up:
                        position.Y -= (velocity * (float)gameTime.ElapsedGameTime.Milliseconds) / 1000;
                        break;
                    case State.down:
                        position.Y += (velocity * (float)gameTime.ElapsedGameTime.Milliseconds) / 1000;
                        break;
                }
            } else if (state != State.up && state != State.down) {
                frame = 0;
            }
        }

        public override void Draw(GameTime gameTime) {
            target.X = 16 * frame + offset;
            spriteBatch.Draw(sprite, position, target, Color.White);
        }

        private void Animate(GameTime gameTime) {
            rootbeer += gameTime.ElapsedGameTime.Milliseconds;
            if (1000 / fps <= rootbeer) {
                frame = (frame + 1) % 2;
                //Play the walking sound here.
                rootbeer = 0;
            }

        }
    }
}
