using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankTrouble
{
    public static class Globals
    {

        // Fields 

        private static SpriteBatch spriteBatch;

        private static GraphicsDeviceManager graphicsDeviceManager;

        private static ContentManager contentManager;

        private static GameTime gameTime;


        // Properties

        public static SpriteBatch SpriteBatch { get { return spriteBatch; } set { spriteBatch = value; } }

        public static GraphicsDeviceManager GraphicsDeviceManager { get { return graphicsDeviceManager; } set { graphicsDeviceManager = value; } }

        public static ContentManager ContentManager { get { return contentManager; } set { contentManager = value; } }

        public static GameTime GameTime { get { return gameTime; } set { gameTime = value; } }

        public static double DeltaTime { get { return GameTime.ElapsedGameTime.TotalSeconds; } }


    }

}




