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
    class InGame : State
    {
        Inventory inventory;

        public InGame(SpriteBatch sb, ContentManager contentManager, 
            Dictionary<string, Actor> actors)
            : base(sb, contentManager) {
            this.actors = actors;
            ((Character)actors["main"]).PlayerControlled = true;
            actors.Remove("statcover");
            SoundBank.Instance.play("bgmusic", 0.8f, 0, 0, true);

            Register(
                "chest",
                new Actor(
                    sb,
                    contentManager,
                    new Vector2(240, 136),
                    "chest",
                    new Rectangle(0, 0, 16, 16)
                    )
                );

            Register(
                "stats",
                new Stats(
                    sb,
                    contentManager,
                    new Vector2(256, 40),
                    "stats",
                    new Rectangle(0, 0, 0, 0)
                    )
                );

            Register(
                "key_shadow",
                new Actor(
                    sb,
                    contentManager,
                    new Vector2(152, 136),
                    "key_shadow",
                    new Rectangle(0, 0, 0, 0)
                    )
                );

            Register(
                "key",
                new Key(
                    sb,
                    contentManager,
                    new Vector2(152, 128),
                    "key",
                    new Rectangle(6, 10, 6, 6)
                    )
                );

            Register("door_pad",
                new Pad(
                    sb,
                    contentManager,
                    actors["door"].Position,
                    new Rectangle(16, 12, 6, 6)
                    )
                );

            Register("chest_pad_R", 
                new Pad(
                    sb, 
                    contentManager, 
                    actors["chest"].Position,
                    new Rectangle(-16, 0, 3, 16)
                    )
                );

            Register("chest_pad_D",
                new Pad(
                    sb,
                    contentManager,
                    actors["chest"].Position,
                    new Rectangle(0, -6, 16, 6)
                    )
                );

            Register("chest_pad_U",
                new Pad(
                    sb,
                    contentManager,
                    actors["chest"].Position,
                    new Rectangle(0, 16, 16, 6)
                    )
                );

            inventory = new Inventory(spriteBatch, content, actors);

        }

        public override void Update(GameTime gameTime) {
            //Get the key
            if (GameState.Instance.Current.Collision("key", "main")) {
                actors["key"].Transpose(new Vector2(actors["main"].Position.X, actors["main"].Position.Y - 15));
                GameState.Instance.Enter(new GotKey(spriteBatch, content, actors));
            }
            
            //Go to inventory
            if(InputHandler.Instance.pressed("Start"))
                GameState.Instance.Enter(new InventoryUp(spriteBatch, content, actors, inventory));
            
            //Win the game
            if(((Stats)actors["stats"]).Remaining <= 0)
                GameState.Instance.Enter(new Congrats(spriteBatch, content, actors));

            //Try the door
            if (GameState.Instance.Current.Collision(actors["main"].HitBox, "door_pad") &&
                InputHandler.Instance.pressed("A") && (((Character)actors["main"]).Heading == "L")) {
                GameState.Instance.Enter(new DoorLocked(spriteBatch, content, "DOOR", actors));
            }

            //Try the chest
            if ((ChestPad("U") || ChestPad("D") || ChestPad("R")) && InputHandler.Instance.pressed("A")) {
                GameState.Instance.Enter(new DoorLocked(spriteBatch, content, "CHEST", actors));
            }

            //Switch to fast music
            if ((SoundBank.Instance.effect("bgmusic") != null) &&
                (SoundBank.Instance.effect("bgmusic").State == SoundState.Playing) &&
                ((Stats)actors["stats"]).Remaining <= 5000) {
                SoundBank.Instance.stop("bgmusic");
                SoundBank.Instance.play("bgmusic_fast", 0.8f, 0, 0, true);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime) {
            base.Draw(gameTime);
        }

        private bool ChestPad(String side) {
            return GameState.Instance.Current.Collision(actors["main"].HitBox, "chest_pad_" + side) && (((Character)actors["main"]).Heading == side);
        }
    }
}
