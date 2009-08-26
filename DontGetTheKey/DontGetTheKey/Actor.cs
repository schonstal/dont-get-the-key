using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

using System.Text;

namespace DontGetTheKey
{
    //Actors are defined by collections of components, rather than type
    public class Actor
    {
        protected string name;
        protected SpriteBatch spriteBatch;
        protected ContentManager content;

        //Dynamic Stuff
        private Dictionary<string, Component> components;

        private Dictionary<string, object> properties;
        private Dictionary<string, Action> methods;

        public void Init(string actorName, SpriteBatch sb, ContentManager contentManager)
        { 
            components = new Dictionary<string,Component>();
            properties = new Dictionary<string, object>();
            methods = new Dictionary<string, Action>();
            name = actorName;
            spriteBatch = sb;
            content = contentManager;
        }

        public void Update()
        {
            //It seems like there should be a better way to do this. (Like List.ForEach)
            foreach (KeyValuePair<string, Component> kvp in components)
                kvp.Value.Update();
        }

        public void Draw() 
        {
            foreach (KeyValuePair<string, Component> kvp in components)
                kvp.Value.Draw(); 
        }

        public void LoadContent() 
        {
            foreach (KeyValuePair<string, Component> kvp in components)
                kvp.Value.LoadContent();
        }

        //Creates a new component and adds it to the list of components; accepts arbitrary data.
        public void Register<C>(Dictionary<string, object> parameters) where C : Component, new()
        {
            if (!components.Any(c => c.GetType() is C))
            {
                C c = new C();
                c.Init(this, parameters);
                //components.Add(c);
            }
        }

        //register and invoke arbitrary methods
        public void Register(string methodName, Action method)
        {
            methods[methodName] = method;
        }

        public Action Invoke(string method)
        {
            if (methods.ContainsKey(method))
                return methods[method];
            else
                throw new Exception("Method " + method + " undefined");
        }
    }
}
