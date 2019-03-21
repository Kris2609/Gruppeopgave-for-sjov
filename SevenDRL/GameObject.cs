using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenDRL
{
    public class GameObject
    {
        private List<Component> components = new List<Component>();
        private Transform transform;

        /// <summary>
        /// Transform for this GameObject
        /// </summary>
        public Transform Transform
        {
            get => transform;
        }

        /// <summary>
        /// Creates a new GameObject
        /// </summary>
        public GameObject()
        {
            this.transform = new Transform(Vector2.Zero);
            AddComponent(this.transform);
        }

        /// <summary>
        /// Creates a new GameObject with a starting position
        /// </summary>
        /// <param name="startPos">The position to instantiate Transform at</param>
        public GameObject (Vector2 startPos)
        {
            this.transform = new Transform(startPos);
            AddComponent(this.transform);
        }

        /// <summary>
        /// Adds a component to this GameObject and calls Initialize on the component
        /// </summary>
        /// <param name="component">The Component to add</param>
        public void AddComponent(Component component)
        {
            component.Attach(this);
            component.Initialize();
            components.Add(component);
        }

        /// <summary>
        /// Finds the first component that matches componentName as class name
        /// </summary>
        /// <param name="componentName">Name of the component to find</param>
        /// <returns>First component if found, null otherwise</returns>
        public Component GetComponent(string componentName)
        {
            return components.FirstOrDefault((component) =>
            {
                return (component.ToString() == "SevenDRL." + componentName);
            });
        }

        /// <summary>
        /// Finds all components that matches componentName as class name
        /// </summary>
        /// <param name="componentName">Name of the component to find</param>
        /// <returns>List of matching Components</returns>
        public List<Component> GetComponents(string componentName)
        {
            return components.FindAll((component) =>
            {
                return (component.ToString() == "SevenDRL." + componentName);
            });
        }


        public void LoadContent()
        {
            foreach (Component component in components)
            {
                component.LoadContent();
            }
        }

        /// <summary>
        /// Called each Update iteration within MonoGame
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            foreach (Component component in components)
            {
                component.Update(gameTime);
            }
        }

        /// <summary>
        /// Called each Draw iteration within MonoGame
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Component component in components)
            {
                component.Draw(spriteBatch);
            }
        }

    }
}
