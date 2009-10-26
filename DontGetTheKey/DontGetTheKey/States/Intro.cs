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
    public class Intro : State
    {
        public Intro(SpriteBatch sb, ContentManager contentManager) : base(sb, contentManager) 
        {
            spriteBatch = sb;
            content = contentManager;
            Register(new Character("main", sb, content));
        }
    }
}
