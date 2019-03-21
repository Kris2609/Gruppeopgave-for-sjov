using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SevenDRL.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenDRL
{
    public class Map : GameState
    {
        private List<GameObject> gameObjects;
        private Texture2D sprite;
        static Map mapInstance;
        private SpriteBatch spriteBatch;
        private GameObject island;
        private GameObject island2;
        private GameObject island3;

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

        /// <summary>
        /// Singleton
        /// </summary>
        public static Map MapInstance
        {
            get
            {
                if (mapInstance == null)
                {
                    mapInstance = new Map();
                }
                return mapInstance;
            }
        }

        private Map()
        {
            gameObjects = new List<GameObject>();
            island = IslandFactory.IslandInstance.Create(true, new Vector2(50, 100), IslandClick);
            island2 = IslandFactory.IslandInstance.Create(false, new Vector2(50, 600), IslandClick);
            island3 = IslandFactory.IslandInstance.Create(true, new Vector2(1000, 300), IslandClick);
        }

        /// <summary>
        /// Load constructor
        /// </summary>
        public void LoadContent()
        {
            sprite = GameManager.ManagerInstance.Content.Load<Texture2D>("wasser");
        }

        /// <summary>
        /// Draw constructor
        /// </summary>
        /// <param name="spriteBatch">picture to draw</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, Vector2.Zero, Color.White);
            foreach (GameObject item in gameObjects)
            {
                item.Draw(spriteBatch);
            }
        }

        public void Enter()
        {
            gameObjects.Add(island);
            gameObjects.Add(island2);
            gameObjects.Add(island3);
            MapInstance.LoadContent();
            //MapInstance.Execute();
        }

        public void Execute(GameTime gameTime)
        {
            foreach (GameObject item in gameObjects)
            {
                item.Update(gameTime);
            }
        }

        public void Exit()
        {

        }        
        
        public void IslandClick(object sender, EventArgs e)
        {
            if (GameWorld.Instance.CurrentState == this)
            {
                Rectangle rec = new Rectangle(new Point((int)island.Transform.Position.X, (int)island.Transform.Position.Y), IslandFactory.IslandInstance.GetRenderer.SpriteRectangle.Size);
                Rectangle rec2 = new Rectangle(new Point((int)island2.Transform.Position.X, (int)island2.Transform.Position.Y), IslandFactory.IslandInstance.GetRenderer.SpriteRectangle.Size);
                Rectangle rec3 = new Rectangle(new Point((int)island3.Transform.Position.X, (int)island3.Transform.Position.Y), IslandFactory.IslandInstance.GetRenderer.SpriteRectangle.Size);

                if (rec.Contains(new Point(((Point)sender).X, ((Point)sender).Y)))
                {
                    GameWorld.Instance.ChangeState(BattleScene.Instance);
                }
                else if (rec2.Contains(new Point(((Point)sender).X, ((Point)sender).Y)))
                {
                    GameWorld.Instance.ChangeState(BattleScene.Instance);
                }
                else if (rec3.Contains(new Point(((Point)sender).X, ((Point)sender).Y)))
                {
                    GameWorld.Instance.ChangeState(BattleScene.Instance);
                }
            }                     
        }
    }
}
