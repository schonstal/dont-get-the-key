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
    class Selector : Actor
    {
        int slot = 0;

        public Selector(SpriteBatch sb, ContentManager contentManager)
            : base(sb, contentManager, new Vector2(48,-144), "selector", new Rectangle(0,0,0,0)) {
        }

        public int Slot {
            get { return slot; }
            set { slot = value; }
        }

        public override void Update(GameTime gameTime) {
            if (slot < 4) {
                position.X = 48 + (slot * 32);
            } else {
                position.X = 48 + ((slot - 4) * 32);
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime) {
            spriteBatch.Draw(ImageBank.Instance.texture(sprite), 
                (slot < 4 ? position : new Vector2(position.X, position.Y + 32)), color);
        }
    }
}
