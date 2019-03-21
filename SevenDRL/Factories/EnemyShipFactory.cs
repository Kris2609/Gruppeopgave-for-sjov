using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenDRL
{
    public class EnemyShipFactory
    {
        private Dictionary<string, SpriteRenderer> spriteRendererPrototypes;
        private static EnemyShipFactory instance;

        /// <summary>
        /// Singleton instance of this factory
        /// </summary>
        public static EnemyShipFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EnemyShipFactory();
                }

                return instance;
            }
        }

        /// <summary>
        /// Private constructor for this singleton
        /// </summary>
        private EnemyShipFactory()
        {
            spriteRendererPrototypes = new Dictionary<string, SpriteRenderer>();

            spriteRendererPrototypes.Add("enemy_one_wep", new SpriteRenderer("enemies/enemy_one_wep"));
            spriteRendererPrototypes.Add("enemy_two_wep", new SpriteRenderer("enemies/enemy_two_wep"));

            // Add each spriteRenderer here... . .. .. 
            // One for each image
        }


        /// <summary>
        /// Creates a new enemy ship
        /// </summary>
        /// <param name="health">The health of this ship</param>
        /// <param name="spriteName">Name of the sprite to use for this card</param>
        /// <returns>a newly created enemy ship</returns>
        public GameObject Create(int health, string spriteName)
        {
            GameObject newEnemy = new GameObject(new Vector2(800, 0));
            newEnemy.AddComponent(new Ship(health));

            // Setup same weapons for all ships
            newEnemy.AddComponent(new Weapon(5, 800));
            newEnemy.AddComponent(new Weapon(7, 700));
            // Set player as target


            // Add the sprite renderer based on spriteName, or throw exception
            if (spriteRendererPrototypes.ContainsKey(spriteName))
            {
                newEnemy.AddComponent(spriteRendererPrototypes[spriteName].Clone());
            }
            else
            {
                throw new KeyNotFoundException("This cardName was not found!");
            }

            return newEnemy;
        }

        /// <summary>
        /// Used to Eager Load all of the sprites used for SpriteRenderer component
        /// </summary>
        public void EagerLoadContent()
        {
            foreach (SpriteRenderer renderer in spriteRendererPrototypes.Values)
            {
                renderer.LoadContent();
            }
        }
    }
}
