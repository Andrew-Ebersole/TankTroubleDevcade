// Tank Trouble
// Will and Andrew

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TankTrouble
{
    public class Game1 : Game
    {
        // variables yay!
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
            int size = 1080;
            _graphics.PreferredBackBufferHeight = size;
            _graphics.PreferredBackBufferWidth = 9*(size / 21 );
        }

        protected override void Initialize()
        {

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            var kstate = Keyboard.GetState();

            // Player 1 Controls --- //
            // Forward
            if (kstate.IsKeyDown(Keys.W))
            {

            }
            // Backwards
            if (kstate.IsKeyDown(Keys.S))
            {

            }
            // Turn Left
            if (kstate.IsKeyDown(Keys.A))
            {

            }
            // Turn Right
            if (kstate.IsKeyDown(Keys.D))
            {

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

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightGray);

            base.Draw(gameTime);
        }
    }
}