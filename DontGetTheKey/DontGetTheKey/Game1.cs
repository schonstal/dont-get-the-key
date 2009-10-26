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

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 320;
            graphics.PreferredBackBufferHeight = 240;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            GameState.Instance.Enter(new Intro());
            //GameState.Instance.Current.Register(new Actor(s, spriteBatch, Content));
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            GameState.Instance.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            spriteBatch.End();
            base.Draw(gameTime);
        }

        /*
        SpriteFont font;
        GameState state;
        GamePadState prev;
        Thread loading;

        //THE MOST IMPORTANT VARIABLE
        int numKeys;
        bool walking;
        bool isDead;

        PlayerIndex p1;
        //PlayerIndex p2;

        int characterState;
        int charFrame;

        bool started;
        bool alreadyPlayedTheFuckingMotherFuckingSound;
        
        string message;

        Texture2D character;
        Texture2D key;
        Texture2D background;
        Texture2D title;
        Texture2D keyShadow;
        Texture2D door;
        Texture2D lives;
        Texture2D chest;
        Texture2D tombstone;

        //God damn what the fuck man
        Rectangle doorPos;
        Rectangle doorSrc;
        Rectangle charPos;
        Rectangle keyPos;
        Rectangle keyHit;
        Rectangle bgPos;
        Rectangle bgSrc;

        Rectangle chestBoxNorth;
        Rectangle chestBoxEast;
        Rectangle chestBoxSouth;
        Rectangle chestBox;

        SoundEffect walk1;
        SoundEffect walk2;
        SoundEffect locked;
        SoundEffect pickup;
        SoundEffect close;
        SoundEffect hit;
        SoundEffect failure;
        SoundEffect start;
        SoundEffect intro;

        SoundEffect main;
        SoundEffect fast;
        SoundEffect music;
        SoundEffect menu;

        SoundEffect pause;
        SoundEffect chestEffect;
        SoundEffect congrats;

        //Fuck you, you fucking asshole
        Color bgColor;
        Color textColor;
        Color[] colors = new Color[] { Color.Aqua, Color.GreenYellow, Color.Red, Color.Violet };
        Color[] textColors = new Color[] { Color.Red, Color.Violet, Color.Aqua, Color.GreenYellow };

        string[] reasons = {"STARVATION", "DYSENTERY", "DEHYDRATION", "OLD AGE", "A HEART ATTACK", "DIABETES", "AIDS", "POLIO", "POISON", "OBESITY", "SUFFOCATION", "SEIZURE"};

        //one shot
        static SoundEffectInstance pickupInstance;
        static SoundEffectInstance failureInstance;
        static SoundEffectInstance startInstance;
        static SoundEffectInstance chestInstance;
        static SoundEffectInstance congratsInstance;
        
        //sound effect
        static SoundEffectInstance pauseInstance;
        static SoundEffectInstance hitInstance;
        static SoundEffectInstance lockedInstance;
        static SoundEffectInstance walk1Instance;
        static SoundEffectInstance walk2Instance;

        //music
        static SoundEffectInstance mainInstance;
        static SoundEffectInstance menuInstance;
        static SoundEffectInstance musicInstance;
        static SoundEffectInstance introInstance;

        static SoundEffectInstance closeInstance;

        int blackBarSize;
        int pressFlash;
        int walkTimer;
        int keyTimer;
        int scrollTimer;
        int bounceCount;
        int titleScreenDelay;
        int remainingTime;
        int bgTimer;

        enum GameState { titleScreen, paused, starting, inGame, timeUp, gotKey, message, preGame, over, dead }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 320;
            graphics.PreferredBackBufferHeight = 240;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Init();

            base.Initialize();
        }

        void Init()
        {
            isDead = false;
            blackBarSize = 56;
            bgPos = new Rectangle(32, blackBarSize, 256, 184);
            bgSrc = new Rectangle(-364, 0, 256, 184);
            numKeys = 0;
            characterState = 0;
            charFrame = 0;
            walking = true;
            charPos = new Rectangle(152, 136, 16, 16);
            keyPos = new Rectangle(152, 130, 16, 16);
            doorSrc = new Rectangle(0, 0, 16, 32);
            doorPos = new Rectangle(-32, -32, 16, 32);
            pressFlash = 0;
            walkTimer = 0;
            keyTimer = 0;

            remainingTime = 1859;

            scrollTimer = -80;
            bounceCount = 15;
            state = GameState.preGame;
            titleScreenDelay = 40;
            started = false;
            bgColor = Color.White;
            bgTimer = 0;
            alreadyPlayedTheFuckingMotherFuckingSound = false;

            chestBox = new Rectangle(240, blackBarSize + 80, 16, 16);
            chestBoxEast = new Rectangle(224, blackBarSize + 80, 16, 16);
            chestBoxNorth = new Rectangle(242, blackBarSize + 64, 14, 16);
            chestBoxSouth = new Rectangle(242, blackBarSize + 96, 14, 16);
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //This stuff doesn't load properly in the loading thread.
            message = "Loading Font";
            font = Content.Load<SpriteFont>("PressStart");

            //one shots
            message = "Loading Sound Effects (1)";
            pickup = Content.Load<SoundEffect>("pickup_key");
            close = Content.Load<SoundEffect>("door_close");
            failure = Content.Load<SoundEffect>("game_over");
            start = Content.Load<SoundEffect>("start");
            menu = Content.Load<SoundEffect>("menu");
            congrats = Content.Load<SoundEffect>("congrats");

            //music
            message = "Loading Music";
            intro = Content.Load<SoundEffect>("titlemusic_intro");
            main = Content.Load<SoundEffect>("titlemusic_main");
            fast = Content.Load<SoundEffect>("bgmusic_fast");
            music = Content.Load<SoundEffect>("bgmusic");

            //sound effects
            message = "Loading Sound Effects (2)";
            pause = Content.Load<SoundEffect>("pause");
            hit = Content.Load<SoundEffect>("hit_wall");
            locked = Content.Load<SoundEffect>("door_locked");
            walk1 = Content.Load<SoundEffect>("walk1");
            walk2 = Content.Load<SoundEffect>("walk2");
            chestEffect = Content.Load<SoundEffect>("chest_locked");


            loading = new Thread(new ThreadStart(LoadThread));
            loading.Start();

        }

        void LoadThread()
        {
#if XBOX
            loading.SetProcessorAffinity(new[] { 3 });
#endif
            Exiting += delegate { if (loading != null ) loading.Abort(); };

            message = "Loading Textures";
            character = Content.Load<Texture2D>("character");
            background = Content.Load<Texture2D>("background");
            door = Content.Load<Texture2D>("door");
            title = Content.Load<Texture2D>("title");
            keyShadow = Content.Load<Texture2D>("key_shadow");
            lives = Content.Load<Texture2D>("lives");
            chest = Content.Load<Texture2D>("chest");
            tombstone = Content.Load<Texture2D>("gravestone");
            key = Content.Load<Texture2D>("key");

            //Load in the sounds.
            message = "Loading Sound Effect Instances";
            loadInstance(ref walk1Instance, walk1, 0.5f);
            loadInstance(ref walk2Instance, walk2, 0.5f);
            loadInstance(ref pauseInstance, pause, 0.5f);
            loadInstance(ref hitInstance, hit, 1);
            loadInstance(ref lockedInstance, locked, 1);
            loadInstance(ref chestInstance, chestEffect, 0.5f);

            loadInstance(ref menuInstance, menu, 1);
            loadInstance(ref pickupInstance, pickup, 0.6f);
            loadInstance(ref failureInstance, failure, 1);
            loadInstance(ref introInstance, intro, 1);
            loadInstance(ref congratsInstance, congrats, 1, true);

            loading = null;
        }

        void loadInstance(ref SoundEffectInstance instance, SoundEffect sound, float vol)
        {
            loadInstance(ref instance, sound, vol, false);
        }

        void loadInstance(ref SoundEffectInstance instance, SoundEffect sound, float vol, bool loop)
        {
            instance = sound.Play(0, 0, 0, loop);
            instance.Stop();
            instance.Volume = vol;
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            //NP{E
        }

        protected override void Update(GameTime gameTime)
        {
            if (loading == null)
            {
                // Allows the game to exit
                if (GamePad.GetState(p1).Buttons.Back == ButtonState.Pressed)
                    this.Exit();

                if (state == GameState.inGame || state == GameState.message)
                    countDown();

                switch (state)
                {
                    case GameState.titleScreen:
                        titleScreen();
                        break;
                    case GameState.starting:
                        starting();
                        break;
                    case GameState.inGame:
                        inGame();
                        break;
                    case GameState.paused:
                        if (prev.Buttons.Start != ButtonState.Pressed && GamePad.GetState(p1).Buttons.Start == ButtonState.Pressed)
                        {
                            musicInstance.Resume();
                            state = GameState.inGame;
                        }
                        break;

                    case GameState.gotKey:
                        if (GamePad.GetState(p1).Buttons.A == ButtonState.Pressed)
                        {
                            failureInstance.Play();
                            state = GameState.over;
                        }
                        break;

                    case GameState.timeUp:
                        timeUp();
                        break;

                    case GameState.dead:
                        dead();
                        break;

                    case GameState.message:
                        if ((prev.Buttons.A != ButtonState.Pressed && GamePad.GetState(p1).Buttons.A == ButtonState.Pressed) ||
                            (prev.Buttons.B != ButtonState.Pressed && GamePad.GetState(p1).Buttons.B == ButtonState.Pressed) ||
                            (prev.Buttons.Start != ButtonState.Pressed && GamePad.GetState(p1).Buttons.Start == ButtonState.Pressed))
                            state = GameState.inGame;
                        break;

                    case GameState.preGame:
                        preGame();
                        break;

                    case GameState.over:
                        gameOver();
                        break;
                }

                prev = GamePad.GetState(p1);

                // TODO: Add your update logic here
                // SWEATERS
            }

            base.Update(gameTime);
        }

        void timeUp()
        {
            charFrame = 0;
            walking = false;
            bgTimer++;
            if (bgTimer < colors.Length * 9)
            {
                    bgColor = colors[bgTimer / 9];
                    textColor = textColors[bgTimer / 9];
            }
            else
            {
                bgTimer = 0;
            }

            if (bgTimer % 9 == 0)
            {
                if (characterState == 1)
                    characterState = 3;
                else if (characterState == 3)
                    characterState = 8;
                else if (characterState == 8)
                    characterState = 5;
                else
                    characterState = 1;
            }

            if ((prev.Buttons.A != ButtonState.Pressed && GamePad.GetState(p1).Buttons.A == ButtonState.Pressed) ||
                (prev.Buttons.B != ButtonState.Pressed && GamePad.GetState(p1).Buttons.B == ButtonState.Pressed) ||
                (prev.Buttons.Start != ButtonState.Pressed && GamePad.GetState(p1).Buttons.Start == ButtonState.Pressed))
            {
                isDead = true;
                message = "YOU HAVE DIED OF\n" + randomReason(); // "STARVATION.";
                state = GameState.dead;
            }
        }

        void dead()
        {
            congratsInstance.Stop();
            if ((prev.Buttons.A != ButtonState.Pressed && GamePad.GetState(p1).Buttons.A == ButtonState.Pressed) ||
                (prev.Buttons.B != ButtonState.Pressed && GamePad.GetState(p1).Buttons.B == ButtonState.Pressed) ||
                (prev.Buttons.Start != ButtonState.Pressed && GamePad.GetState(p1).Buttons.Start == ButtonState.Pressed))
            {
                failureInstance.Play();
                state = GameState.over;
            }
        }

        string randomReason()
        {
            Random rand = new Random();
            return reasons[rand.Next(0, reasons.Length)];
        }

        void preGame()
        {
            introInstance.Play();

            if (bgSrc.X < 0)
            {
                bgSrc.X++;
            }
            else
            {
                state = GameState.titleScreen;
                walking = false;
            }
        }

        void gameOver()
        {
            if (prev.Buttons.B != ButtonState.Pressed && GamePad.GetState(p1).Buttons.B == ButtonState.Pressed)
            {
                this.Exit();
            }
            if (prev.Buttons.A != ButtonState.Pressed && GamePad.GetState(p1).Buttons.A == ButtonState.Pressed)
            {
                restart();
            }
        }

        void restart()
        {
            Init();
            return;
        }

        void titleScreen()
        {
            if(introInstance.State == SoundState.Stopped && mainInstance != null)
            {
                for (PlayerIndex index = PlayerIndex.One; index <= PlayerIndex.Four; index++)
                {
                    if (GamePad.GetState(index).Buttons.Start == ButtonState.Pressed)
                    {
                        p1 = index;
                        startGame();
                    }
                }
            }
        }

        void startGame()
        {
            pressFlash = 0;
            mainInstance.Stop();
            startInstance = start.Play();
            state = GameState.starting;
        }

        void getKey()
        {
            musicInstance.Stop();
            pickupInstance.Play();
            state = GameState.gotKey;
            characterState = 6;
            walking = false;
            charFrame = 0;
            charPos.Y -= 8;
            keyPos.X = charPos.X;
            keyPos.Y = charPos.Y - 16;
            numKeys = 1;
        }

        void bounce(int direction)
        {
            if (bounceCount == 15)
            {
                switch (direction)
                {
                    case 0:
                        charPos.X += 3;
                        break;
                    case 1:
                        charPos.Y += 3;
                        break;
                    case 2:
                        charPos.Y -= 3;
                        break;
                    case 3:
                        charPos.X -= 3;
                        break;
                }
                bounceCount = 0;
                hitInstance.Play();
            }
            bounceCount++;
        }

        void countDown()
        {
            remainingTime--;

            if (!started && closeInstance.State == SoundState.Stopped)
            {
                musicInstance = music.Play(0.6f, 0f, 0f, true);
                started = true;
            }
            if (remainingTime == 360)
            {
                musicInstance.Stop();
                musicInstance = fast.Play(0.6f, 0f, 0f, true);
            }
            if (remainingTime == 59)
            {
                congratsInstance.Play();
                state = GameState.timeUp;
                walking = false;
                musicInstance.Stop();
            }
        }

        void inGame()
        {
            walking = false;
            if (prev.Buttons.Start != ButtonState.Pressed && GamePad.GetState(p1).Buttons.Start == ButtonState.Pressed)
            {
                musicInstance.Pause();
                pauseInstance.Play();
                state = GameState.paused;
            }

            if (prev.Buttons.A != ButtonState.Pressed && GamePad.GetState(p1).Buttons.A == ButtonState.Pressed
                && ((characterState == 0 && charPos.Intersects(chestBoxEast)) || 
                (characterState == 2 && charPos.Intersects(chestBoxSouth)) || 
                (characterState == 4 && charPos.Intersects(chestBoxNorth))))
            {
                message = "CHEST IS LOCKED.\nYOU NEED A KEY TO OPEN IT.";
                chestInstance.Play();
                state = GameState.message;
            }

            else if (GamePad.GetState(p1).DPad.Left == ButtonState.Pressed)
            {
                if (charPos.X == 65 && charPos.Y <= 154 && charPos.Y >= 122)
                {
                    lockedInstance.Play();
                    message = "DOOR IS LOCKED.\nYOU NEED A KEY TO EXIT.";
                    state = GameState.message;
                }
                else if (charPos.Intersects(keyPos))
                {
                    getKey();
                }
                else
                {
                    walking = true;
                    characterState = 7;
                    if (charPos.X > 65)
                        charPos.X--;
                    else
                        bounce(0);
                }
            }
            else if (GamePad.GetState(p1).DPad.Right == ButtonState.Pressed)
            {
                if (charPos.Intersects(keyPos))
                {
                    getKey();
                }
                else
                {
                    walking = true;
                    if (charPos.X < 240 && !(charPos.Intersects(chestBox) && charPos.Intersects(chestBoxEast)))
                        charPos.X++;
                    else
                        bounce(3);
                    characterState = 0;
                }
            }
            else if (GamePad.GetState(p1).DPad.Up == ButtonState.Pressed)
            {
                if (charPos.Intersects(keyPos))
                {
                    getKey();
                }
                else
                {
                    walking = true;
                    characterState = 2;
                    if (charPos.Y > 78 && !(charPos.Intersects(chestBox) && charPos.Intersects(chestBoxSouth)))
                        charPos.Y--;
                    else
                        bounce(1);
                }
            }
            else if (GamePad.GetState(p1).DPad.Down == ButtonState.Pressed)
            {
                if (charPos.Intersects(keyPos))
                {
                    getKey();
                }
                else
                {
                    walking = true;
                    characterState = 4;
                    if (charPos.Y < 184 && !(charPos.Intersects(chestBox) && charPos.Intersects(chestBoxNorth)))
                        charPos.Y++;
                    else
                        bounce(2);
                }
            }
        }

        void starting()
        {
            if (scrollTimer > 0)
            {
                if (scrollTimer <= 108)
                {
                    charPos.X++;
                    walking = true;
                }
                else if (scrollTimer > 108 && scrollTimer <= 140)
                {
                    walking = false;
                    charPos.X = -16;
                }
                else if (scrollTimer > 140 && scrollTimer <= 204)
                {
                    bgSrc.X += 4;
                    walking = false;
                }
                else if (scrollTimer == 205)
                {
                    charPos.X = 48;
                }
                else if (scrollTimer < 237)
                {
                    charPos.X++;
                    walking = true;
                }
                else if (scrollTimer < 246)
                {
                    doorPos = new Rectangle(48, 128, 16, 32);
                    walking = false;
                }
                else
                {
                    closeInstance = close.Play();
                    doorSrc.X += 16;
                    state = GameState.inGame;
                }
                
            }
            scrollTimer++;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            if (loading != null)
            {
                drawMessage();
            }
            else
            {
                if (state != GameState.starting && state != GameState.titleScreen && state != GameState.preGame)
                    drawStats();

                if (state != GameState.over && state != GameState.dead)
                    spriteBatch.Draw(background, bgPos, bgSrc, bgColor);

                if (state == GameState.titleScreen)
                    DrawTitleScreen();

                if (state == GameState.inGame || state == GameState.gotKey || state == GameState.paused || state == GameState.message || state == GameState.timeUp)
                {
                    DrawKey();
                    spriteBatch.Draw(chest, chestBox, bgColor);
                }

                if (state == GameState.inGame || state == GameState.starting || state == GameState.gotKey || state == GameState.paused || state == GameState.message || state == GameState.timeUp)
                    spriteBatch.Draw(door, doorPos, doorSrc, bgColor);

                DrawCharacter();

                if (state == GameState.timeUp)
                {
                    message = "CONGRATULATIONS!!";
                    drawMessage();
                }


                if (state == GameState.paused)
                {
                    message = "PAUSED";
                    drawMessage();
                }

                if (state == GameState.message || state == GameState.dead)
                    drawMessage();

                if (state == GameState.gotKey)
                {
                    message = "YOU GOT THE KEY!!";
                    drawMessage();
                }

                if (state == GameState.over)
                {
                    message = "GAME OVER";
                    drawMessage();
                }

                if (state == GameState.starting && startInstance.State == SoundState.Playing)
                {
                    flashingText("PUSH START BUTTON", new Vector2(92, 183), 2);
                }
            }

            spriteBatch.End();

            // TODO: Add your drawing code here
            //SHORE

            base.Draw(gameTime);
        }

        //Disgusting hack
        void drawMessage()
        {
            string[] drawables = message.Split('\n');
            int lines = 0;
            foreach (string str in drawables)
            {
                spriteBatch.DrawString(font, str, new Vector2(161 - (str.Length * 4), 93 + (8 * lines)), Color.Black);
                spriteBatch.DrawString(font, str, new Vector2(160 - (str.Length * 4), 92 + (8 * lines)), (state == GameState.timeUp ? textColor : Color.White));
                lines++;
            }
        }

        void drawStats()
        {
            spriteBatch.DrawString(font, "LEVEL 0-1", new Vector2(45, blackBarSize - 32), Color.White);

            spriteBatch.Draw(key, new Vector2(40, blackBarSize - 20), Color.White);
            spriteBatch.DrawString(font, "X" + numKeys.ToString(), new Vector2(53, blackBarSize-16),Color.White);

            spriteBatch.Draw(lives, new Vector2(72, blackBarSize - 20), Color.White);
            spriteBatch.DrawString(font, "X1", new Vector2(88, blackBarSize - 16), Color.White);

            spriteBatch.DrawString(font, "TIME", new Vector2(248, blackBarSize - 32), (remainingTime >= 360 || (remainingTime >= 120 && remainingTime % 15 == 0) || (remainingTime < 120 && remainingTime % 5 == 0) ? Color.White : Color.Red));
            spriteBatch.DrawString(font, (remainingTime / 60).ToString(), new Vector2((remainingTime >= 600 ? 264 : 272), blackBarSize - 24), (remainingTime >= 360 || (remainingTime >= 120 && remainingTime % 15 == 0) || (remainingTime < 120 && remainingTime % 5 == 0) ? Color.White : Color.Red));
        }

        void flashingText(string text, Vector2 pos, int interval)
        {
            pressFlash++;
            if (pressFlash < interval)
                spriteBatch.DrawString(font, text, pos, Color.White);
            if (pressFlash >= ((interval*2)-1))
                pressFlash = 0;
        }

        void DrawKey()
        {
            keyTimer++;
            if (state != GameState.gotKey)
            {
                if (keyTimer % 9 == 0)
                {
                    if (keyTimer <= 27)
                        keyPos.Y++;
                    else if (keyTimer <= 54)
                        keyPos.Y--;
                    else keyTimer = 0;
                }
                spriteBatch.Draw(keyShadow, new Vector2(152, 138), bgColor);
            }
            spriteBatch.Draw(key, keyPos, bgColor);
        }

        void DrawCharacter()
        {
            if (!isDead)
            {
                if (walking)
                {
                    if (walkTimer >= (remainingTime > 300 ? 15 : 7))
                    {
                        if (charFrame == 0)
                        {
                            charFrame = 1;
                            walk1Instance.Play();
                        }
                        else
                        {
                            charFrame = 0;
                            walk2Instance.Play();
                        }
                        walkTimer = 0;
                    }
                    walkTimer++;
                }
                else
                {
                    if (characterState == 0 || characterState == 7)
                        charFrame = 0;
                }
                spriteBatch.Draw(character, charPos, new Rectangle(16 * characterState + charFrame * 16, 0, 16, 16), Color.White);
            }
            else
                spriteBatch.Draw(tombstone, new Vector2(144, blackBarSize + 64), Color.White);
        }

        void DrawDoor()
        {
            spriteBatch.Draw(character, charPos, new Rectangle(16 * characterState + charFrame * 16, 0, 16, 16), Color.White);
        }

        void DrawTitleScreen()
        {
            if (introInstance.State == SoundState.Stopped && !alreadyPlayedTheFuckingMotherFuckingSound)
            {
                alreadyPlayedTheFuckingMotherFuckingSound = true;
                menuInstance.Play();
            }

            if (menuInstance != null)
            {
                if (alreadyPlayedTheFuckingMotherFuckingSound)
                {
                    if (mainInstance == null)
                        mainInstance = main.Play(0.8f, 0, 0, true);
                    else
                        mainInstance.Play();
                }

                if (titleScreenDelay <= 0)
                {
                    spriteBatch.Draw(title, new Vector2(32, 8), Color.White);

                    if (menuInstance.State == SoundState.Stopped)
                        if (pressFlash < 30)
                            spriteBatch.DrawString(font, "PUSH START BUTTON", new Vector2(92, 183), Color.White);
                    if (pressFlash < 60)
                        pressFlash++;
                    else
                        pressFlash = 0;

                    spriteBatch.DrawString(font, "©2009 SUPEREGO STUDIOS", new Vector2(32, 224), Color.White);
                }
            }
            titleScreenDelay--;
        }
        */
    }
}
