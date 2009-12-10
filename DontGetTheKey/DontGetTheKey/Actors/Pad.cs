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
    class Pad : Actor
    {
        public Pad(SpriteBatch sb, ContentManager contentManager, Vector2 position, Rectangle hitBox)
            : base(sb, contentManager, position, "", hitBox) {
        }

        public override void Update(GameTime gameTime) {
            //Doesn't Update
        }

        public override void Draw(GameTime gameTime) {
            //Doesn't Draw
        }
    }
}
