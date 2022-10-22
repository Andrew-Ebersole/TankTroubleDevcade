using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankTrouble
{
    internal class Balls
    {
        // Fields
        public Rectangle ball;
        double x;
        double y;
        float xVelo;
        float yVelo;
        int size;

        private Texture2D texture;

        // Properties
        double X { get { return x; } }
        double Y { get { return y; } }

        // Constructor
        public Balls(double x, double y, int size, float xVelo, float yVelo, Texture2D texture)
        {
            this.xVelo = xVelo;
            this.yVelo = yVelo;
            this.x = x;
            this.y = y;
            this.size = size;
            ball = new Rectangle((int)X, (int)Y, size, size);

            this.texture = texture;

        }

        // Methods
        public void update()
        {

            x += xVelo * Globals.DeltaTime;
            ball.X = (int)x;


            y += yVelo * Globals.DeltaTime;
            ball.Y = (int)y;


            // top and bottom wall
            if ( Y > Globals.WindowHeight - size || Y < 0 )
            {
                yVelo = yVelo * -1;
            }

            // left and right wall
            if ( X > Globals.WindowWidth - size || X < 0)
            {
                xVelo = xVelo * -1;
            }






        }

        public void Draw()
        {

            Globals.SpriteBatch.Draw(texture, ball, Color.White);

        }
    }
}
