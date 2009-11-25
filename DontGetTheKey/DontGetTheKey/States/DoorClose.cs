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
    class DoorClose : State
    {
        public DoorClose(SpriteBatch sb, ContentManager contentManager, 
            Dictionary<string, Actor> actors)
            : base(sb, contentManager) {
            this.actors = actors;
            Register(
                "door",
                new Actor(
                    sb,
                    contentManager,
                    new Vector2(40,120),
                    "door",
                    new Rectangle(0,0,0,0)
                    )
                );
        }

        public override void Update(GameTime gameTime) {
            SoundBank.Instance.play("door_close");
            GameState.Instance.Enter(new InGame(spriteBatch, content, actors));
            base.Update(gameTime);
        }
    }
}
