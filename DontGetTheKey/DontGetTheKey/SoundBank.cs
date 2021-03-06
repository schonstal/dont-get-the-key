﻿using System;
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
    //Sort of fakes one of the good aspects of dynamic programming
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

        public ContentManager Content {
            set { content = value; }
        }

        //There should only be one instance of a sound effect at a time.
        public void play(string effectName) {
            if (!sei.ContainsKey(effectName) && soundEffects.ContainsKey(effectName))
                sei[effectName] = soundEffects[effectName].CreateInstance();
            sei[effectName].Play();
        }

        public void play(string effectName, float volume, float pitch, float pan, bool loop) {
            if (!sei.ContainsKey(effectName) && soundEffects.ContainsKey(effectName))
                sei[effectName] = soundEffects[effectName].CreateInstance();
            sei[effectName].Volume = volume;
            sei[effectName].Pitch = pitch;
            sei[effectName].Pan = pan;
            if (loop)
                sei[effectName].IsLooped = true;
            sei[effectName].Play(); //Can't change loop except when starting
        }

        public void stop(string effectName) {
            if (sei.ContainsKey(effectName)) {
                sei[effectName].Dispose();
                sei.Remove(effectName);
            }
        }

        public SoundEffectInstance effect(string effectName) {
            if (sei.ContainsKey(effectName))
                return sei[effectName];
            return null;
        }

        public void load(string assetName) {
            soundEffects[assetName] = content.Load<SoundEffect>(assetName);
        }
    }
}
