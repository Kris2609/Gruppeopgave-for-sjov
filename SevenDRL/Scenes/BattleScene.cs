using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenDRL
{
    public class BattleScene : GameState
    {
        private List<GameObject> gameObjects;

        private static BattleScene instance;
        private GameObject enemy;

        private Ship playerShip;
        private Ship enemyShip;
        private Weapon playerWeapon;
        private List<Component> enemyWeapons;

        private List<GameObject> gameObjectsToRemove;

        public bool CardAlreadyPlayed
        {
            get
            {
                return (gameObjectsToRemove.Count > 0) ? true : false;
            }
        }

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

        public static BattleScene Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BattleScene();
                }
                return instance;
            }
        }

        private BattleScene()
        {
            gameObjects = new List<GameObject>();
            gameObjectsToRemove = new List<GameObject>();
        }


        public void HandleShipDeath(GameObject theShip)
        {
            playerWeapon.RemoveTarget();

            foreach (Weapon wep in enemyWeapons)
            {
                wep.RemoveTarget();
            }

            if (theShip == PlayerManager.Instance.PlayerShip)
            {
                Console.WriteLine("SPILLEREN ER DØD!");
                Console.WriteLine("GAME OVER?");
            }
            else
            {
                if (theShip == enemy)
                {
                    GameWorld.Instance.ChangeState(CardRewardScene.Instance);
                    Console.WriteLine("ENEMY ER DØD!");
                    Console.WriteLine("NÆSTE SCENE?");
                }
            }
        }

        public void Enter()
        {
            enemy = EnemyShipFactory.Instance.Create(100, "enemy_one_wep");

            enemyWeapons = enemy.GetComponents("Weapon");
            playerWeapon = (Weapon)PlayerManager.Instance.PlayerShip.GetComponent("Weapon");
            playerShip = (Ship)PlayerManager.Instance.PlayerShip.GetComponent("Ship");

            enemyShip = (Ship)enemy.GetComponent("Ship");

            playerWeapon.ChangeTarget(enemyShip);
            foreach (Weapon wep in enemyWeapons)
            {
                wep.ChangeTarget(playerShip);
            }

            gameObjects.Add(PlayerManager.Instance.PlayerShip);
            gameObjects.Add(enemy);
            gameObjects.AddRange(PlayerManager.Instance.PlayerCards);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (GameObject item in gameObjects)
            {
                item.Draw(spriteBatch);
            }


            // Draw health bars
            int playerStatusSpritestartPosX = 80;
            int enemyStatusSpritestartPosX = 800;

            for (int i = 0; i < playerShip.HealthPoints; i++)
            {
                spriteBatch.Draw(GameWorld.Instance.HPSprite, new Vector2(playerStatusSpritestartPosX + i, 40), Color.White);
            }

            for (int i = 0; i < enemyShip.HealthPoints; i++)
            {
                spriteBatch.Draw(GameWorld.Instance.HPSprite, new Vector2(enemyStatusSpritestartPosX + i, 40), Color.White);
            }

            for (int i = 0; i < playerWeapon.ReloadProgressPercent; i++)
            {
                spriteBatch.Draw(GameWorld.Instance.ReloadSprite, new Vector2(playerStatusSpritestartPosX + i, 80), Color.White);
            }

            for (int i = 0; i < enemyWeapons.Count; i++)
            {
                for (int j = 0; j < ((Weapon)enemyWeapons[i]).ReloadProgressPercent; j++)
                {
                    spriteBatch.Draw(GameWorld.Instance.ReloadSprite, new Vector2(enemyStatusSpritestartPosX + j, 80 + (40*i)), Color.White);
                }
            }

        }

        public void Execute(GameTime gameTime)
        {
            foreach (GameObject item in gameObjects)
            {
                item.Update(gameTime);
            }

            foreach (GameObject card in gameObjectsToRemove)
            {
                this.gameObjects.Remove(card);
            }
            gameObjectsToRemove.Clear();
        }

        public void Exit()
        {
            gameObjects = new List<GameObject>();
            // throw new NotImplementedException();
        }

        public void RemoveGameObject(GameObject obj)
        {
            gameObjectsToRemove.Add(obj);
        }
    }
}
