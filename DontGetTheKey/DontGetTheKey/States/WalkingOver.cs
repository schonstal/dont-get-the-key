using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DontGetTheKey
{
    class WalkingOver : State
    {
        public WalkingOver(SpriteBatch sb, ContentManager contentManager, 
            Dictionary<string, Actor> actors)
            : base(sb, contentManager) {
            this.actors = actors;
        }
    }
}
