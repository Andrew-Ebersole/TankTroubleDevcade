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
        private double x;
        private double y;
        private float xVelo;
        private float yVelo;
        private int size;
        private bool active;

        private Texture2D texture;

        // Properties
        public double X { get { return x; } set { x = value; } }
        public double Y { get { return y; } set { y = value; } }
        public bool Active { get { return active; } set { active = value; } }
        public float XVelo { get { return xVelo; } set { xVelo = value; } }
        public float YVelo { get { return yVelo; } set { yVelo = value; } }

        // Constructor
        public Balls(double x, double y, int size, float xVelo, float yVelo, Texture2D texture, bool active)
        {
            this.xVelo = xVelo;
            this.yVelo = yVelo;
            this.x = x;
            this.y = y;
            this.size = size;
            ball = new Rectangle((int)X, (int)Y, size, size);

            this.texture = texture;
            this.active = active;
        }

        // Methods
        public void update()
        {
            if (active)
            {
                x += xVelo * Globals.DeltaTime;
                ball.X = (int)x;


                y += yVelo * Globals.DeltaTime;
                ball.Y = (int)y;

                // top and bottom wall
                if (Y > Globals.WindowHeight - size || Y < 0)
                {
                    yVelo = yVelo * -1;
                }

                // left and right wall
                if (X > Globals.WindowWidth - size || X < 0)
                {
                    xVelo = xVelo * -1;
                }
            }
        }

        public void Draw()
        {
            if (active)
            {
                Globals.SpriteBatch.Draw(texture, ball, Color.White);
            }
        }
    }
}
