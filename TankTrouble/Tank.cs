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
        private double xPos;
        private double yPos;
        private float rotation;

        // tank's velocities
        private float velocity;

        private Rectangle rect;

        private int ammo;

        private Texture2D texture;



        // Properties
        
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
                if (value >= 0 && value <= Globals.GraphicsDeviceManager.PreferredBackBufferWidth)
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
                if (value >= 0 && value <= Globals.GraphicsDeviceManager.PreferredBackBufferHeight)
                {
                    yPos = value;
                }
                   
            }

        }


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


            rect = new Rectangle((int)X, (int)Y, width, height);



            this.texture = texture;

            

        }


        // Methods
        


        public void Update()
        {
            //TODO



            rect.X = (int)X;
            rect.Y = (int)Y;



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

            }
        }

    }
}
