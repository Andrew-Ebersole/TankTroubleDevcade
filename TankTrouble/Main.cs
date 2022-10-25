// Tank Trouble
// Will Lyons and Andrew

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Threading.Tasks;

namespace TankTrouble
{
    public class Game1 : Game
    {
        // variables yay!
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Rectangle tankRect;
        private float tankRotation;


        private Texture2D activeTexture;

        private Rectangle wallRect;



        private int tankHeight;
        private int tankWidth;

        private Balls testBall;

        private Texture2D blue;
        private Texture2D red;
        private Texture2D black;

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
            tankHeight = 50;
            tankRect = new Rectangle(100, 100, tankWidth, tankHeight);

            wallRect = new Rectangle(150, 400, 30, 150);


            testBall = new Balls(200, 200, 15, 200f, 200f, black, false);


            Globals.SpriteBatch = _spriteBatch;
            Globals.GraphicsDeviceManager = _graphics;
            Globals.ContentManager = Content;
            Globals.WindowWidth = _graphics.PreferredBackBufferWidth;
            Globals.WindowHeight = _graphics.PreferredBackBufferHeight;


            player1 = new Tank(100, 100, 0, tankWidth, tankHeight, blue);
            player2 = new Tank(300, 800, 0, tankWidth, tankHeight, red);
        }

        /// <summary>
        /// Loads the content from the content folder
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);


            // test blue texture
            blue = new Texture2D(GraphicsDevice, 1, 1);
            blue.SetData(new Color[] { Color.Blue });

            // test red texture
            red = new Texture2D(GraphicsDevice, 1, 1);
            red.SetData(new Color[] { Color.Red });

            // test black texture
            black = new Texture2D(GraphicsDevice, 1, 1);
            black.SetData(new Color[] { Color.Black });


            activeTexture = black;

        }



        // if hit wall when positive velocity


        /// <summary>
        /// Used for the logic parts of the code like calculating velocity
        /// and taking keyboard inputs
        /// </summary>
        /// <param name="gameTime">Amount of milliseconds since last update</param>
        protected override void Update(GameTime gameTime)
        {

            Globals.GameTime = gameTime;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            var kstate = Keyboard.GetState();

            // Player 1 Controls --- //
            // Forward
            if (kstate.IsKeyDown(Keys.W))
            {
                player1.Velocity = 1;
            }
            // Backwards
            else if (kstate.IsKeyDown(Keys.S))
            {
                player1.Velocity = -1;
            } else
            {
                
                if (player1.Intersect(wallRect))
                {
                    player1.Velocity *= -2;
                }
                player1.Velocity = 0;
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
                //if (testBall.Active == false)
                {
                    testBall.X = player1.X + (int)(-5 + -45f * (Math.Sin(player1.Rotation)));
                    testBall.Y = player1.Y + (int)(-5 + 45f * (Math.Cos(player1.Rotation)));
                    testBall.XVelo = (int)(-200f*(Math.Sin(player1.Rotation))) ;
                    testBall.YVelo = (int)(200f*(Math.Cos(player1.Rotation))) ;
                    testBall.Active = true;
                }
                

            }
            // Ability
            if (kstate.IsKeyDown(Keys.V))
            {


            }

            // Player 2 Controls --- //
            // Forward
            if (kstate.IsKeyDown(Keys.Up))
            {
                player2.Velocity = 1;
            }
            // Backwards
            else if (kstate.IsKeyDown(Keys.Down))
            {
                player2.Velocity = -1;
            }
            else
            {
                if (player2.Intersect(wallRect))
                {
                    player2.Velocity *= -2;
                    player2.UpdatePosition();
                }
                player2.Velocity = 0;
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

            // Collision
            player1.Intersect(wallRect);
            player2.Intersect(wallRect);

            testBall.update(player1, player2);

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

            //_spriteBatch.Draw(black, ballRect, Color.White);

            testBall.Draw();

            _spriteBatch.Draw(activeTexture, wallRect, Color.White);

            _spriteBatch.End();
            

            base.Draw(gameTime);
        }
    }
}