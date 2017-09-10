﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AsteroidBeltAssault {
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        StarField starField;
        AsteroidManager asteroidManager;
        PlayerManager playerManager;
        EnemyManager enemyManager;
        ExplosionManager explosionManager;
        CollisionManager collisionManager;

        enum GameStates {
            TitleScreen, Playing, PlayerDeath, GameOver
        }
        GameStates gameState = GameStates.Playing;
        Texture2D titleScreen;
        Texture2D spriteSheet;
        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            titleScreen = Content.Load<Texture2D>("Textures//TitleScreen");
            spriteSheet = Content.Load<Texture2D>("Textures//SpriteSheet");
            starField = new StarField(this.Window.ClientBounds.Width, this.Window.ClientBounds.Height, 2000, new Vector2(0, 30f), spriteSheet, new Rectangle(0, 450, 2, 2));
            asteroidManager = new AsteroidManager(10, spriteSheet, new Rectangle(0, 0, 50, 50), 20, this.Window.ClientBounds.Width, this.Window.ClientBounds.Height);
            playerManager = new PlayerManager(spriteSheet, new Rectangle(0, 150, 50, 50), 3, new Rectangle(0, 0, this.Window.ClientBounds.Width, this.Window.ClientBounds.Height));
            enemyManager = new EnemyManager(spriteSheet, new Rectangle(0, 200, 50, 50), 6, playerManager, new Rectangle(0, 0, this.Window.ClientBounds.Width, this.Window.ClientBounds.Height));
            explosionManager = new ExplosionManager(spriteSheet, new Rectangle(0, 100, 50, 50), 3, new Rectangle(0, 450, 2, 2));
            collisionManager = new CollisionManager(asteroidManager, playerManager, enemyManager, explosionManager);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            switch (gameState) {
                case GameStates.TitleScreen:
                    break;
                case GameStates.Playing:
                    starField.Update(gameTime);
                    asteroidManager.Update(gameTime);
                    playerManager.Update(gameTime);
                    enemyManager.Update(gameTime);
                    explosionManager.Update(gameTime);
                    collisionManager.CheckCollisions();

                    break;
                case GameStates.PlayerDeath:
                    break;
                case GameStates.GameOver:
                    break;
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            if(gameState == GameStates.TitleScreen) {
                spriteBatch.Draw(titleScreen, new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height), Color.White);
            }
            if ((gameState == GameStates.Playing) ||
                (gameState == GameStates.PlayerDeath) ||
                (gameState == GameStates.GameOver)) {
                starField.Draw(spriteBatch);
                asteroidManager.Draw(spriteBatch);
                playerManager.Draw(spriteBatch);
                enemyManager.Draw(spriteBatch);
                explosionManager.Draw(spriteBatch);
            }
            if (gameState == GameStates.GameOver) {

            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
