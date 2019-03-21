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
    public class GameWorld
    {
        static GameWorld instance;
        private GameState currentState;
        private bool stateChanged;
        
        private GameObject playerShip;

        private Texture2D hpSprite;
        private Texture2D reloadSprite;

        public Texture2D HPSprite
        {
            get => hpSprite;
        }

        public Texture2D ReloadSprite
        {
            get => reloadSprite;
        }

        public GameObject PlayerShip
        {
            get => playerShip;
        }

        public GameState CurrentState
        {
            get => currentState;
        }

        public bool StateChanged
        {
            get => stateChanged;
        }

        /// <summary>
        /// Singleton
        /// </summary>
        public static GameWorld Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameWorld();
                }
                return instance;
            }
        }
        
        public void ChangeState(GameState newState)
        {
            if (currentState != null)
            {
                currentState.Exit();
            }

            currentState = newState;
            currentState.Enter();
            stateChanged = true;
        }

        private GameWorld()
        {
            stateChanged = false;
        }

        /// <summary>
        /// Load constructor
        /// </summary>
        public void LoadContent()
        {
            StartScreen.Instance.LoadContent();
            ButtonFactory.Instance.EagerLoadContent();
            CrewWeaponCardFactory.Instance.EagerLoadContent();
            ActiveCardFactory.Instance.EagerLoadContent();
            EnemyShipFactory.Instance.EagerLoadContent();
            IslandFactory.IslandInstance.EagerLoadContent();

            // Laver en midlertidig player manager
            PlayerManager.Instance.LoadContent();
            
            Map.MapInstance.LoadContent();

            // Load bar sprites
            hpSprite = GameManager.ManagerInstance.Content.Load<Texture2D>("hp_pixel");
            reloadSprite = GameManager.ManagerInstance.Content.Load<Texture2D>("reload_pixel");

            ChangeState(StartScreen.Instance);
        }

        /// <summary>
        /// Update constructor
        /// </summary>
        /// <param name="gameTime">time to update</param>
        public void Update(GameTime gameTime)
        {

            if (currentState != null)
            {
                currentState.Execute(gameTime);
            }

            stateChanged = false;
        }

        /// <summary>
        /// Draw constructor
        /// </summary>
        /// <param name="spriteBatch">picture to draw</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            currentState.Draw(spriteBatch);

        }
    }
}
