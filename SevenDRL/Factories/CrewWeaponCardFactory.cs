using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenDRL
{
    public class CrewWeaponCardFactory
    {
        private Dictionary<string, SpriteRenderer> spriteRendererPrototypes;
        private static CrewWeaponCardFactory instance;

        private Dictionary<string, float> damageModifiers;
        private Dictionary<string, float> reloadModifiers;

        /// <summary>
        /// Singleton instance of this factory
        /// </summary>
        public static CrewWeaponCardFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CrewWeaponCardFactory();
                }

                return instance;
            }
        }

        /// <summary>
        /// Private constructor for this singleton
        /// </summary>
        private CrewWeaponCardFactory()
        {
            spriteRendererPrototypes = new Dictionary<string, SpriteRenderer>();
            damageModifiers = new Dictionary<string, float>();
            reloadModifiers = new Dictionary<string, float>();

            spriteRendererPrototypes.Add("card1", new SpriteRenderer("cards/card1"));
            spriteRendererPrototypes.Add("card2", new SpriteRenderer("cards/card2"));
            spriteRendererPrototypes.Add("card3", new SpriteRenderer("cards/card3"));
            spriteRendererPrototypes.Add("card4", new SpriteRenderer("cards/card4"));
            spriteRendererPrototypes.Add("card5", new SpriteRenderer("cards/card5"));
            spriteRendererPrototypes.Add("card6", new SpriteRenderer("cards/card6"));
            spriteRendererPrototypes.Add("card7", new SpriteRenderer("cards/card7"));
            spriteRendererPrototypes.Add("card8", new SpriteRenderer("cards/card8"));
            spriteRendererPrototypes.Add("card9", new SpriteRenderer("cards/card9"));

            damageModifiers.Add("card1", 1.30f);
            damageModifiers.Add("card2", 1.10f);
            damageModifiers.Add("card3", -0.85f);
            damageModifiers.Add("card4", 1f);
            damageModifiers.Add("card5", 1.15f);
            damageModifiers.Add("card6", 0.50f);
            damageModifiers.Add("card7", 1.20f);
            damageModifiers.Add("card8", 1.05f);
            damageModifiers.Add("card9", 1.12f);

            reloadModifiers.Add("card1", 1.10f);
            reloadModifiers.Add("card2", 1.30f);
            reloadModifiers.Add("card3", 1.50f);
            reloadModifiers.Add("card4", 1.05f);
            reloadModifiers.Add("card5", -0.9f);
            reloadModifiers.Add("card6", 1.50f);
            reloadModifiers.Add("card7", 1.05f);
            reloadModifiers.Add("card8", 1.05f);
            reloadModifiers.Add("card9", 1.03f);

        }

        /// <summary>
        /// Creates a new CrewWeaponCard
        /// </summary>
        /// <param name="dmgModifier">The damage modifier for this card</param>
        /// <param name="rldModifier">The reload modifier for this card</param>
        /// <param name="energy">Amount of uses for this card</param>
        /// <param name="cardName">Name of this card</param>
        /// <param name="spriteName">Name of the sprite to use for this card</param>
        /// <returns>a newly created CrewWeaponCard</returns>
        public GameObject Create(string spriteName, string cardName)
        {
            float dmgModifier = 0f;
            float rldModifier = 0f;
            int tempEnergy = 2;

            GameObject newCard = new GameObject();

            // Add the sprite renderer based on spriteName, or throw exception
            if (spriteRendererPrototypes.ContainsKey(spriteName))
            {
                newCard.AddComponent(spriteRendererPrototypes[spriteName].Clone());
                dmgModifier = damageModifiers[spriteName];
                rldModifier = reloadModifiers[spriteName];
            }
            else
            {
                throw new KeyNotFoundException("This cardName was not found!");
            }
            newCard.AddComponent(new CrewWeaponCard(dmgModifier, rldModifier, tempEnergy, cardName));

            return newCard;
        }

        /// <summary>
        /// Used to Eager Load all of the sprites used for SpriteRenderer component
        /// </summary>
        public void EagerLoadContent()
        {
            foreach(SpriteRenderer renderer in spriteRendererPrototypes.Values)
            {
                renderer.LoadContent();
            }
        }
    }
}
