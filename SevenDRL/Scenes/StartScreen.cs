using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenDRL
{
    class StartScreen : GameState
    {
        static StartScreen instance;
        private Texture2D sprite;
        private Vector2 origin;
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

        public static StartScreen Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new StartScreen(new Vector2(0,0));
                }
                return instance;
            }
        }

        public void OECSC (object sender, EventArgs e)
        {
            if (GameWorld.Instance.CurrentState == this)
            {
                Rectangle mRec = new Rectangle(550, 450, 224, 61);

                if (mRec.Contains(new Point(((Point)sender).X, ((Point)sender).Y)))
                {
                    GameWorld.Instance.ChangeState(Map.MapInstance);
                }
            }            
        }   
        
        private StartScreen(Vector2 origin)
        {
            this.origin = origin;
            gameObjects = new List<GameObject>();
        }
        public void LoadContent()
        {
            sprite = GameManager.ManagerInstance.Content.Load<Texture2D>("Boaty");
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, origin, Color.White);
            foreach (GameObject navn in gameObjects)
            {
                navn.Draw(spriteBatch);
            }
        }
        public void Enter()
        {
            gameObjects.Add(ButtonFactory.Instance.Create(new Vector2(550, 450), OECSC, "start"));
        }
        public void Execute(GameTime gameTime)
        {
            
        }
        public void Exit()
        {
            
        }                
    }
}
