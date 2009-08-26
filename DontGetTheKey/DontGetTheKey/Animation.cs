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
    public class Animation
    {
        float frameRate;
        bool looping;
        int numFrames;
        float time = 0.0f;
        int frame = 0;
        
        //Where the first frame is located.
        Vector2 first;
        
        //The dimensions of each frame.
        Vector2 size;

        public Rectangle Source
        {
            get
            {
                return new Rectangle((int)first.X * frame, (int)first.Y, (int)size.X, (int)size.Y);
            }
        }

        public Animation(float rate, bool loop, Vector2 pos, Vector2 dimensions, int length)
        {
            frameRate = rate;
            looping = loop;
            first = pos;
            size = dimensions;
            numFrames = length;
        }

        public bool Play(GameTime gameTime)
        {
            //The animation has ended, and it's not to loop
            if (looping == false && frame >= numFrames)
                return false;
            
            time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            frame = (int)(time / frameRate) % numFrames;
            
            return true;
        }
    }
}
