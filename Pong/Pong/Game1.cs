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

namespace Pong
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D GFX_Paddle;
        Texture2D GFX_Ball;
        MouseState MouseHandler;

        int PlayerPaddlePosition;
        int EnemyPaddlePosition;

        Point BallPosition = new Point(100,100);
        Point BallDirection = new Point(1, -1);
        int BallSpeed = 5;

        int MAX_WINDOW_HEIGHT;
        int MAX_WINDOW_WIDTH;

        Color BallColor = Color.White;
        int ColorCounter = 255;

        int playerScore = 0;

        
        SpriteFont TXT_scoreFont;
        Vector2 scorePos;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
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
            // TODO: Add your initialization logic here
            MAX_WINDOW_HEIGHT = GraphicsDevice.Viewport.Height;
            MAX_WINDOW_WIDTH = GraphicsDevice.Viewport.Width;
            scorePos = new Vector2 (MAX_WINDOW_WIDTH / 2, MAX_WINDOW_HEIGHT / 2);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            GFX_Ball = Content.Load<Texture2D>("ball");
            GFX_Paddle = Content.Load<Texture2D>("paddle");
            TXT_scoreFont = Content.Load<SpriteFont>("Times New Roman");

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            MouseHandler = Mouse.GetState();
            PlayerPaddlePosition = MouseHandler.Y;
            BallColor = new Color(ColorCounter, ColorCounter, ColorCounter);
            if (ColorCounter < 255)
            {
                ColorCounter += 15;
            }
            else if (ColorCounter > 255)
            {
                ColorCounter = 255;
            }

            if (PlayerPaddlePosition < 0)
            {
                PlayerPaddlePosition = 0;
            }
            else if (PlayerPaddlePosition > MAX_WINDOW_HEIGHT - 150)
            {
                PlayerPaddlePosition = MAX_WINDOW_HEIGHT- 150;
            }

            BallPosition.X += BallDirection.X * BallSpeed;
            BallPosition.Y += BallDirection.Y * BallSpeed;

            if (BallPosition.Y < 0)
            {
                BallDirection.Y = 1;
                ColorCounter = 0;

            }
            else if (BallPosition.Y > MAX_WINDOW_HEIGHT - 30)
            {
                BallDirection.Y = -1;
                ColorCounter = 0;
            }

            if (BallPosition.X < -50 || BallPosition.X + 30 > MAX_WINDOW_WIDTH)
            {
                BallDirection.X = 1;
                BallPosition.X = 100;
                BallPosition.Y = 100;
                BallSpeed = 5;
                playerScore = 0;
                
            }
            else if (BallPosition.X > MAX_WINDOW_WIDTH)
            {
                BallDirection.X = -1;
                ColorCounter = 0;
            }


            if (BallPosition.X > 50 && BallPosition.X < 50 + 25 && BallPosition.Y + 30 > PlayerPaddlePosition && BallPosition.Y < PlayerPaddlePosition + 150)
            {
                BallDirection.X = 1;
                ColorCounter = 0;
                BallSpeed += 1;
                playerScore += 1;

            }
            else if (BallPosition.X + 30 > MAX_WINDOW_WIDTH - 75 && BallPosition.X + 30 < MAX_WINDOW_WIDTH - 50 && BallPosition.Y + 30 > ((MAX_WINDOW_HEIGHT - 150) - PlayerPaddlePosition) && BallPosition.Y < ((MAX_WINDOW_HEIGHT - 150) - PlayerPaddlePosition + 150))
            {
                BallDirection.X = -1;
                ColorCounter = 0;
                BallSpeed += 1;
                playerScore += 1;
            }

            base.Update(gameTime);
            
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            Rectangle Source_Paddle = new Rectangle(0, 0, 25, 150);

            Rectangle Dest_Paddle = new Rectangle(50, PlayerPaddlePosition, 25, 150);
            Rectangle Enemy_Paddle = new Rectangle(MAX_WINDOW_WIDTH - 75, (MAX_WINDOW_HEIGHT - 150) - PlayerPaddlePosition, 25, 150);

            Rectangle Dest_Ball = new Rectangle(BallPosition.X, BallPosition.Y, 30, 30);


            Vector2 FontOrigin = TXT_scoreFont.MeasureString("score: " + playerScore.ToString()) / 2;
            
            spriteBatch.Draw(GFX_Paddle, Dest_Paddle, Source_Paddle, Color.White);
            spriteBatch.Draw(GFX_Ball, Dest_Ball, BallColor);
            spriteBatch.Draw(GFX_Paddle, Enemy_Paddle, Source_Paddle, Color.White);
        
            spriteBatch.DrawString(TXT_scoreFont, "score: " + playerScore.ToString(), scorePos, Color.White);


            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
