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
    class Restart : State
    {
        bool restart = true;
        public Restart(SpriteBatch sb, ContentManager contentManager, 
            Dictionary<string, Actor> actors) 
            : base(sb, contentManager) {
            this.actors = actors;
            actors.Remove("main"); //Oh snap
            actors.Remove("game_over");

            Register("restart", new Message(sb, contentManager, "RESTART"));
            Register("quit", new Message(sb, contentManager, "QUIT"));
            actors["quit"].Move(new Vector2(0, 16));

            actors["key"].Transpose(new Vector2(116, 94));
        }

        public override void Update(GameTime gameTime) {
            if (InputHandler.Instance.pressed("Up") || InputHandler.Instance.pressed("Down")) {
                actors["key"].Move(new Vector2(0, (restart ? 16 : -16)));
                restart = !restart;
                SoundBank.Instance.play("select");
            }

            if (!restart && (InputHandler.Instance.pressed("A") || InputHandler.Instance.pressed("Start")))
                GameState.Instance.Exit();
                
            base.Update(gameTime);
        }
    }
}
