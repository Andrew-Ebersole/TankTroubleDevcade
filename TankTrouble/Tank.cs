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
        private float rotation;

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
                if (value >= 0 && value < Globals.GraphicsDeviceManager.PreferredBackBufferWidth)
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
            


            rect = new Rectangle(x, y, width, height);



            this.texture = texture; 


        }


        // Methods




    }
}
