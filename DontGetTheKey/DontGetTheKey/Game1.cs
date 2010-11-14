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

using System.Threading;

namespace DontGetTheKey
{
    //WORST FUCKING GAME EVER YOU PIECE OF SHHIT
    public partial class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Matrix spriteScale;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 320;
            graphics.PreferredBackBufferHeight = 240;
            Content.RootDirectory = "Content";
            this.Window.AllowUserResizing = true;
            this.Window.ClientSizeChanged += new EventHandler<EventArgs>(this.Window_ClientSizeChanged);

            // Guide init
            //Components.Add(new GamerServicesComponent(this));

            // Uncomment to test trial mode
            //Guide.SimulateTrialMode = true; 

        }

        void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            float screenscalew = (float)graphics.GraphicsDevice.Viewport.Width / 320f;
            float screenscaleh = (float)graphics.GraphicsDevice.Viewport.Height / 320f;
            spriteScale = Matrix.CreateScale(screenscalew, screenscaleh, 1);

        }
        protected override void Initialize() {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // Get default scale
            float screenscalew = 1f;
            float screenscaleh = 1f;
            spriteScale = Matrix.CreateScale(screenscalew, screenscaleh, 1);
            GameState.Instance.Enter(new Intro(spriteBatch, Content));
            ImageBank.Instance.Content = Content;
            SoundBank.Instance.Content = Content;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            //A perfect opporunity to use Map :(
            SoundBank.Instance.load("pickup_key");
            SoundBank.Instance.load("door_close");
            SoundBank.Instance.load("game_over");
            SoundBank.Instance.load("start");
            SoundBank.Instance.load("menu");
            SoundBank.Instance.load("congrats");

            SoundBank.Instance.load("titlemusic_intro");
            SoundBank.Instance.load("titlemusic_main");
            SoundBank.Instance.load("bgmusic_fast");
            SoundBank.Instance.load("bgmusic");

            SoundBank.Instance.load("pause");
            SoundBank.Instance.load("hit_wall");
            SoundBank.Instance.load("door_locked");
            SoundBank.Instance.load("walk1");
            SoundBank.Instance.load("walk2");
            SoundBank.Instance.load("chest_locked");
            SoundBank.Instance.load("select");
            SoundBank.Instance.load("typing");
            SoundBank.Instance.load("nope");
            SoundBank.Instance.load("sad_pause");

            ImageBank.Instance.loadFont("PressStart");

            ImageBank.Instance.load("character");
            ImageBank.Instance.load("background");
            ImageBank.Instance.load("door");
            ImageBank.Instance.load("title");
            ImageBank.Instance.load("key_shadow");
            ImageBank.Instance.load("lives");
            ImageBank.Instance.load("chest");
            ImageBank.Instance.load("gravestone");
            ImageBank.Instance.load("key");
            ImageBank.Instance.load("blackbar");
            ImageBank.Instance.load("statcover");
            ImageBank.Instance.load("swipe");

            ImageBank.Instance.load("item_bikelock");
            ImageBank.Instance.load("item_binoculars");
            ImageBank.Instance.load("item_goldpouch");
            ImageBank.Instance.load("item_journal");
            ImageBank.Instance.load("item_keyrings");
            ImageBank.Instance.load("item_lockbox");
            ImageBank.Instance.load("item_locket");
            ImageBank.Instance.load("item_lockofhair");
            ImageBank.Instance.load("item_lockpicks");
            ImageBank.Instance.load("item_padlock");
            ImageBank.Instance.load("item_rope");

            ImageBank.Instance.load("selector");
        }

        protected override void Update(GameTime gameTime) {
            GameState.Instance.Update(gameTime);
            
            //Hack :)
            if (GameState.Instance.Terminate)
                this.Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);
            GameState.Instance.Draw(gameTime, spriteScale);
            //Needs to be on top of everything.
            spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, spriteScale);
            spriteBatch.Draw(ImageBank.Instance.texture("blackbar"), new Vector2(0, 0), Color.White);
            spriteBatch.Draw(ImageBank.Instance.texture("blackbar"), new Vector2(288, 0), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
