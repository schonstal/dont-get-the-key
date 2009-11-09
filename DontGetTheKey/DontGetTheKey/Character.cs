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
    //So cool
    public class Character : Actor
    {
        SoundEffectInstance[] walk;
        Rectangle target;
        float velocity = 45;
        float sensitivity = 0.3f;

        float rootbeer = 0; //elapsed
        float fps = 4;
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

        public Character(SpriteBatch sb, ContentManager contentManager,
            Vector2 pos, Texture2D texture, Rectangle box)
            : base(sb, contentManager, pos, texture, box) {
            //Player Input
            input = new InputHandler(PlayerIndex.One);
            target = new Rectangle(0, 0, 16, 16);

            //walking sounds
            walk = new SoundEffectInstance[2];
            walk[0] = loadSound(contentManager, "walk1");
            walk[1] = loadSound(contentManager, "walk2");
            return;
        }

        public override void Update(GameTime gameTime) {
            //Check inputs
            if (playerControlled) {
                if (input.held("Right") || input.LeftStick.X > sensitivity) {
                    setWalk(State.right, 0);
                } else if (input.held("Left") || input.LeftStick.X < -sensitivity) {
                    setWalk(State.left, 7);
                } else if (input.held("Up") || input.LeftStick.Y > sensitivity) {
                    setWalk(State.up, 2);
                } else if (input.held("Down") || input.LeftStick.Y < -sensitivity) {
                    setWalk(State.down, 4);
                } else {
                    walking = false;
                }
            }

            //Perform actions based on state -- don't do it in setWalk because there is no dynamic progamming :(
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
            target.X = 16 * (frame + offset);
            spriteBatch.Draw(sprite, position, target, Color.White);
        }


        ////////////
        //Helpers
        ////////////
        private void Animate(GameTime gameTime) {
            rootbeer += gameTime.ElapsedGameTime.Milliseconds;
            if (1000 / fps <= rootbeer) {
                frame = (frame + 1) % 2;
                walk[frame].Play();
                rootbeer = 0;
            }
        }

        private void setWalk(State direction, int frameOffset) {
            walking = true;
            state = direction;
            offset = frameOffset;
        }

        //Weird that SEI doesn't have something like this
        SoundEffectInstance loadSound(ContentManager contentManager, string name) {
            SoundEffect sound = contentManager.Load<SoundEffect>(name);
            SoundEffectInstance instance = sound.Play(0, 0, 0, false);
            instance.Stop();
            instance.Volume = 0.8f;
            return instance;
        }
    }
}
