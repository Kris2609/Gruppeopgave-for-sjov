using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenDRL
{
    public class Ship : Component
    {
        private int healthPoints;
        private int maxHealthPoints;

        /// <summary>
        /// Health point for this ship
        /// </summary>
        public int HealthPoints
        {
            get => healthPoints;
        }

        /// <summary>
        /// Constructor for creating a new ship
        /// </summary>
        /// <param name="health">starting health point for this ship</param>
        public Ship (int health)
        {
            this.healthPoints = health;
            this.maxHealthPoints = health;
        }

        /// <summary>
        /// Deducts the health point of this ship and checks if ship should be destroyed
        /// </summary>
        /// <param name="amount">The amount of health to deduct</param>
        public virtual void DeductHealth(int amount)
        {
            this.healthPoints -= amount;

            if (this.healthPoints <= 0)
            {
                // Håndter det der med at dø her.. .. ..
                BattleScene.Instance.HandleShipDeath(this.GameObject);
            }
        }

        public virtual void AddHealth(int amount)
        {
            this.healthPoints += amount;

            if (this.healthPoints > this.maxHealthPoints)
            {
                this.healthPoints = this.maxHealthPoints;
            }
        }
    }
}
