using Microsoft.Xna.Framework;
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
        // Fields

        // position fields
        private int xPos;
        private int yPos;
        private double xDouble;
        private double yDouble;
        private float rotation;

        // tank's velocities
        private float xVelo;
        private float yVelo;

        private Rectangle rect;

        private int ammo;

        private Texture2D texture;



        // Properties
        
        /// <summary>
        /// public property for x position
        /// </summary>
        public int X { 

            get 
            { 
                return xPos; 
            }

            set 
            {

                // if X value is within 0 and max height
                if (value >= 0 && value <= Globals.GraphicsDeviceManager.PreferredBackBufferWidth)
                {
                    xPos = value;
                }

            }

        }
        
        /// <summary>
        /// public property for y position
        /// </summary>
        public int Y
        {

            get
            {
                return yPos;
            }

            set
            {

                // if y value is within 0 and max height
                if (value >= 0 && value <= Globals.GraphicsDeviceManager.PreferredBackBufferHeight)
                {
                    yPos = value;
                }
                   
            }

        }

        /// <summary>
        /// public property for X velocity
        /// </summary>
        public float XVelo { get { return xVelo; } set { xVelo = value; } }

        /// <summary>
        /// public property for Y veloctiy
        /// </summary>
        public float YVelo { get { return yVelo; } set { yVelo = value; } }

        /// <summary>
        /// public property for rotation
        /// </summary>
        public float Rotation { get { return rotation; } set { rotation = value; } }

        /// <summary>
        /// public property for rectangle
        /// </summary>
        public Rectangle Rectangle { get { return rect; } }



        // Constructor


        public Tank(int x, int y, float rotation, int width, int height, Texture2D texture)
        {

            // TODO
            // calculate starting rotation

            // enter calculated positions to rectangle construtor
            
            this.xPos = x;
            this.yPos = y;


            rect = new Rectangle(X, Y, width, height);



            this.texture = texture;

            xDouble = X;
            yDouble = Y;

        }


        // Methods
        public void MoveTank(int distance)
        {
            xDouble += -distance * (Math.Sin(Rotation));
            yDouble += distance * (Math.Cos(Rotation));
            X = (int)xDouble;
            Y = (int)yDouble;
        }



        public void Update()
        {
            //TODO



            rect.X = X;
            rect.Y = Y;



        }


        public void Draw()
        {
            // TODO

            Globals.SpriteBatch.Draw(texture, Rectangle, null, Color.White, Rotation, new Vector2(0.5f, 0.5f), SpriteEffects.None, 1);


        }



        public void Intersect(Rectangle wall)
        {
            if (rect.Intersects(wall))
            {

                MoveTank(-1);


            }
        }


    }
}
