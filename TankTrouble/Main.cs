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
        private bool winScreen;
        private Rectangle tankRect;
        private float tankRotation;

        private float passiveSpin;
        
        private Texture2D activeTexture;

        private string[] gamemodes;
        private int gamemodeIndex;

        private List<Rectangle> walls;

        private bool gameRunning;
        private int tankHeight;
        private int tankWidth;
        private int winner;
        private Balls testBall;
        private Texture2D purple;
        private Texture2D red;
        private Texture2D black;

        private int amountOfWins;
        
        private int menuX;
        private int menuY;
        private int size;
        private float newRoundDelay;

        //Wall grid 
        int wallXGrid;
        int wallYGrid;
        int wallThickness;
        int wallXGap;
        int wallYGap;

        Tank player1;
        Tank player2;

        Random rng;
        // keyboard states and
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
        }

        /// <summary>
        /// set values for the variables
        /// </summary>
        protected override void Initialize()
        {

            base.Initialize();

            #region
#if DEBUG
            _graphics.PreferredBackBufferWidth = 425;
            _graphics.PreferredBackBufferHeight = 1000;
            _graphics.ApplyChanges();
#else
			_graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
			_graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
			_graphics.ApplyChanges();
#endif
            #endregion
            gameRunning = false;
            winner = 0;
            walls = new List<Rectangle>();

            size = _graphics.PreferredBackBufferHeight;

            tankWidth = size*30/1000;
            tankHeight = size*38/1000;
            //tankRect = new Rectangle(100, 100, tankWidth, tankHeight);

            // Wall Grid
            wallThickness = size*10 / 1000;
            wallXGrid = size*(425-wallThickness) / 4000;
            wallYGrid = size*(900-wallThickness) / 10000;
            wallXGap = wallXGrid - wallThickness;
            wallYGap = wallYGrid - wallThickness;
            rng = new Random();

            //testBall = new Balls(200, 200, 15, 200f, 200f, black, 0);

            Globals.SpriteBatch = _spriteBatch;
            Globals.GraphicsDeviceManager = _graphics;
            Globals.ContentManager = Content;
            Globals.WindowWidth = _graphics.PreferredBackBufferWidth;
            Globals.WindowHeight = _graphics.PreferredBackBufferHeight;

            player1 = new Tank(size*100/1000, size*100/1000, 0, tankWidth, tankHeight, purple, false, size);
            player2 = new Tank(size*300/1000, size*800/1000, 3.14f, tankWidth, tankHeight, red, false, size);

            MainMenu();
            gamemodes = new string[4];
            gamemodes[0] = "Normal";
            gamemodes[1] = "One Shot";
            gamemodes[2] = "Infinite Ammo";
            gamemodes[3] = "Dizzy";

            passiveSpin = 0;
            Devcade.Input.Initialize();


        }

        /// <summary>
        /// Loads the content from the content folder
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);


            // test purple texture
            purple = new Texture2D(GraphicsDevice, 1, 1);
            purple.SetData(new Color[] { Color.Purple });

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

        /// <summary>
        /// Used for the logic parts of the code like calculating velocity
        /// and taking keyboard inputs
        /// </summary>
        /// <param name="gameTime">Amount of milliseconds since last update</param>
        protected override void Update(GameTime gameTime)
        {

            Globals.GameTime = gameTime;

            // close window if esc or devcade menu buttons pressed
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Keys.Escape)
                || (Devcade.Input.GetButton(1, Devcade.Input.ArcadeButtons.Menu)
                && Devcade.Input.GetButton(2, Devcade.Input.ArcadeButtons.Menu)))
                Exit();
            
            var kstate = Keyboard.GetState();
            currentKB = Keyboard.GetState();

            // Menu selection when game is not running
            if (!gameRunning)
            {

                if (previousKB.IsKeyUp(Keys.Down) && currentKB.IsKeyDown(Keys.Down)
                    || Devcade.Input.GetButtonDown(1, Devcade.Input.ArcadeButtons.StickDown))
                {
                    if (menuY == 1)
                    {
                        menuY++;
                    }
                }

                if (previousKB.IsKeyUp(Keys.Up) && currentKB.IsKeyDown(Keys.Up)
                    || Devcade.Input.GetButtonDown(1, Devcade.Input.ArcadeButtons.StickUp))
                {
                    if (menuY == 2)
                    {
                        menuY--;
                    }
                }

                if (previousKB.IsKeyUp(Keys.Left) && currentKB.IsKeyDown(Keys.Left)
                    || Devcade.Input.GetButtonDown(1, Devcade.Input.ArcadeButtons.StickLeft))
                {
                    if (menuY == 2 && amountOfWins > 0)
                    {
                        amountOfWins--;
                    }
                    else if (gamemodeIndex > 0)
                    {
                        gamemodeIndex--;
                    }
                }
                if (previousKB.IsKeyUp(Keys.Right) && currentKB.IsKeyDown(Keys.Right)
                    || Devcade.Input.GetButtonDown(1, Devcade.Input.ArcadeButtons.StickRight))
                {
                    if (menuY == 2 && amountOfWins < int.MaxValue)
                    {
                        amountOfWins++;
                    }
                    else if(gamemodeIndex<3)
                    {
                        gamemodeIndex++;
                    }
                }
                if (previousKB.IsKeyUp(Keys.C) && currentKB.IsKeyDown(Keys.C)
                    || Devcade.Input.GetButtonDown(2, Devcade.Input.ArcadeButtons.A1))
                {
                    SelectGamemode();
                }

                if (previousKB.IsKeyUp(Keys.V) && currentKB.IsKeyDown(Keys.V)
                    || (Devcade.Input.GetButton(1, Devcade.Input.ArcadeButtons.Menu)))
                {
                    MainMenu();
                }
            }
            // Player controls when game is running
            else if (gameRunning)
            {
                if (!winScreen)
                {
                    #region Player 2 Controls
                    // --- Player 2 Controls --- //
                    // Forward
                    if (kstate.IsKeyDown(Keys.W)
                        || Devcade.Input.GetButton(2, Devcade.Input.ArcadeButtons.StickUp))
                    {
                        player1.Velocity = size * 150 / 1000;
                    }
                    // Backwards
                    else if (kstate.IsKeyDown(Keys.S)
                        || Devcade.Input.GetButton(2, Devcade.Input.ArcadeButtons.StickDown))
                    {
                        player1.Velocity = size * -150 / 1000;
                    }
                    else
                    {
                        player1.Velocity = 0;
                    }
                    // Turn Left
                    if (kstate.IsKeyDown(Keys.A)
                        || Devcade.Input.GetButton(2, Devcade.Input.ArcadeButtons.StickLeft)
                        || Devcade.Input.GetButton(2, Devcade.Input.ArcadeButtons.A1))
                    {
                        player1.Rotation -= 0.06f;
                    }
                    // Turn Right
                    if (kstate.IsKeyDown(Keys.D)
                        || Devcade.Input.GetButton(2, Devcade.Input.ArcadeButtons.StickRight)
                        || Devcade.Input.GetButton(2, Devcade.Input.ArcadeButtons.A3))
                    {
                        player1.Rotation += 0.06f;
                    }
                    // Shoot
                    // Is there a better way of doing this? idk.
                    if (previousKB.IsKeyUp(Keys.C) && currentKB.IsKeyDown(Keys.C)
                        || Devcade.Input.GetButtonDown(2, Devcade.Input.ArcadeButtons.A2))
                    {
                        player1.Shoot();
                    }
                    // Ability
                    
                    #endregion
                    #region Player 1 Controls
                    // --- Player 1 Controls --- //
                    // Forward
                    if (kstate.IsKeyDown(Keys.Up)
                        || Devcade.Input.GetButton(1, Devcade.Input.ArcadeButtons.StickUp))
                    {
                        player2.Velocity = size * 150 / 1000;
                    }
                    // Backwards
                    else if (kstate.IsKeyDown(Keys.Down)
                        || Devcade.Input.GetButton(1, Devcade.Input.ArcadeButtons.StickDown))
                    {
                        player2.Velocity = size * -150 / 1000;
                    }
                    else
                    {
                        player2.Velocity = 0;
                    }
                    // Turn Left
                    if (kstate.IsKeyDown(Keys.Left)
                        || Devcade.Input.GetButton(1, Devcade.Input.ArcadeButtons.StickLeft)
                        || Devcade.Input.GetButton(1, Devcade.Input.ArcadeButtons.A1))
                    {
                        player2.Rotation -= 0.06f;
                    }
                    // Turn Right
                    if (kstate.IsKeyDown(Keys.Right)
                        || Devcade.Input.GetButton(1, Devcade.Input.ArcadeButtons.StickRight)
                        || Devcade.Input.GetButton(1, Devcade.Input.ArcadeButtons.A3))
                    {
                        player2.Rotation += 0.06f;
                    }
                    // Shoot
                    if (previousKB.IsKeyUp(Keys.RightShift) && currentKB.IsKeyDown(Keys.RightShift)
                        || Devcade.Input.GetButtonDown(1, Devcade.Input.ArcadeButtons.A2))
                    {
                        player2.Shoot();
                    }
                    // Ability
                    if (kstate.IsKeyDown(Keys.NumPad2))
                    {
                        // Not implemented RN
                    }
                    #endregion
                }

                #region Tank and Ball Collisions
                // Check if tank collides with wall 
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

                #endregion
                #region other game stuff
                //Update Tanks
                if (!winScreen)
                {
                    player1.Update();
                    player2.Update();
                }

                //when die start new round
                if (!player1.Alive || !player2.Alive && gameRunning)
                {
                    newRoundDelay -= Globals.GameTime.ElapsedGameTime.Milliseconds;
                    if (newRoundDelay <= 0 && !winScreen)
                    {
                        NewRound();
                    }
                }

                player1.Rotation -= passiveSpin;
                player2.Rotation -= passiveSpin;
                #endregion
                #region Win screen and restart

                if (player1.Deaths == amountOfWins || player2.Deaths == amountOfWins)
                {
                    if (amountOfWins != 0)
                    {
                        winScreen = true;
                    }
                }

                if (kstate.IsKeyDown(Keys.V)
                        || (Devcade.Input.GetButton(1, Devcade.Input.ArcadeButtons.Menu)))
                {
                    MainMenu();
                }
                #endregion
            }


            //update input stuff
            previousKB = currentKB;
            Devcade.Input.Update();
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

            if (!gameRunning)
            {
                #region draw menu

                // Title
                _spriteBatch.DrawString(_font,
                       $"TANKS",
                       new Vector2(size * 100 / 1000, size * 300 / 1000), Color.Black);

                // Gamemode
                _spriteBatch.DrawString(_font,
                       $"Select Game Mode",
                       new Vector2(size * 100 / 1000, size * 500 / 1000), Color.Black);

                if (menuY == 1)
                {
                    _spriteBatch.DrawString(_font,
                       $"< {gamemodes[gamemodeIndex]} >",
                       new Vector2(size * 100 / 1000, size * 600 / 1000), Color.DarkGoldenrod);
                   
                    if(amountOfWins> 0)
                    {
                        _spriteBatch.DrawString(_font,
                    $"< {amountOfWins} >",
                       new Vector2(size * 100 / 1000, size * 800 / 1000), Color.Black);
                    }
                    else
                    {
                        _spriteBatch.DrawString(_font,
                    $"< infinite >",
                       new Vector2(size * 100 / 1000, size * 800 / 1000), Color.Black);
                    }

                } else
                {
                    
                    _spriteBatch.DrawString(_font,
                       $"< {gamemodes[gamemodeIndex]} >",
                       new Vector2(size * 100 / 1000, size * 600 / 1000), Color.Black);

                    if (amountOfWins > 0)
                    {
                        _spriteBatch.DrawString(_font,
                    $"< {amountOfWins} >",
                       new Vector2(size * 100 / 1000, size * 800 / 1000), Color.DarkGoldenrod);
                    }
                    else
                    {
                        _spriteBatch.DrawString(_font,
                    $"< infinite >",
                       new Vector2(size * 100 / 1000, size * 800 / 1000), Color.DarkGoldenrod);
                    }
                }
                

                // AMount of wins
                _spriteBatch.DrawString(_font,
                       $"First To",
                       new Vector2(size * 100 / 1000, size * 700 / 1000), Color.Black);

                
                #endregion
            }
            if (gameRunning)
            {
                #region draw tanks and walls
                // draw tanks
                if (!winScreen)
                {
                    player1.Draw();
                    player2.Draw();
                }

                // draw walls
                if (!winScreen)
                {
                    for (int i = 0; i < walls.Count; i++)
                    {
                        _spriteBatch.Draw(activeTexture, walls[i], Color.White);
                    }
                }

                // Text
                _spriteBatch.DrawString(_font,
                       $"Purple: {player2.Deaths}",
                       new Vector2(size * 10 / 1000, size * 940 / 1000), Color.Purple);

                _spriteBatch.DrawString(_font,
                       $"Red: {player1.Deaths}",
                       new Vector2(size * 220 / 1000, size * 940 / 1000), Color.Red);
                #endregion
                if (winScreen)
                {
                    if (player1.Deaths > player2.Deaths)
                    {
                        _spriteBatch.DrawString(_font,
                       $"Player 2 Wins!",
                       new Vector2(size * 100 / 1000, size * 300 / 1000), Color.Red);
                    } else
                    {
                        _spriteBatch.DrawString(_font,
                       $"Player 1 Wins!",
                       new Vector2(size * 100 / 1000, size * 300 / 1000), Color.Purple);
                    }
                }
            }
            


            _spriteBatch.End();
            base.Draw(gameTime);
        }

        public void MainMenu()
        {
            gameRunning = false;
            menuX = 1;
            menuY= 1;
            amountOfWins = 5;
            gamemodeIndex = 0;
            player1.Deaths = 0;
            player2.Deaths = 0;
            winScreen = false;
        }

        public void SelectGamemode()
        {
            switch (gamemodeIndex)
            {
                case 0:
                    player1.MaxAmmo = 4;
                    player2.MaxAmmo = 4;
                    break;

                case 1:
                    player1.MaxAmmo = 1;
                    player2.MaxAmmo = 1;
                    break;

                case 2:
                    player1.MaxAmmo = 200;
                    player2.MaxAmmo = 200;
                    break;

                case 3:
                    player1.MaxAmmo = 4;
                    player2.MaxAmmo = 4;
                    passiveSpin = 0.08f;
                    break;
            }
            NewRound();
        }

        /// <summary>
        /// Creates a list full of rectangles containing the walls for the maps
        /// If a map outside of the list is selected it will draw a map with only border walls
        /// </summary>
        /// <param name="map"> The number of map to draw starting at 0</param>
        public void GenerateWalls(int map)
        {
            if (gameRunning)
            {
                
                walls.Clear();
                walls.Add(new Rectangle(wallThickness, 0, _graphics.PreferredBackBufferWidth, wallThickness));
                walls.Add(new Rectangle(0, 0, wallThickness, 10 * wallYGrid));
                walls.Add(new Rectangle(_graphics.PreferredBackBufferWidth - wallThickness, 0, wallThickness, 10 * wallYGrid));
                walls.Add(new Rectangle(0, 10 * wallYGrid, _graphics.PreferredBackBufferWidth, wallThickness));

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
                        walls.Add(new Rectangle(3 * wallXGrid + wallThickness, 9 * wallYGrid, wallXGrid * 2, wallThickness)); ;
                        break;

                    case 1:
                        walls.Add(new Rectangle(wallXGrid, wallYGrid, wallThickness, wallYGrid));
                        walls.Add(new Rectangle(3 * wallXGrid, wallYGrid, wallThickness, wallYGrid));
                        walls.Add(new Rectangle(wallThickness, 2 * wallYGrid, wallXGrid, wallThickness));
                        walls.Add(new Rectangle(3 * wallXGrid, 2 * wallYGrid, wallXGrid * 2, wallThickness));
                        walls.Add(new Rectangle(2 * wallXGrid, 2 * wallYGrid, wallThickness, wallYGrid));
                        walls.Add(new Rectangle(wallXGrid, 3 * wallYGrid, 2 * wallXGrid, wallThickness));
                        walls.Add(new Rectangle(wallXGrid, 3 * wallYGrid, wallThickness, wallYGrid));
                        walls.Add(new Rectangle(3 * wallXGrid, 3 * wallYGrid, wallThickness, wallYGrid));
                        walls.Add(new Rectangle(wallThickness, 5 * wallYGrid, wallXGrid, wallThickness));
                        walls.Add(new Rectangle(3 * wallXGrid, 5 * wallYGrid, wallXGrid * 2, wallThickness));
                        walls.Add(new Rectangle(wallXGrid, 6 * wallYGrid, wallThickness, wallYGrid));
                        walls.Add(new Rectangle(3 * wallXGrid, 6 * wallYGrid, wallThickness, wallYGrid));
                        walls.Add(new Rectangle(2 * wallXGrid, 7 * wallYGrid, wallThickness, wallYGrid));
                        walls.Add(new Rectangle(wallXGrid, 7 * wallYGrid, 2 * wallXGrid + wallThickness, wallThickness));
                        walls.Add(new Rectangle(wallThickness, 8 * wallYGrid, wallXGrid, wallThickness));
                        walls.Add(new Rectangle(3 * wallXGrid, 8 * wallYGrid, wallXGrid * 2, wallThickness));
                        walls.Add(new Rectangle(wallXGrid, 8 * wallYGrid, wallThickness, wallYGrid));
                        walls.Add(new Rectangle(3 * wallXGrid, 8 * wallYGrid, wallThickness, wallYGrid));
                        break;

                    case 2:
                        walls.Add(new Rectangle(2 * wallXGrid, wallThickness, wallThickness, wallYGrid));
                        walls.Add(new Rectangle(wallXGrid, wallYGrid, wallXGrid, wallThickness));
                        walls.Add(new Rectangle(3 * wallXGrid, wallYGrid, wallThickness, 2 * wallYGrid));
                        walls.Add(new Rectangle(wallThickness, 2 * wallYGrid, wallXGrid, wallThickness));
                        walls.Add(new Rectangle(2 * wallXGrid, 2 * wallYGrid, wallXGrid, wallThickness));
                        walls.Add(new Rectangle(wallXGrid, 3 * wallYGrid, 2 * wallXGrid + wallThickness, wallThickness));
                        walls.Add(new Rectangle(wallXGrid, 4 * wallYGrid, wallThickness, wallYGrid));
                        walls.Add(new Rectangle(wallXGrid + wallThickness, 4 * wallYGrid, wallXGrid - wallThickness, wallThickness));
                        walls.Add(new Rectangle(3 * wallXGrid, 4 * wallYGrid, wallThickness, wallYGrid));
                        walls.Add(new Rectangle(wallThickness, 5 * wallYGrid, wallXGrid, wallThickness));
                        walls.Add(new Rectangle(3 * wallXGrid, 5 * wallYGrid, wallXGrid * 2, wallThickness));
                        walls.Add(new Rectangle(wallXGrid, 6 * wallYGrid, wallThickness, wallYGrid));
                        walls.Add(new Rectangle(3 * wallXGrid, 6 * wallYGrid, wallThickness, wallYGrid));
                        walls.Add(new Rectangle(wallXGrid, 8 * wallYGrid, wallThickness, wallYGrid));
                        walls.Add(new Rectangle(3 * wallXGrid, 8 * wallYGrid, wallThickness, wallYGrid));

                        break;

                    default:
                        break;
                }
            }
        }

        public void NewRound()
        {
            gameRunning = true;
            if (player1.Alive && !player2.Alive)
            {
                player2.Deaths += 1;
            }
            if (player2.Alive && !player1.Alive)
            {
                player1.Deaths += 1;
            }
            newRoundDelay = 2000.0f;
            player1.Balls.Clear();
            GenerateWalls(rng.Next(0,3));
            player1.X = rng.Next(size*100/1000, size*350/1000);
            player1.Y = rng.Next(size*100/1000, size*800/1000);
            player2.Balls.Clear();
            player2.X = rng.Next(size*100/1000, size*350/1000);
            player2.Y = rng.Next(size*100/1000, size*800/1000);
            player1.Alive = true;
            player2.Alive = true;

        }
    }
}