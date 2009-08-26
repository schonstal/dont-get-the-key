using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DontGetTheKey
{
    public abstract class Component
    {
        protected Actor owner;
        protected int priority;

        public int Priority
        {
            get
            {
                return priority;
            }
            set
            {
                priority = value;
            }
        }

        public abstract void Update();
        public abstract void Draw();
        public abstract void LoadContent();
        public abstract void Init(Actor a, Dictionary<string, object> parameters);
    }
}
