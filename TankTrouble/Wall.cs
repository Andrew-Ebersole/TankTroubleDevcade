using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankTrouble
{
    internal class Wall
    {

        // Fields 

        private Rectangle rect;
        private int x;
        private int y;
        private int width;
        private int height;
        private bool isVertical;
        private Texture2D texture;


        // Properties

        public Rectangle Rect { get { return rect; } set { rect = value; } }
        public int X { get { return x; } set { x = value; } }
        public int Y { get { return y; } set { y = value; } }
        public bool IsVertical { get { return isVertical; } set { isVertical = value; } }
        public Texture2D Texture { get { return Texture; } set { Texture = value; } }


        // Constructor

        public Wall(int x, int y, int width, int height, bool isVertical, Texture2D texture)
        {

            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;

            rect = new Rectangle(x, y, width, height);

            this.isVertical = isVertical;
            this.texture = texture;
            
        }


        // Methods

            // TODO

            // Update()

            // Draw ()







    }
}
