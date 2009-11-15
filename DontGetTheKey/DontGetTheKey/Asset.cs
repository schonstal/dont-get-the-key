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
    class Asset
    {
        static Asset instance;

        Dictionary<string, Texture2D> textures;
        Dictionary<string, SoundEffect> soundEffects;
        Dictionary<string, SoundEffectInstance> sei;
        //only need one, it can be changed
        SpriteFont spriteFont; 

        private Asset() {
            textures = new Dictionary<string, Texture2D>();
            soundEffects = new Dictionary<string, SoundEffect>();
        }

        public static Asset Instance {
            get {
                if (instance == null)
                    instance = new Asset();
                return instance; 
            }
        }

        public Texture2D texture(string tex) {
            return textures[tex];
        }

        //There should only be one instance of a sound effect at a time.
        public void playSound(string effectName) {
            if (!sei.ContainsKey(effectName) && soundEffects.ContainsKey(effectName))
                sei[effectName] = soundEffects[effectName].Play();
            else
                sei[effectName].Play();
        }

        public SpriteFont font {
            get { return spriteFont; }
        }

        public void loadTexture(string assetName, ContentManager content) {
            textures[assetName] = content.Load<Texture2D>(assetName);
        }

        public void loadSound(string assetName, ContentManager content) {
            soundEffects[assetName] = content.Load<SoundEffect>(assetName);
        }

        public void loadFont(string assetName, ContentManager content) {
            spriteFont = content.Load<SpriteFont>(assetName);
        }
    }
}
