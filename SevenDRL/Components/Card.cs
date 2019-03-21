using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenDRL
{
    public enum CardType { CrewWeapon, ActiveCard }

    public abstract class Card : Component
    {
        private CardType type;
        private string name;

        /// <summary>
        /// The type of card
        /// </summary>
        public CardType Type
        {
            get => type;
        }

        /// <summary>
        /// The name of this card
        /// </summary>
        public string Name
        {
            get => name;
        }

        /// <summary>
        /// abstract constructor for the Card super class
        /// </summary>
        /// <param name="name">Name of this card</param>
        /// <param name="type">The type of card</param>
        public Card(string name, CardType type)
        {
            this.name = name;
            this.type = type;
        }

    }
}
