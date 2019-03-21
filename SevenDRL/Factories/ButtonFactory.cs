using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenDRL
{
    public class ButtonFactory
    {
        private static ButtonFactory instance;
        private Dictionary<string, SpriteRenderer> spriteRendererPrototypes;

        public static ButtonFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ButtonFactory();
                }
                return instance;
            }
        }

        private ButtonFactory()
        {
            spriteRendererPrototypes = new Dictionary<string, SpriteRenderer>();

            spriteRendererPrototypes.Add("start", new SpriteRenderer("start"));
            spriteRendererPrototypes.Add("cr_accept", new SpriteRenderer("gui/btn_accept"));
            spriteRendererPrototypes.Add("cr_decline", new SpriteRenderer("gui/btn_decline"));
            spriteRendererPrototypes.Add("cr_continue", new SpriteRenderer("gui/btn_continue"));
            // Add sprites to dict here
        }


        /// <summary>
        /// Creates a new button to use in GUI
        /// </summary>
        /// <param name="position">Position where to place the button</param>
        /// <param name="onClickHandler">EventHandler that can be execute when clicked</param>
        /// <param name="spriteName">Name of sprite to load</param>
        /// <returns></returns>
        public GameObject Create(Vector2 position, EventHandler onClickHandler, string spriteName)
        {
            GameObject btn = new GameObject(position);
            btn.AddComponent(new Button(onClickHandler));

            // Add the sprite renderer based on spriteName, or throw exception
            if (spriteRendererPrototypes.ContainsKey(spriteName))
            {
                btn.AddComponent(spriteRendererPrototypes[spriteName].Clone());
            }
            else
            {
                throw new KeyNotFoundException("This cardName was not found!");
            }

            return btn;
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
