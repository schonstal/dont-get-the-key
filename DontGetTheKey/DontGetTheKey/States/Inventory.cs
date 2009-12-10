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
    class Inventory : State
    {
        List<Item> items;

        public Inventory(SpriteBatch sb, ContentManager contentManager, 
            Dictionary<string, Actor> actors)
            : base(sb, contentManager) {
            this.actors = actors;

            items = new List<Item>();

            Dictionary<string, ItemTuple> init = new Dictionary<string, ItemTuple>()
            {
                {"bikelock", new ItemTuple {messages = new List<string>() { 
                    "AREN'T YOU FORGETTING SOMETHING?", 
                    "SURE, YOU SHOULD ALWAYS BE PREPARED, BUT YOU DON'T EVEN HAVE A BIKE"
                }, name = "BIKE LOCK"}},
                {"binoculars", new ItemTuple {messages = new List<string>() { 
                    "AREN'T YOU FORGETTING SOMETHING?", 
                    "SURE, YOU SHOULD ALWAYS BE PREPARED, BUT YOU DON'T EVEN HAVE A BIKE"
                }, name = "BINOCULARS"}},
                {"goldpouch", new ItemTuple {messages = new List<string>() { 
                    "AREN'T YOU FORGETTING SOMETHING?", 
                    "SURE, YOU SHOULD ALWAYS BE PREPARED, BUT YOU DON'T EVEN HAVE A BIKE"
                }, name = "GOLD POUCH"}},
                {"journal", new ItemTuple {messages = new List<string>() { 
                    "AREN'T YOU FORGETTING SOMETHING?", 
                    "SURE, YOU SHOULD ALWAYS BE PREPARED, BUT YOU DON'T EVEN HAVE A BIKE"
                }, name = "JOURNAL"}},
                {"keyrings", new ItemTuple {messages = new List<string>() { 
                    "AREN'T YOU FORGETTING SOMETHING?", 
                    "SURE, YOU SHOULD ALWAYS BE PREPARED, BUT YOU DON'T EVEN HAVE A BIKE"
                }, name = "KEYRINGS"}},
                {"lockbox", new ItemTuple {messages = new List<string>() { 
                    "AREN'T YOU FORGETTING SOMETHING?", 
                    "SURE, YOU SHOULD ALWAYS BE PREPARED, BUT YOU DON'T EVEN HAVE A BIKE"
                }, name = "LOCK BOX"}},
                {"locket", new ItemTuple {messages = new List<string>() { 
                    "AREN'T YOU FORGETTING SOMETHING?", 
                    "SURE, YOU SHOULD ALWAYS BE PREPARED, BUT YOU DON'T EVEN HAVE A BIKE"
                }, name = "LOCKET"}},
                {"lockofhair", new ItemTuple {messages = new List<string>() { 
                    "AREN'T YOU FORGETTING SOMETHING?", 
                    "SURE, YOU SHOULD ALWAYS BE PREPARED, BUT YOU DON'T EVEN HAVE A BIKE"
                }, name = "LOCK OF HAIR"}},
                {"lockpicks", new ItemTuple {messages = new List<string>() { 
                    "AREN'T YOU FORGETTING SOMETHING?", 
                    "SURE, YOU SHOULD ALWAYS BE PREPARED, BUT YOU DON'T EVEN HAVE A BIKE"
                }, name = "LOCK PICKS"}},
                {"padlock", new ItemTuple {messages = new List<string>() { 
                    "AREN'T YOU FORGETTING SOMETHING?", 
                    "SURE, YOU SHOULD ALWAYS BE PREPARED, BUT YOU DON'T EVEN HAVE A BIKE"
                }, name = "PAD LOCK"}},
                {"rope", new ItemTuple {messages = new List<string>() { 
                    "AREN'T YOU FORGETTING SOMETHING?", 
                    "SURE, YOU SHOULD ALWAYS BE PREPARED, BUT YOU DON'T EVEN HAVE A BIKE"
                }, name = "ROPE"}}
            };

            Random rng = new Random();
            int slot = 0;
            foreach (KeyValuePair<string, ItemTuple> kvp in init) {
                if (rng.Next(10) < 6 && slot < 8) {
                    items.Add(new Item(spriteBatch, content, kvp.Key,
                        slot, kvp.Value.name, kvp.Value.messages));
                    slot++;
                } else if (slot == 8) {
                    break;
                }
            }

            foreach (Item i in items)
                Register(i.Name, i);

            Register("selector", new Selector(spriteBatch, content));
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
        }
    }

    //C# *really* needs native tuples (and a lot of other stuff)
    class ItemTuple
    {
        public List<string> messages;
        public string name;
    }
}
