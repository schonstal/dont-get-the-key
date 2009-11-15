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
    class SoundBank
    {
        static SoundBank instance;

        ContentManager content;

        Dictionary<string, SoundEffect> soundEffects;
        Dictionary<string, SoundEffectInstance> sei;

        private SoundBank() {
            soundEffects = new Dictionary<string, SoundEffect>();
            sei = new Dictionary<string, SoundEffectInstance>();
        }

        public static SoundBank Instance {
            get {
                if (instance == null)
                    instance = new SoundBank();
                return instance; 
            }
        }

        //There should only be one instance of a sound effect at a time.
        public void play(string effectName) {
            if (!sei.ContainsKey(effectName) && soundEffects.ContainsKey(effectName))
                sei[effectName] = soundEffects[effectName].Play();
            else
                sei[effectName].Play();
        }

        public void play(string effectName, float volume, float pitch, float pan, bool loop) {
            if (!sei.ContainsKey(effectName) && soundEffects.ContainsKey(effectName)) {
                sei[effectName] = soundEffects[effectName].Play(volume, pitch, pan, loop);
            } else {
                sei[effectName].Volume = volume;
                sei[effectName].Pitch = pitch;
                sei[effectName].Pan = pan;
                sei[effectName].Play(); //Can't loop except when starting
            }
        }

        public void stop(string effectName) {
            if (sei.ContainsKey(effectName))
                sei[effectName].Dispose();
        }

        public void loadSound(string assetName) {
            soundEffects[assetName] = content.Load<SoundEffect>(assetName);
        }
    }
}
