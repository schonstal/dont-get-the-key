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
    class InventoryUp : State
    {
        public InventoryUp(SpriteBatch sb, ContentManager contentManager, 
            Dictionary<string, Actor> actors)
            : base(sb, contentManager) {
            this.actors = actors;
            SoundBank.Instance.play("pause");
            foreach (KeyValuePair<string, Actor> kvp in actors)
                kvp.Value.Tween(new Vector2(kvp.Value.Position.X, kvp.Value.Position.Y + 184), 1.2);

            ((Character)actors["main"]).PlayerControlled = false;
            ((Character)actors["main"]).Walking = false;
        }

        public override void Update(GameTime gameTime) {
              

            base.Update(gameTime);
        }
    }
}
