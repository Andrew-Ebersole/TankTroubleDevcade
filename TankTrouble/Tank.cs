using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TankTrouble
{
    internal class Tank
    {
        // --- Fields --- //

        // position fields
        private double xPos;
        private double yPos;
        private float rotation;
        
        // tank's velocity
        private float velocity;

        // Rect is drawn hitbox is used for collisions
        private Rectangle rect;
        private Rectangle hitbox;
        private Rectangle cannon;

        // list of balls
        private List<Balls> balls;
        private int maxAmmo;

        // texture
        private Texture2D texture;

        // tank deaths
        private int deaths;
        private bool alive;

        private int size;

        // Rng
        Random rng;


        // --- Properties --- //
        
        /// <summary>
        /// public property for x position
        /// </summary>
        public double X 
        { 
            get 
            { 
                return xPos; 
            }
            set 
            {
                // if X value is within 0 and max height
                if (value >= 0 + hitbox.Width / 2 && value <= Globals.GraphicsDeviceManager.PreferredBackBufferWidth - hitbox.Width / 2)
                {
                    xPos = value;
                }
            }
        }
        /// <summary>
        /// public property for y position
        /// </summary>
        public double Y
        {
            get
            {
                return yPos;
            }
            set
            {

                // if y value is within 0 and max height
                if (value >= 0 + hitbox.Width / 2 && value <= Globals.GraphicsDeviceManager.PreferredBackBufferHeight - hitbox.Width / 2)
                {
                    yPos = value;
                }      
            }
        }
        /// <summary>
        /// public property for velocity
        /// </summary>
        public float Velocity { get { return velocity; } set { velocity = value; } }
        /// <summary>
        /// public property for rotation
        /// </summary>
        public float Rotation { get { return rotation; } set { rotation = value; } }
        /// <summary>
        /// public property for rectangle
        /// </summary>
        public Rectangle Rectangle { get { return rect; } }
        public List<Balls> Balls {  get { return balls; } }
        public int Deaths { get { return deaths; } set { deaths = value; } }
        /// <summary>
        /// Checks if the tank is alive or not
        /// </summary>
        public bool Alive { get { return alive; } set { alive = value; } }
        // --- Constructor --- //
        public int MaxAmmo { get { return maxAmmo; } set { maxAmmo = value; } }
        public Tank(int x, int y, float rotation, int width, int height, Texture2D texture, bool alive, int size)
        {
            // TODO
            // enter calculated positions to rectangle construtor

            this.xPos = x;
            this.yPos = y;
            this.rotation = rotation;

            rect = new Rectangle((int)X, (int)Y, width, height);
            hitbox = new Rectangle((int)X - height / 2, (int)Y - height / 2, height, height);
            cannon = new Rectangle((int)X, (int)Y*2, width / 3, (int)height*1);
            this.texture = texture;
            balls = new List<Balls>();
            deaths = 0;
            rng = new Random();
            this.alive = alive;
            this.size = size;
        }



        // --- Methods --- //

        /// <summary>
        /// Updates logic every frame
        /// </summary>
        public void Update()
        {
            UpdatePosition();

            // update balls
            for (int i = 0; i < balls.Count; i++)
            {
                balls[i].update();
            }
            for (int i = 0; i < balls.Count; i++)
            {
                if (balls[i].Life <= 0)
                {
                    balls.RemoveAt(i);
                }
            }

        }

        
        /// <summary>
        /// updates visuals every frame
        /// </summary>
        public void Draw()
        {
            if (alive)
            {
                // TODO
                Globals.SpriteBatch.Draw(texture, hitbox, null, Color.Black, 0, new Vector2(0, 0), SpriteEffects.None, 1);
                Globals.SpriteBatch.Draw(texture, Rectangle, null, Color.White, Rotation, new Vector2(0.5f, 0.5f), SpriteEffects.None, 1);
                Globals.SpriteBatch.Draw(texture, cannon, null, Color.White, Rotation, new Vector2(0.5f, 0f), SpriteEffects.None, 1);
            }

            // draw balls
            for (int i = 0; i < balls.Count; i++)
            {
                Globals.SpriteBatch.Draw(texture, balls[i].ball, Color.White);
            }
        }

        public void Shoot()
        {
            if (balls.Count < maxAmmo && alive)
            {
                balls.Add(new Balls(

                X + size*(int)(-5 + -15f * (Math.Sin(Rotation)))/1000,
                Y + size*(int)(-5 + 15f * (Math.Cos(Rotation)))/1000,
                size*8/1000,
                size*(int)(-300 * (Math.Sin(Rotation)))/1000,
                size*(int)(300 * (Math.Cos(Rotation)))/1000,
                texture,
                4.12f

            ));
            }
        }



        /// <summary>
        /// Used to check if the tank is intersecting a wall
        /// </summary>
        /// <param name="wall"> The rectangle to check if the tank has collided with </param>
        public void Intersect(Rectangle wall)
        {
            int wallXMid = wall.X + wall.Width / 2;
            int wallYMid = wall.Y + wall.Height / 2;

            // If the tanks hitbox and wall are colliding
            if (hitbox.Intersects(wall))
            {
                int xDistance;
                int yDistance;

                if (xPos > wallXMid)
                {
                    xDistance = (int)((xPos - hitbox.Width) - (wallXMid + wall.Width / 2));
                } else
                {
                    xDistance = (int)((xPos + hitbox.Width) - (wallXMid - wall.Width / 2));
                }
                if (yPos > wallYMid)
                {
                    yDistance = (int)((yPos - hitbox.Height) - (wallYMid + wall.Height / 2));
                }
                else
                {
                    yDistance = (int)((yPos + hitbox.Height) - (wallYMid - wall.Height / 2));
                }

                if (Math.Abs(xDistance) > Math.Abs(yDistance))
                {
                    // If the middle of the tank is lower than the middle of the wall
                    if (yPos > wallYMid)
                    {
                        yPos = wallYMid + wall.Height / 2 + hitbox.Height / 2;
                    }
                    // If the middle of the tank is above the middle of the wall
                    if (yPos < wallYMid)
                    {
                        yPos = wallYMid - wall.Height / 2 - hitbox.Height / 2;
                    }
                }
                else
                {
                    // If the middle of the tank is right of the middle of the wall
                    if (xPos > wallXMid)
                    {
                        xPos = wallXMid + wall.Width / 2 + hitbox.Width / 2;
                    }
                    // If the middle of the tank is left of the wall
                    if (xPos < wallXMid)
                    {
                        xPos = wallXMid - wall.Width / 2 - hitbox.Width / 2;
                    }
                }
            }

            for (int i = 0; i < balls.Count; i++)
            {
                Balls[i].Intersect(wall);
            }
        }


        public bool Hit(Rectangle ball)
        {
            if (hitbox.Intersects(ball))
            {
                // TODO Make it so tanks actually die and stuff
                xPos = -100;
                yPos = -100;
                alive = false;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Removes the ball from the list
        /// </summary>
        /// <param name="ballIndex"> the position in the list the ball is at </param>
        public void RemoveBall(int ballIndex)
        {
            balls[ballIndex].Life = 0;

        }


        /// <summary>
        /// Moves the X and Y coordinates based on the rotation of the tank
        /// </summary>
        public void UpdatePosition()
        {
            X += (Globals.DeltaTime) * -Velocity * (Math.Sin(Rotation));
            Y += (Globals.DeltaTime) * Velocity * (Math.Cos(Rotation));
            rect.X = (int)X;
            rect.Y = (int)Y;
            hitbox.X = (int)X - rect.Height / 2;
            hitbox.Y = (int)Y - rect.Height / 2;
            cannon.X = (int)X;
            cannon.Y = (int)Y;
        }


    }
}
