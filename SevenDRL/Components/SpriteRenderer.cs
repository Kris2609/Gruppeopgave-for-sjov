using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenDRL
{
    public class SpriteRenderer : Component
    {
        private Rectangle rectangle;
        private Texture2D sprite;
        private string spriteName;

        public Rectangle SpriteRectangle
        {
            get
            {
                return rectangle;
            }
            set
            {
                rectangle = value;
            }
            
        }

        /// <summary>
        /// Creates a new SpriteRenderer Component
        /// </summary>
        /// <param name="spriteName">The name of a texture to load</param>
        public SpriteRenderer (string spriteName)
        {
            this.spriteName = spriteName;
        }

        /// <summary>
        /// Called each Draw iteration within MonoGame
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.sprite, base.GameObject.Transform.Position, this.rectangle, Color.White);
        }

        public override void LoadContent()
        {
            base.LoadContent();

            this.sprite = GameManager.ManagerInstance.Content.Load<Texture2D>(spriteName);
            this.rectangle = new Rectangle(0, 0, sprite.Width, sprite.Height);
            // Opret rectangle
        }

        /// <summary>
        /// Clones this SpriteRenderer
        /// </summary>
        /// <returns>An identical SpriteRenderer</returns>
        public SpriteRenderer Clone()
        {
            return (SpriteRenderer)this.MemberwiseClone();
        }
    }
}
