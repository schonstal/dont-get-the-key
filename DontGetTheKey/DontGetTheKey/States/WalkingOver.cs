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
    class WalkingOver : State
    {
        public WalkingOver(SpriteBatch sb, ContentManager contentManager, 
            Dictionary<string, Actor> actors)
            : base(sb, contentManager) {
            this.actors = actors;
            ((Character)this.actors["main"]).Walking = true;
            actors.Remove("title");
            actors.Remove("easy");
            actors.Remove("hard");
            actors.Remove("key");
        }

        public override void Update(GameTime gameTime) {
            if (actors["main"].Position.X >= 240) {
                ((Character)actors["main"]).Walking = false;
                GameState.Instance.Enter(new ScrollRight(spriteBatch, content, actors));
            }
            base.Update(gameTime);
        }
    }
}
