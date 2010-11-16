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
    class DifficultySelect : State
    {
        public DifficultySelect(SpriteBatch sb, ContentManager contentManager,
            Dictionary<string, Actor> actors)
            : base(sb, contentManager)
        {
            this.actors = actors;
            actors.Remove("push_start");

            Register("easy", new Message(sb, contentManager, "EASY"));
            if (!Guide.IsTrialMode)
                Register("hard", new Message(sb, contentManager, "HARD"));
            else
                Register("hard", new Message(sb, contentManager, "HARD", Color.Gray));

            actors["easy"].Move(new Vector2(0, 80));
            actors["hard"].Move(new Vector2(0, 96));
            Register("key", new Key(sb, content, new Vector2(127, 175), "key", new Rectangle(0, 0, 0, 0)));
        }

        public override void Update(GameTime gameTime) {
            if (InputHandler.Instance.pressed("Up") || InputHandler.Instance.pressed("Down") ||
                InputHandler.Instance.stickPressed("LeftStick", "Up") || InputHandler.Instance.stickPressed("LeftStick", "Down"))
            {
                SoundBank.Instance.play("select", 0.5f, 0, 0, false);
                actors["key"].Move(new Vector2(0, (GameState.Instance.Easy ? 16 : -16)));
                GameState.Instance.Easy = !GameState.Instance.Easy;
            }
            else if (InputHandler.Instance.pressed("Start") || InputHandler.Instance.pressed("A"))
            {
                if (Guide.IsTrialMode && !GameState.Instance.Easy) 
                {
                    SoundBank.Instance.play("nope", 0.4f, 0, 0, false);
                }
                else
                {
                    SoundBank.Instance.play("pickup_key", 1.0f, 0, 0, false);
                    GameState.Instance.Enter(new WalkingOver(spriteBatch, content, actors));
                }
            }
            base.Update(gameTime);
        }
    }
}
