﻿using System;
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
    class InventoryUp : State
    {
        Inventory inventory;

        public InventoryUp(SpriteBatch sb, ContentManager contentManager, 
            Dictionary<string, Actor> actors, Inventory inventory)
            : base(sb, contentManager) {
            this.actors = actors;
            this.inventory = inventory;

            SoundBank.Instance.play("pause", 0.5f, 0, 0, false);
            foreach (KeyValuePair<string, Actor> kvp in actors)
                kvp.Value.Tween(new Vector2(kvp.Value.Position.X, kvp.Value.Position.Y + 184), 1.2);

            ((Character)actors["main"]).PlayerControlled = false;
            ((Character)actors["main"]).Walking = false;
        }

        public override void Update(GameTime gameTime) {
            if (actors["background"].Position.Y > -1)
                GameState.Instance.Enter(inventory);

            base.Update(gameTime);
        }
    }
}
