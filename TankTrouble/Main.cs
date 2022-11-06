// Tank Trouble
// Will Lyons and Andrew

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TankTrouble
{
    public class Game1 : Game
    {
        // variables yay!
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;

        private Rectangle tankRect;
        private float tankRotation;

        
        private Texture2D activeTexture;

        private List<Rectangle> walls;


        private int tankHeight;
        private int tankWidth;

        private Balls testBall;

        private Texture2D blue;
        private Texture2D red;
        private Texture2D black;

        private int size;

        //Wall grid 
        int wallXGrid;
        int wallYGrid;
        int wallThickness;

        Tank player1;
        Tank player2;


        // keyboard states
        private KeyboardState currentKB;
        private KeyboardState previousKB;

        /// <summary>
        /// Only happens at the beginning
        /// </summary>
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
            size = 1000;
            _graphics.PreferredBackBufferHeight = size;
            _graphics.PreferredBackBufferWidth = 9*(size / 21 );
        }

        /// <summary>
        /// set values for the variables
        /// </summary>
        protected override void Initialize()
        {

            base.Initialize();

            tankWidth = 40;
            tankHeight = 50;
            tankRect = new Rectangle(100, 100, tankWidth, tankHeight);

            // Wall Grid
            wallThickness = 15;
            wallXGrid = 424 / 4;
            wallYGrid = 900 / 10;

            // Adding walls lmao
            GenerateWalls(0);


            testBall = new Balls(200, 200, 15, 200f, 200f, black, 0);

            Globals.SpriteBatch = _spriteBatch;
            Globals.GraphicsDeviceManager = _graphics;
            Globals.ContentManager = Content;
            Globals.WindowWidth = _graphics.PreferredBackBufferWidth;
            Globals.WindowHeight = _graphics.PreferredBackBufferHeight;

            Vector2 p1Spawn;
            p1Spawn.X = 60;
            p1Spawn.Y = 40;
            Vector2 p2Spawn;
            p2Spawn.X = 300;
            p2Spawn.Y = 800;

            player1 = new Tank(100, 100, 0, tankWidth, tankHeight, blue, p1Spawn);
            player2 = new Tank(300, 800, 3.14f, tankWidth, tankHeight, red, p2Spawn);
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

            // load font
            _font = Content.Load<SpriteFont>("File");

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

            // close window if esc is pressed
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            var kstate = Keyboard.GetState();

            currentKB = Keyboard.GetState();

            // --- Player 1 Controls --- //
            // Forward
            if (kstate.IsKeyDown(Keys.W))
            {
                player1.Velocity = 150;
            }
            // Backwards
            else if (kstate.IsKeyDown(Keys.S))
            {
                player1.Velocity = -150;
            } 
            else
            {
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
            if (previousKB.IsKeyUp(Keys.C) && currentKB.IsKeyDown(Keys.C))
            {
                player1.Shoot();
            }
            // Ability
            if (kstate.IsKeyDown(Keys.V))
            {
                // Not implemented RN
            }

            // --- Player 2 Controls --- //
            // Forward
            if (kstate.IsKeyDown(Keys.Up))
            {
                player2.Velocity = 150;
            }
            // Backwards
            else if (kstate.IsKeyDown(Keys.Down))
            {
                player2.Velocity = -150;
            }
            else
            {
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
            if (previousKB.IsKeyUp(Keys.RightShift) && currentKB.IsKeyDown(Keys.RightShift))
            {
                player2.Shoot();
            }
            // Ability
            if (kstate.IsKeyDown(Keys.NumPad2))
            {
                // Not implemented RN
            }

            // Colides with wall 
            for (int i = 0; i < walls.Count; i++)
            {
                player1.Intersect(walls[i]);
                player2.Intersect(walls[i]);
            }

            // Colides with ball
            for (int i = 0; i < player1.Balls.Count; i++)
            {
                if (player1.Balls[i].Life <= 4)
                {
                    if (player1.Hit(player1.Balls[i].ball))
                    {
                        player1.RemoveBall(i);
                    }
                    if (player2.Hit(player1.Balls[i].ball))
                    {
                        player1.RemoveBall(i);
                    }
                }
            }
            for (int i = 0; i < player2.Balls.Count; i++)
            {
                if (player2.Balls[i].Life <= 4)
                {
                    if (player1.Hit(player2.Balls[i].ball))
                    {
                        player2.RemoveBall(i);
                    }
                    if (player2.Hit(player2.Balls[i].ball))
                    {
                        player2.RemoveBall(i);
                    }
                }
            }

            //Update Tanks
            player1.Update();
            player2.Update();

            previousKB = currentKB;

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
            for (int i = 0; i < walls.Count; i++)
            {
                _spriteBatch.Draw(activeTexture, walls[i], Color.White);
            }

            // Text
            _spriteBatch.DrawString(_font,
                   $"Blue: {player2.Deaths}",
                   new Vector2(10, 940), Color.Blue);

            _spriteBatch.DrawString(_font,
                   $"Red: {player1.Deaths}",
                   new Vector2(220, 940), Color.Red);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public void GenerateWalls(int map)
        {
            walls = new List<Rectangle>();
            walls.Add(new Rectangle(wallThickness, 0, 4 * wallXGrid - wallThickness * 2, wallThickness));
            walls.Add(new Rectangle(0, 0, wallThickness, 10 * wallYGrid));
            walls.Add(new Rectangle(4 * wallXGrid - wallThickness, 0, wallThickness, 10 * wallYGrid));
            walls.Add(new Rectangle(0, 10 * wallYGrid, 4 * wallXGrid, wallThickness));

            switch (map)
            {
                case 0:
                    walls.Add(new Rectangle(0, 1 * wallYGrid, wallXGrid + wallThickness, wallThickness));
                    walls.Add(new Rectangle(2 * wallXGrid, 0, wallThickness, wallYGrid));
                    walls.Add(new Rectangle(2 * wallXGrid, wallYGrid, wallXGrid + wallThickness, wallThickness));
                    walls.Add(new Rectangle(wallXGrid + wallThickness, 2 * wallYGrid, wallXGrid, wallThickness));
                    walls.Add(new Rectangle(wallXGrid, 2 * wallYGrid, wallThickness, 3 * wallYGrid));
                    walls.Add(new Rectangle(3 * wallXGrid, 2 * wallYGrid, wallThickness, 3 * wallYGrid));
                    walls.Add(new Rectangle(2 * wallXGrid + wallThickness, 3 * wallYGrid, wallXGrid - wallThickness, wallThickness));
                    walls.Add(new Rectangle(2 * wallXGrid, 5 * wallYGrid, wallThickness, wallYGrid));
                    walls.Add(new Rectangle(wallXGrid + wallThickness, 4 * wallYGrid, wallXGrid, wallThickness));
                    walls.Add(new Rectangle(2 * wallXGrid, 6 * wallYGrid, wallXGrid + wallThickness, wallThickness));
                    walls.Add(new Rectangle(wallXGrid, 6 * wallYGrid, wallThickness, 2 * wallYGrid));
                    walls.Add(new Rectangle(wallXGrid + wallThickness, 9 * wallYGrid, wallXGrid, wallThickness));
                    walls.Add(new Rectangle(2 * wallXGrid, 7 * wallYGrid, wallThickness, 2 * wallYGrid));
                    walls.Add(new Rectangle(wallThickness, 8 * wallYGrid, wallXGrid, wallThickness));
                    walls.Add(new Rectangle(wallThickness + 2 * wallXGrid, 8 * wallYGrid, wallXGrid, wallThickness));
                    walls.Add(new Rectangle(3 * wallXGrid + wallThickness, 9 * wallYGrid, wallXGrid, wallThickness)); ;
                    break;
                default:
                    break;
            }
        }
    }
}