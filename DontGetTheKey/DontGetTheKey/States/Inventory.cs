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
                    "WON'T OPEN WITHOUT A KEY"
                }, name = "BIKE LOCK"}},
                {"binoculars", new ItemTuple {messages = new List<string>() { 
                    "THE LENS COVERS ARE LOCKED IN PLACE"
                }, name = "BINOCULARS"}},
                {"goldpouch", new ItemTuple {messages = new List<string>() { 
                    "YOU CAN'T OPEN THIS WITHOUT A KEY"
                }, name = "GOLD POUCH"}},
                {"journal", new ItemTuple {messages = new List<string>() { 
                    "IT WON'T OPEN WITHOUT A KEY"
                }, name = "JOURNAL"}},
                {"keyrings", new ItemTuple {messages = new List<string>() { 
                    "YOU DON'T HAVE ANY KEYS", 
                    "YOU CAN'T USE THESE WITHOUT A KEY"
                }, name = "KEYRINGS"}},
                {"lockbox", new ItemTuple {messages = new List<string>() { 
                    "IT'S LOCKED"
                }, name = "LOCK BOX"}},
                {"locket", new ItemTuple {messages = new List<string>() { 
                    "IT'S LOCKED SHUT"
                }, name = "LOCKET"}},
                {"lockofhair", new ItemTuple {messages = new List<string>() { 
                    "IT'S A PUN"
                }, name = "HAIR LOCK"}},
                {"lockpicks", new ItemTuple {messages = new List<string>() { 
                    "NOT ENOUGH SKILL"
                }, name = "LOCK PICKS"}},
                {"padlock", new ItemTuple {messages = new List<string>() { 
                    "YOU NEED AT LEAST ONE KEY TO USE THIS"
                }, name = "PAD LOCK"}},
                {"rope", new ItemTuple {messages = new List<string>() { 
                    "IT'S LOCKED"
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
            Register("selected", new SelectedItem(spriteBatch, content));
        }

        public override void Update(GameTime gameTime) {
            select();

            //Fill the name box
            ((SelectedItem)actors["selected"]).Set(
                items[((Selector)actors["selector"]).Slot].Texture,
                items[((Selector)actors["selector"]).Slot].Name);

            //Display description
            if (InputHandler.Instance.pressed("A")) {
                if (!actors.ContainsKey("description"))
                {
                    Register("description", new TypingMessage(spriteBatch, content,
                        items[((Selector)actors["selector"]).Slot].Info));
                }
                else
                {
                    actors.Remove("description");
                }
            }

            //Remove description
            if (InputHandler.Instance.pressed("B")) 
                if (actors.ContainsKey("description"))
                    actors.Remove("description");
                else
                    GameState.Instance.Enter(new InventoryDown(spriteBatch, content, actors));

            //Remove description/leave inventory
            if (InputHandler.Instance.pressed("Start"))
            {
                if (actors.ContainsKey("description"))
                    actors.Remove("description");
                GameState.Instance.Enter(new InventoryDown(spriteBatch, content, actors));
            }

            base.Update(gameTime);
        }

        void select() {
            if (InputHandler.Instance.pressed("Left") || InputHandler.Instance.stickPressed("LeftStick", "Left"))
            {
                if (((Selector)actors["selector"]).Slot > 0)
                    ((Selector)actors["selector"]).Slot--;
                else
                    ((Selector)actors["selector"]).Slot = items.Count - 1;
                moveEffect();
            }

            if (InputHandler.Instance.pressed("Right") || InputHandler.Instance.stickPressed("LeftStick", "Right"))
            {
                if (((Selector)actors["selector"]).Slot < items.Count - 1)
                    ((Selector)actors["selector"]).Slot++;
                else
                    ((Selector)actors["selector"]).Slot = 0;
                moveEffect();
            }

            if (InputHandler.Instance.pressed("Up") || InputHandler.Instance.stickPressed("LeftStick", "Up"))
            {
                if (((Selector)actors["selector"]).Slot - 4 >= 0) {
                    ((Selector)actors["selector"]).Slot -= 4;
                    moveEffect();
                }
            }

            if (InputHandler.Instance.pressed("Down") || InputHandler.Instance.stickPressed("LeftStick", "Down"))
            {
                if (((Selector)actors["selector"]).Slot + 4 < items.Count) {
                    ((Selector)actors["selector"]).Slot += 4;
                    moveEffect();
                }
            }
        }

        void moveEffect() {
            if (actors.ContainsKey("description"))
                actors.Remove("description");
            SoundBank.Instance.play("select", 0.8f, 0, 0, false);
        }
    }

    //C# *really* needs native tuples (and a lot of other stuff)
    class ItemTuple
    {
        public List<string> messages;
        public string name;
    }
}
