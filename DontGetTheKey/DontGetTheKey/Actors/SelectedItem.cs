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
    class SelectedItem : Actor
    {
        string item = "";
        string name = "";
        Vector2 textPos;


        public SelectedItem(SpriteBatch sb, ContentManager contentManager)
            : base(sb, contentManager, new Vector2(224,-140), "item_rope", new Rectangle(0,0,0,0)) {
            textPos = new Vector2(0, 0);
        }


        public override void Move(Vector2 amount)
        {
            position += amount;
            textPos += amount;
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            textPos.X = (position.X + 8) - (name.Length * 4);
            textPos.Y = position.Y + 22;
        }

        public override void Draw(GameTime gameTime) {
            if (item != "")
                spriteBatch.Draw(ImageBank.Instance.texture("item_" + item), position, color);
            spriteBatch.DrawString(ImageBank.Instance.font, name, textPos, color);
        }

        public void Set(string item, string name) {
            this.item = item;
            this.name = name;
        }
    }
}
