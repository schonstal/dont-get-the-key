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
    class ScrollRight : State
    {
        public ScrollRight(SpriteBatch sb, ContentManager contentManager, 
            Dictionary<string, Actor> actors)
            : base(sb, contentManager) {
            this.actors = actors;
            actors["background"].Tween(new Vector2(-224, -184), 1.5);
            actors["main"].Transpose(new Vector2(-500, -500));
        }

        public override void Update(GameTime gameTime) {
            if (actors["background"].Position.X <= -223) {
                actors["main"].Transpose(new Vector2(55, 136));
                GameState.Instance.Enter(new EnterGame(spriteBatch, content, actors));
            }

            base.Update(gameTime);
        }
    }
}
