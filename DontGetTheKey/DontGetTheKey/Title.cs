using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DontGetTheKey
{
    public class Title : State
    {
        public void Update()
        {
            actors.ForEach(a => a.Update());
        }

        public void Draw()
        {
            actors.ForEach(a => a.Draw());
        }

        public void Register(Actor a)
        {
            actors.Add(a);
        }
    }
}
