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

        public void Init(Actor a)
        {
            owner = a;
        }

        public abstract void Update();
        public abstract void Draw();
        public abstract void LoadContent();

    }
}
