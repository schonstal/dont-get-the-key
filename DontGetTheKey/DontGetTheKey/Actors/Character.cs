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
    //Disgusting
    public class Character : Actor
    {
        Rectangle target;
        float velocity = 45;
        float sensitivity = 0.3f;

        Vector2 ptm; //for walking
        float rootbeer = 0; //elapsed
        float fps = 4;
        int frame = 0;
        int offset = 0;

        Rectangle boundingBox;

        enum State
        {
            left,
            right,
            up,
            down,
            key
        }

        State state = State.right;
        bool playerControlled = false;
        bool walking = false;
        bool playing = true;

        public bool Walking {
            get { return walking; }
            set { walking = value; playing = value; }
        }

        public bool Playing {
            get { return playing; }
            set { playing = value; }
        }

        public bool PlayerControlled {
            get { return playerControlled; }
            set { playerControlled = value; }
        }

        public Rectangle HitBox {
            get {
                return new Rectangle(
                    hitBox.X + (int)position.X,
                    hitBox.Y + (int)position.Y,
                    hitBox.Width,
                    hitBox.Height
                    );
            }
        }

        public Character(SpriteBatch sb, ContentManager contentManager,
            Vector2 pos, string texture, Rectangle box)
            : base(sb, contentManager, pos, texture, box) {
            target = new Rectangle(0, 0, 16, 16);
            ptm = new Vector2(0, 0);

            boundingBox = new Rectangle(64, 78, 192, 121);
            return;
        }

        public override void Update(GameTime gameTime) {
            Vector2 chunk;

            //Check inputs
            if (playerControlled) {
                if (InputHandler.Instance.held("Right") || InputHandler.Instance.LeftStick.X > sensitivity) {
                    setWalk(State.right, 0);
                } else if (InputHandler.Instance.held("Left") || InputHandler.Instance.LeftStick.X < -sensitivity) {
                    setWalk(State.left, 7);
                } else if (InputHandler.Instance.held("Up") || InputHandler.Instance.LeftStick.Y > sensitivity) {
                    setWalk(State.up, 2);
                } else if (InputHandler.Instance.held("Down") || InputHandler.Instance.LeftStick.Y < -sensitivity) {
                    setWalk(State.down, 4);
                } else {
                    Walking = false;
                }
            }

            if (playing)
                Animate(gameTime);

            //Perform actions based on state -- don't do it in setWalk because there is no dynamic progamming :(
            if (walking == true) {
                switch (state) {
                    case State.right:
                        ptm.X += (velocity * (float)gameTime.ElapsedGameTime.Milliseconds) / 1000;
                        break;
                    case State.left:
                        ptm.X -= (velocity * (float)gameTime.ElapsedGameTime.Milliseconds) / 1000;
                        break;
                    case State.up:
                        ptm.Y -= (velocity * (float)gameTime.ElapsedGameTime.Milliseconds) / 1000;
                        break;
                    case State.down:
                        ptm.Y += (velocity * (float)gameTime.ElapsedGameTime.Milliseconds) / 1000;
                        break;
                }
            } else if (state != State.up && state != State.down && !playing) {
                frame = 0;
            }

            chunk = new Vector2((float)Math.Floor(ptm.X), (float)Math.Floor(ptm.Y));
            if (playerControlled == false || boundingBox.Contains(
                new Rectangle(HitBox.X + (int)chunk.X, HitBox.Y + (int)chunk.Y,
                    HitBox.Width, HitBox.Height)
                    )) {
                position += chunk;
            }
            ptm -= chunk;
        }

        public override void Draw(GameTime gameTime) {
            target.X = 16 * (frame + offset);
            spriteBatch.Draw(ImageBank.Instance.texture(sprite), position, target, Color.White);
        }

        ////////////
        //Helpers
        ////////////
        private void Animate(GameTime gameTime) {
            rootbeer += gameTime.ElapsedGameTime.Milliseconds;
            if (1000 / fps <= rootbeer) {
                frame = (frame + 1) % 2;
                SoundBank.Instance.play((frame==0?"walk1":"walk2"));
                rootbeer = 0;
            }
        }

        private void setWalk(State direction, int frameOffset) {
            Walking = true;
            state = direction;
            offset = frameOffset;
        }
    }
}
