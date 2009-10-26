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
    public class Title : State
    {
        public Title(SpriteBatch sb, ContentManager content)
            : base(sb, content)
        {
            Register(new Character("main", spriteBatch, content));
        }

        /*
        public void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        */
    }
}
