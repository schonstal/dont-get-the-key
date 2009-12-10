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
    class Item : Actor
    {
        List<String> messages;
        int msg = -1;
        string name;
        string file;

        public Item(SpriteBatch sb, ContentManager contentManager, String texture, 
            int slot, String name, List<String> messages)
            : base(sb, contentManager, new Vector2(56,-136), "item_" + texture, new Rectangle(0,0,0,0)) {
            if (slot < 4) {
                position.X += slot * 32;
            } else {
                position.Y += 32;
                position.X += (slot - 4) * 32;
            }
            this.messages = messages;
            this.name = name;
            file = texture;
        }

        public String Info {
            get {
                if (msg < messages.Count - 1)
                    msg++;
                return messages[msg];
            }
        }

        public String Texture {
            get { return file; }
        }

        public String Name {
            get { return name; }
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime) {
            spriteBatch.Draw(ImageBank.Instance.texture(sprite), position, color);
        }
    }
}
