using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenDRL
{
    public class CrewWeaponCard : Card
    {
        private float damageModifier;
        private float reloadModifier;
        private int energy;

        /// <summary>
        /// The amount this card modifies a weapons damage
        /// </summary>
        public float DamageModifier
        {
            get => damageModifier;
        }
        
        /// <summary>
        /// The amount this card modifies a weapons reload speed
        /// </summary>
        public float ReloadModifier
        {
            get => reloadModifier;
        }

        /// <summary>
        /// The energy / amount of uses for this card
        /// </summary>
        public int Energy
        {
            get => Energy;
        }

        /// <summary>
        /// Creates a new CrewWeapon Card
        /// </summary>
        /// <param name="dmgModifier">The damage modifier for this card</param>
        /// <param name="rldModifier">The reload modifier for this card</param>
        /// <param name="energy">Amount of uses for this card</param>
        /// <param name="name">Name of this card</param>
        public CrewWeaponCard(float dmgModifier, float rldModifier, int energy, string name) : base(name, CardType.CrewWeapon)
        {
            this.damageModifier = dmgModifier;
            this.reloadModifier = rldModifier;
            this.energy = energy;
        }

        /// <summary>
        /// Removes one point of energy on this card and checks if it has no energy left
        /// </summary>
        public void DeductEnergyPoint()
        {
            this.energy -= 1;

            if (this.energy <= 0)
            {
                // Handle destruction of this card gracefully here!
                // Er måske en metode på superklassen??
            }
        }
    }
}
