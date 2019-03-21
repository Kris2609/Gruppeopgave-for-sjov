using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenDRL
{
    public class Transform : Component
    {
        private Vector2 position;

        /// <summary>
        /// The Vector2 position of this Transform
        /// </summary>
        public Vector2 Position { get => position; }

        /// <summary>
        ///  Creates a new Transform component with a starting position
        /// </summary>
        /// <param name="startPos">The starting position</param>
        public Transform(Vector2 startPos)
        {
            this.position = startPos;
        }

        /// <summary>
        /// Adds the translation to this Transforms position
        /// </summary>
        /// <param name="translation"></param>
        public void Translate(Vector2 translation)
        {
            this.position += translation;
        }

        /// <summary>
        /// Sets the position of this Translate
        /// </summary>
        /// <param name="position">The new Vector2 position</param>
        public void SetPosition(Vector2 position)
        {
            this.position = position;
        }
    }
}
