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
            //Main PC
            Register(
                new Character(
                    "main", 
                    sb, 
                    content,
                    new Vector2(0,0),
                    content.Load<Texture2D>("character"),
                    new Rectangle(0,0,15,15)
                    )
                );
        }
    }
}
