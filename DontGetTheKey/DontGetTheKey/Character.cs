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
    public class Character : Actor
    {
        Rectangle target;
        public Character(string actorName, SpriteBatch sb, ContentManager contentManager,
            Vector2 pos, Texture2D texture, Rectangle box)
            : base(actorName, sb, contentManager, pos, texture, box)
        {
            target = new Rectangle(0, 0, 16, 16);
            return;
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(sprite, position, target, Color.White);
        }
    }
}
