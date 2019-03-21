using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenDRL
{
    public class ActiveCardFactory
    {
        private static ActiveCardFactory instance;

        private Dictionary<string, SpriteRenderer> spriteRendererPrototypes;


        public static ActiveCardFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ActiveCardFactory();
                }

                return instance;
            }
        }

        private ActiveCardFactory()
        {
            spriteRendererPrototypes = new Dictionary<string, SpriteRenderer>();

            spriteRendererPrototypes.Add("card1", new SpriteRenderer("cards/card1"));
            spriteRendererPrototypes.Add("card2", new SpriteRenderer("cards/card2"));
            spriteRendererPrototypes.Add("card3", new SpriteRenderer("cards/card3"));
            spriteRendererPrototypes.Add("card4", new SpriteRenderer("cards/card4"));
            spriteRendererPrototypes.Add("card5", new SpriteRenderer("cards/card5"));
            spriteRendererPrototypes.Add("card6", new SpriteRenderer("cards/card6"));
            spriteRendererPrototypes.Add("card7", new SpriteRenderer("cards/card7"));
            spriteRendererPrototypes.Add("card8", new SpriteRenderer("cards/card8"));
            spriteRendererPrototypes.Add("card9", new SpriteRenderer("cards/card9"));

        }

        /// <summary>
        /// Used to create a new ActiveCard. The card values are set based on the card type
        /// </summary>
        /// <param name="type">ActiveCardType for this card</param>
        /// <param name="modifier">How much this modifies its appropriate weapon value</param>
        /// <param name="duration">How long this boost last if it was a boost card</param>
        /// <param name="amount">Amount of heal/damage for instant cards</param>
        /// <param name="spriteName">Name of the sprite for card</param>
        /// <param name="cardName">Name of this card</param>
        public GameObject Create(ActiveCardType type, float modifier, int duration, int amount, string spriteName, string cardName)
        {
            GameObject newCard = new GameObject();

            // Add the sprite renderer based on spriteName, or throw exception
            if (spriteRendererPrototypes.ContainsKey(spriteName))
            {
                newCard.AddComponent(spriteRendererPrototypes[spriteName].Clone());
            }
            else
            {
                throw new KeyNotFoundException("This cardName was not found!");
            }
            ActiveCard myCard = new ActiveCard(type, modifier, duration, amount, cardName);
            newCard.AddComponent(myCard);
            newCard.AddComponent(new Button(myCard.CardClick));

            return newCard;

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
