﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
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

        // list of walls
        //TODO

        private int ammo;
        private Texture2D texture;
   
        // Used to keep track of player number
        


        // --- Properties --- //
        
        /// <summary>
        /// public property for x position
        /// </summary>
        public double X { 

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



        // --- Constructor --- //

        public Tank(int x, int y, float rotation, int width, int height, Texture2D texture)
        {
            // TODO
            // calculate starting rotation
            // enter calculated positions to rectangle construtor
            
            this.xPos = x;
            this.yPos = y;

            rect = new Rectangle((int)X, (int)Y, width, height);
            hitbox = new Rectangle((int)X - height / 2, (int)Y - height / 2, height, height);
            cannon = new Rectangle((int)X, (int)Y*2, width / 3, (int)height*1);
            this.texture = texture;
            balls = new List<Balls>();
        }



        // --- Methods --- //

        /// <summary>
        /// Updates logic every frame
        /// </summary>
        public void Update()
        {
            //TODO

            UpdatePosition();

            // change rectangle positions to new x and y

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
            // TODO
            Globals.SpriteBatch.Draw(texture, hitbox, null, Color.Black, 0, new Vector2(0, 0), SpriteEffects.None, 1);
            Globals.SpriteBatch.Draw(texture, Rectangle, null, Color.White, Rotation, new Vector2(0.5f, 0.5f), SpriteEffects.None, 1);
            Globals.SpriteBatch.Draw(texture, cannon, null, Color.White, Rotation, new Vector2(0.5f, 0f), SpriteEffects.None, 1);

            // draw balls
            for (int i = 0; i < balls.Count; i++)
            {
                Globals.SpriteBatch.Draw(texture, balls[i].ball, Color.White);
            }
        }

        public void Shoot()
        {
            if (balls.Count < 10)
            {
                balls.Add(new Balls(

                X + (int)(-5 + -30f * (Math.Sin(Rotation))),
                Y + (int)(-5 + 30f * (Math.Cos(Rotation))),
                12,
                (int)(-350 * (Math.Sin(Rotation))),
                (int)(350 * (Math.Cos(Rotation))),
                texture,
                10.08f

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
                xPos = 40;
                yPos = 40;
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
