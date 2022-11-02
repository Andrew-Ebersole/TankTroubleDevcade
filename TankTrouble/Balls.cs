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
        // --- Fields --- //

        public Rectangle ball;
        private double x;
        private double y;
        private float xVelo;
        private float yVelo;
        private int size;
        private double life;
        private Texture2D texture;


        // --- Properties --- //
        public double X { get { return x; } set { x = value; } }
        public double Y { get { return y; } set { y = value; } }
        public double Life { get { return life; } set { life = value; } }
        public float XVelo { get { return xVelo; } set { xVelo = value; } }
        public float YVelo { get { return yVelo; } set { yVelo = value; } }



        // --- Constructor --- //
        public Balls(double x, double y, int size, float xVelo, float yVelo, Texture2D texture, double life)
        {
            this.xVelo = xVelo;
            this.yVelo = yVelo;
            this.x = x;
            this.y = y;
            this.size = size;
            ball = new Rectangle((int)X, (int)Y, size, size);

            this.texture = texture;
            this.life = life;
        }



        // --- Methods --- //
        public void update()
        {
            if (life > 0)
            {
                // Movement
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

                // Drain Life
                life -= Globals.DeltaTime;
                
                // Collision
                //if (player1.Hit())
                {

                }
            } else
            {
                // Remove Ball
                ball.X = -100;
                ball.Y = -100;
            }
            
        }

        public void Draw()
        {
            if (life > 0)
            {
                Globals.SpriteBatch.Draw(texture, ball, Color.White);
            }
        }
    }
}
