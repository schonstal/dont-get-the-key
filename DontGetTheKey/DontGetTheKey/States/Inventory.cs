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
                    "THE LENS COVERS ARE LOCKED IN PLACE", 
                    "REMEMBER TO DOUBLE-CHECK THE MERCHANDISE WHEN TRADING WITH PARANOID BIRD WATCHERS"
                }, name = "BINOCULARS"}},
                {"goldpouch", new ItemTuple {messages = new List<string>() { 
                    "YOU CAN'T OPEN THIS WITHOUT A KEY",
                    "AT LEAST YOU'RE FINANCIALLY SECURE"
                }, name = "GOLD POUCH"}},
                {"journal", new ItemTuple {messages = new List<string>() { 
                    "IT WON'T OPEN WITHOUT A KEY",
                    "AREN'T YOU A LITTLE OLD FOR GOSSIP?",
                    "THE COVER READS \"PROPERTY OF LARRY LOCKSMITH\""
                }, name = "JOURNAL"}},
                {"keyrings", new ItemTuple {messages = new List<string>() { 
                    "YOU DON'T HAVE ANY KEYS", 
                    "YOU CAN'T USE THESE WITHOUT A KEY",
                    "JUST KEEP TELLING YOURSELF FIFTY KEYRINGS FOR A SINGLE GOLD PIECE WAS A GOOD DEAL"
                }, name = "KEYRINGS"}},
                {"lockbox", new ItemTuple {messages = new List<string>() { 
                    "IT'S LOCKED. WHAT A SURPRISE", 
                    "IF ONLY YOU HAD A KEY...",
                    "THAT SANDWICH IS PROBABLY MOLDY BY NOW ANYWAY"
                }, name = "LOCK BOX"}},
                {"locket", new ItemTuple {messages = new List<string>() { 
                    "IT'S FIRMLY CLOSED SHUT", 
                    "IF YOU HAD A KEY, YOU MIGHT BE ABLE TO PRY IT OPEN",
                    "AT THIS RATE, YOU'LL NEVER KNOW IF OLD MAN HAD A HOT WIFE"
                }, name = "LOCKET"}},
                {"lockofhair", new ItemTuple {messages = new List<string>() { 
                    "IT'S \"LOCKED\"",
                    "IT'S A PUN",
                    "WHAT DID YOU EXPECT IT TO DO?"
                }, name = "HAIR LOCK"}},
                {"lockpicks", new ItemTuple {messages = new List<string>() { 
                    "THEY'RE BROKEN", 
                    "STILL BROKEN..."
                }, name = "LOCK PICKS"}},
                {"padlock", new ItemTuple {messages = new List<string>() { 
                    "YOU NEED A KEY TO USE THIS",
                    "WHY DON'T YOU HAVE A KEY YET?",
                }, name = "PAD LOCK"}},
                {"rope", new ItemTuple {messages = new List<string>() { 
                    "IT'S LOCKED?!",
                    "THAT ROPE DEALER WAS AN ASSHOLE"
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

            ((SelectedItem)actors["selected"]).Set(
                items[((Selector)actors["selector"]).Slot].Texture,
                items[((Selector)actors["selector"]).Slot].Name);

            if (InputHandler.Instance.pressed("A")) {
                if (!actors.ContainsKey("description")) {
                    Register("description", new TypingMessage(spriteBatch, content,
                        items[((Selector)actors["selector"]).Slot].Info));
                }
            }

            if (InputHandler.Instance.pressed("B")) 
                if (actors.ContainsKey("description"))
                    actors.Remove("description");

            if (InputHandler.Instance.pressed("Start"))
                if (actors.ContainsKey("description"))
                    actors.Remove("description");
                else
                    GameState.Instance.Enter(new InventoryDown(spriteBatch, content, actors));

            base.Update(gameTime);
        }

        void select() {
            if (InputHandler.Instance.pressed("Left")) {
                if (((Selector)actors["selector"]).Slot > 0)
                    ((Selector)actors["selector"]).Slot--;
                else
                    ((Selector)actors["selector"]).Slot = items.Count - 1;
                moveEffect();
            }

            if (InputHandler.Instance.pressed("Right")) {
                if (((Selector)actors["selector"]).Slot < items.Count - 1)
                    ((Selector)actors["selector"]).Slot++;
                else
                    ((Selector)actors["selector"]).Slot = 0;
                moveEffect();
            }

            if (InputHandler.Instance.pressed("Up")) {
                if (((Selector)actors["selector"]).Slot - 4 >= 0) {
                    ((Selector)actors["selector"]).Slot -= 4;
                    moveEffect();
                }
            }

            if (InputHandler.Instance.pressed("Down")) {
                if (((Selector)actors["selector"]).Slot + 4 < items.Count) {
                    ((Selector)actors["selector"]).Slot += 4;
                    moveEffect();
                }
            }
        }

        void moveEffect() {
            if (actors.ContainsKey("description"))
                actors.Remove("description");
            SoundBank.Instance.play("select");
        }
    }

    //C# *really* needs native tuples (and a lot of other stuff)
    class ItemTuple
    {
        public List<string> messages;
        public string name;
    }
}
