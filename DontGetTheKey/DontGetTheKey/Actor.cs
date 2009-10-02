using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

using System.Text;

namespace DontGetTheKey
{
    //Actors are defined by collections of components, rather than type
    public class Actor
    {
        protected string name;
        protected SpriteBatch spriteBatch;
        protected ContentManager content;

        public Actor(string actorName, SpriteBatch sb, ContentManager contentManager)
        { 
            name = actorName;
            spriteBatch = sb;
            content = contentManager;
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

        public void Draw() 
        {
            throw new NotImplementedException();
        }

        public void LoadContent() 
        {
            throw new NotImplementedException();
        }
    }
}
