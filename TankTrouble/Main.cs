﻿// Tank Trouble
// Will Lyons and Andrew

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace TankTrouble
{
    public class Game1 : Game
    {
        // variables yay!
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Rectangle tankRect;
        private float tankRotation;

        private int tankHeight;
        private int tankWidth;

        private Texture2D texture;
        private Texture2D texture2;

        Tank player1;
        Tank player2;

        /// <summary>
        /// Only happens at the beginning
        /// </summary>
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
            int size = 1000;
            _graphics.PreferredBackBufferHeight = size;
            _graphics.PreferredBackBufferWidth = 9*(size / 21 );
        }

        /// <summary>
        /// Imma be real i have no clue why this is here
        /// </summary>
        protected override void Initialize()
        {

            base.Initialize();

            tankWidth = 40;
            tankHeight = 60;
            tankRect = new Rectangle(100, 100, tankWidth, tankHeight);

            Globals.SpriteBatch = _spriteBatch;
            Globals.GraphicsDeviceManager = _graphics;
            Globals.ContentManager = Content;

            player1 = new Tank(100, 100, 0, tankWidth, tankHeight, texture);
            player2 = new Tank(300, 800, 0, tankWidth, tankHeight, texture2);
        }

        /// <summary>
        /// Loads the content from the content folder
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);


            // test blue texture
            texture = new Texture2D(GraphicsDevice, 1, 1);
            texture.SetData(new Color[] { Color.Blue });

            // test red texture
            texture2 = new Texture2D(GraphicsDevice, 1, 1);
            texture2.SetData(new Color[] { Color.Red });

        }

        /// <summary>
        /// Used for the logic parts of the code like calculating velocity
        /// and taking keyboard inputs
        /// </summary>
        /// <param name="gameTime">Amount of milliseconds since last update</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            var kstate = Keyboard.GetState();

            // Player 1 Controls --- //
            // Forward
            if (kstate.IsKeyDown(Keys.W))
            {
                player1.MoveTank(1);
            }
            // Backwards
            if (kstate.IsKeyDown(Keys.S))
            {
                player1.MoveTank(-1);
            }
            // Turn Left
            if (kstate.IsKeyDown(Keys.A))
            {
                player1.Rotation -= 0.06f;
            }
            // Turn Right
            if (kstate.IsKeyDown(Keys.D))
            {
                player1.Rotation += 0.06f;
            }
            // Shoot
            if (kstate.IsKeyDown(Keys.C))
            {

            }
            // Ability
            if (kstate.IsKeyDown(Keys.V))
            {


            }

            // Player 2 Controls --- //
            // Forward
            if (kstate.IsKeyDown(Keys.Up))
            {
                player2.MoveTank(1);
            }
            // Backwards
            if (kstate.IsKeyDown(Keys.Down))
            {
                player2.MoveTank(-1);
            }
            // Turn Left
            if (kstate.IsKeyDown(Keys.Left))
            {
                player2.Rotation -= 0.06f;
            }
            // Turn Right
            if (kstate.IsKeyDown(Keys.Right))
            {
                player2.Rotation += 0.06f;
            }
            // Shoot
            if (kstate.IsKeyDown(Keys.NumPad1))
            {

            }
            // Ability
            if (kstate.IsKeyDown(Keys.NumPad2))
            {

            }


            player1.Update();
            player2.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// Used for drawing the content on the screen
        /// </summary>
        /// <param name="gameTime">Amount of milliseconds since last update</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightGray);

            _spriteBatch.Begin();

            // draw tank
            //_spriteBatch.Draw(texture, tankRect, null,  Color.White, tankRotation, new Vector2(0.5f ,0.5f), SpriteEffects.None, 1);

            player1.Draw();
            player2.Draw();

            _spriteBatch.End();
            

            base.Draw(gameTime);
        }
    }
}