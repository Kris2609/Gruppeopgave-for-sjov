using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenDRL
{
    class PauseScreen : GameState
    {
        static PauseScreen instance;
        private List<GameObject> gameObjects;
        public List<GameObject> StateList
        {
            get
            {
                return gameObjects;
            }
            set
            {
                gameObjects = value;
            }
        }
        

        public static PauseScreen Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PauseScreen();
                }
                return instance;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
        private PauseScreen()
        {

        }
        public void Enter()
        {

        }
        public void Execute(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
        public void Exit()
        {

        }
    }
}
