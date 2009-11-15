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
    //Lazy lagged
    class ImageBank
    {
        static ImageBank instance;

        ContentManager content;

        Dictionary<string, Texture2D> textures;
        //only need one, it can be changed
        SpriteFont spriteFont;

        private ImageBank() {
            textures = new Dictionary<string, Texture2D>();
        }

        public static ImageBank Instance {
            get {
                if (instance == null)
                    instance = new ImageBank();
                return instance; 
            }
        }

        public ContentManager Content {
            set { content = value; }
        }

        public Texture2D texture(string tex) {
            return textures[tex];
        }

        public SpriteFont font {
            get { return spriteFont; }
        }

        public void loadTexture(string assetName) {
            textures[assetName] = content.Load<Texture2D>(assetName);
        }

        public void loadFont(string assetName) {
            spriteFont = content.Load<SpriteFont>(assetName);
        }
    }
}
