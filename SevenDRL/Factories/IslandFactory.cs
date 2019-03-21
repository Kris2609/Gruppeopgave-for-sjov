using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SevenDRL.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenDRL.Factories
{
    public class IslandFactory
    {
        private SpriteRenderer renderer;
        private static IslandFactory islandInstance;

        public SpriteRenderer GetRenderer
        {
            get
            {
                return renderer;
            }
            set
            {
                renderer = value;
            }
        }

        /// <summary>
        /// Singleton for IslandFactory
        /// </summary>
        public static IslandFactory IslandInstance
        {
            get
            {
                if (islandInstance == null)
                {
                    islandInstance = new IslandFactory();
                }
                return islandInstance;
            }
        }

        /// <summary>
        /// private constructor for the single ton
        /// </summary>
        private IslandFactory()
        {
            renderer = new SpriteRenderer("Islan");
        }

        /// <summary>
        /// Creates a Island
        /// </summary>
        /// <param name="enemy">bool value</param>
        /// <param name="sprite">picture of island</param>
        /// <param name="transform">position</param>
        /// <returns></returns>
        public GameObject Create(bool enemy, Vector2 vector2, EventHandler IslandClick)
        {
            GameObject newIsland = new GameObject(vector2);
            newIsland.AddComponent(new Island(enemy));
            newIsland.AddComponent(renderer.Clone());            
            newIsland.AddComponent(new Button(IslandClick));
            return newIsland;
        }               

        /// <summary>
        /// Used to Eager Load sprite
        /// </summary
        public void EagerLoadContent()
        {
            renderer.LoadContent();
        }        
    }
}
