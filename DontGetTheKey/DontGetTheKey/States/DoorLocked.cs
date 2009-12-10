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
    class DoorLocked : State
    {
        public DoorLocked(SpriteBatch sb, ContentManager contentManager, 
            String obj, Dictionary<string, Actor> actors)
            : base(sb, contentManager) {
            this.actors = actors;
            SoundBank.Instance.play(obj.ToLower() + "_locked");
            Register("locked", new Message(sb, contentManager, "THIS " + obj + " IS LOCKED!"));
            Register("need_key", new Message(sb, contentManager, "YOU NEED A KEY TO OPEN IT."));
            actors["need_key"].Move(new Vector2(0, 8));
            ((Character)actors["main"]).Freeze();
        }

        public override void Update(GameTime gameTime) {
            if ((InputHandler.Instance.pressed("A") || InputHandler.Instance.pressed("Start"))) {
                actors.Remove("locked");
                actors.Remove("need_key");
                ((Character)actors["main"]).PlayerControlled = true;
                GameState.Instance.Previous();
            }
            base.Update(gameTime);
        }
    }
}
