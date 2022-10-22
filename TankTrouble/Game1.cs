// Tank Trouble
// Will Lyons and Andrew

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Security.Cryptography.X509Certificates;

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


        /// <summary>
        /// Only happens at the beginning
        /// </summary>
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
            int size = 1080;
            _graphics.PreferredBackBufferHeight = size;
            _graphics.PreferredBackBufferWidth = 9*(size / 21 );
        }

        /// <summary>
        /// Imma be real i have no clue why this is here
        /// </summary>
        protected override void Initialize()
        {

            base.Initialize();

            tankWidth = 80;
            tankHeight = 120;
            tankRect = new Rectangle(100, 100, tankWidth, tankHeight);

        }

        /// <summary>
        /// Loads the content from the content folder
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);


            texture = new Texture2D(GraphicsDevice, 1, 1);
            texture.SetData(new Color[] { Color.Blue });

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
                MoveTank(1);
            }
            // Backwards
            if (kstate.IsKeyDown(Keys.S))
            {
                MoveTank(-1);
            }
            // Turn Left
            if (kstate.IsKeyDown(Keys.A))
            {
                tankRotation -= 0.06f;
            }
            // Turn Right
            if (kstate.IsKeyDown(Keys.D))
            {
                tankRotation += 0.06f;
            }
            // Shoot
            if (kstate.IsKeyDown(Keys.C))
            {

            }
            // Ability
            if (kstate.IsKeyDown(Keys.V))
            {

            }
            // Move Function
            void MoveTank(int distance)
            {
                tankRect.X += -distance*(int)(10*Math.Sin(tankRotation));
                tankRect.Y += distance*(int)(10*Math.Cos(tankRotation));
            }


            // Player 2 Controls --- //
            // Forward
            if (kstate.IsKeyDown(Keys.Up))
            {

            }
            // Backwards
            if (kstate.IsKeyDown(Keys.Down))
            {

            }
            // Turn Left
            if (kstate.IsKeyDown(Keys.Left))
            {

            }
            // Turn Right
            if (kstate.IsKeyDown(Keys.Right))
            {

            }
            // Shoot
            if (kstate.IsKeyDown(Keys.NumPad1))
            {

            }
            // Ability
            if (kstate.IsKeyDown(Keys.NumPad2))
            {

            }
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


            _spriteBatch.Draw(texture, tankRect, null,  Color.White, tankRotation, new Vector2(0.5f ,0.5f), SpriteEffects.None, 1);


            _spriteBatch.End();
            

            base.Draw(gameTime);
        }
    }
}