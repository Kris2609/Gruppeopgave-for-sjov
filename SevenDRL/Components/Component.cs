using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenDRL
{
    public abstract class Component
    {
        private GameObject gameObject;

        /// <summary>
        /// The GameObject that this component is attached to
        /// </summary>
        public GameObject GameObject
        {
            get => gameObject;
        }

        /// <summary>
        /// Method to declare which GameObject this component is attached to
        /// </summary>
        /// <param name="obj">The GameObject this component is attached to</param>
        public virtual void Attach(GameObject obj)
        {
            this.gameObject = obj;
        }

        /// <summary>
        /// Called as soon as a component is attached to a GameObject
        /// </summary>
        public virtual void Initialize()
        {

        }

        public virtual void LoadContent()
        {

        }

        /// <summary>
        /// Called each Update iteration within MonoGame
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {

        }

        /// <summary>
        /// Called each Draw iteration within MonoGame
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
