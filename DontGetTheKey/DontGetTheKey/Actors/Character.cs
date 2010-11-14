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
    //Disgusting -- this ended up having so much special stuff 
    //should not have been an actor
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
        int[] offsets;

        float ptime = 0;
        float pfps = 4.0f;

        Rectangle boundingBox;

        enum State
        {
            left,
            right,
            up,
            down,
            key,
            dance
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

        public String Heading {
            get {
                switch (state) {
                    case State.down:
                        return "D";
                    case State.left:
                        return "L";
                    case State.up:
                        return "U";
                    case State.right:
                        return "R";
                }
                return "";
            }
        }

        public Character(SpriteBatch sb, ContentManager contentManager,
            Vector2 pos, string texture, Rectangle box)
            : base(sb, contentManager, pos, texture, box) {      
                priority = 2;
                target = new Rectangle(0, 0, 16, 16);
                ptm = new Vector2(0, 0);

                offsets = new int[5] {0,2,7,4,6};

                boundingBox = new Rectangle(64, 78, 192, 121);
                return;
        }

        public override void Update(GameTime gameTime) {
            Vector2 chunk;
            Rectangle next;

            if (GameState.Instance.Current.Collision(HitBox, "key") && state != State.key) {
                getKey();
            }

            //Check inputs
            if (playerControlled) {
                if (InputHandler.Instance.held("Right") || InputHandler.Instance.LeftStick.X > sensitivity) {
                    setWalk(State.right, 0);
                } else if (InputHandler.Instance.held("Left") || InputHandler.Instance.LeftStick.X < -sensitivity) {
                    setWalk(State.left, 2);
                } else if (InputHandler.Instance.held("Up") || InputHandler.Instance.LeftStick.Y > sensitivity) {
                    setWalk(State.up, 1);
                } else if (InputHandler.Instance.held("Down") || InputHandler.Instance.LeftStick.Y < -sensitivity) {
                    setWalk(State.down, 3);
                } else {
                    Walking = false;
                }
            }

            //Animate
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
            } else if (state != State.up && state != State.down && !playing && (state != State.dance)) {
                frame = 0;
            }

            chunk = new Vector2((float)Math.Floor(ptm.X), (float)Math.Floor(ptm.Y));
            next = new Rectangle(HitBox.X + (int)chunk.X, HitBox.Y + (int)chunk.Y,
                    HitBox.Width, HitBox.Height);
            if (playerControlled == false || boundingBox.Contains(next) 
                && !GameState.Instance.Current.Collision(next, "chest")) {
                position += chunk;
            } else if (playerControlled != false) {
                Punt(gameTime);
            }

            ptm -= chunk;

            if (state == State.dance)
                Dance(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime) {
            target.X = 16 * (frame + offsets[offset]);
            spriteBatch.Draw(ImageBank.Instance.texture(sprite), position, target, color);
        }

        public override void Celebrate() {
            Freeze();
            state = State.dance;
            fps = 5.6f;
            frame = 1;
            base.Celebrate();
        }

        public void Freeze() {
            walking = false;
            playing = false;
            playerControlled = false;
        }

        ////////////
        //Helpers
        ////////////
        private void Animate(GameTime gameTime) {
            //I really wouldn't have had to do this every time if I had just made a method that
            //takes in a delegate... oh well.
            rootbeer += gameTime.ElapsedGameTime.Milliseconds;
            if (1000 / fps <= rootbeer) {
                frame = (frame + 1) % 2;
                SoundBank.Instance.play((frame == 0 ? "walk1" : "walk2"), 0.5f, 0, 0, false);
                rootbeer = 0;
            }
        }

        private void Dance(GameTime gameTime) {
            rootbeer += gameTime.ElapsedGameTime.Milliseconds;
            if (1000 / fps <= rootbeer) {
                offset = (offset + 1) % 4;
                rootbeer = 0;
            }
        }

        private void setWalk(State direction, int frameOffset) {
            Walking = true;
            state = direction;
            offset = frameOffset;
        }

        private void getKey() {
            state = State.key;
            Freeze();
            offset = 4;
            frame = 0;
        }

        private void Punt(GameTime gameTime) {
            if(GameState.Instance.Current.GetType().Name == "InGame") {
            //I really should have made an animation object
                ptime += gameTime.ElapsedGameTime.Milliseconds;
                if (1000 / pfps <= ptime) {
                    switch (state) {
                        case State.left:
                            Move(new Vector2(3, 0));
                            break;
                        case State.right:
                            Move(new Vector2(-3, 0));
                            break;
                        case State.up:
                            Move(new Vector2(0, 3));
                            break;
                        case State.down:
                            Move(new Vector2(0, -3));
                            break;
                    }
                    SoundBank.Instance.play("hit_wall");
                    ptime = 0;
                }
            }
        }
    }
}
