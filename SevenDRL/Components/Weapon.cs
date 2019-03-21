using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SevenDRL
{
    public class Weapon : Component
    {
        private Thread threadedReloader;
        private Ship target;

        private int damage;
        private int reloadDelay;
        private int damageOriginal;
        private int reloadDelayOriginal;

        private int reloadProgress;

        private int pauseDelay = 100;

        private CrewWeaponCard currentCardAttached;

        private float activeDamageBoostModifier;
        private float activeSpeedBoostModifier;
        private int activeDamageBoostDuration;
        private int activeSpeedBoostDuration;
        private bool activeBoostEnabled;

        public bool ActiveBoostEnabled
        {
            get => activeBoostEnabled;
        }

        public int ReloadProgressPercent
        {
            get
            {
                return (int)(((float)reloadProgress / (float)reloadDelay) * 100f);
            }
        }

        public Weapon(int damage, int reloadDelay)
        {
            this.threadedReloader = new Thread(FiringDelegate);
            this.threadedReloader.IsBackground = true;

            this.reloadDelayOriginal = reloadDelay;
            this.damageOriginal = damage;

            this.activeBoostEnabled = false;
            this.activeDamageBoostModifier = 1f;
            this.activeSpeedBoostModifier = 1f;
            this.reloadProgress = 0;

            AdjustCardValues();

            threadedReloader.Start();
        }

        /// <summary>
        /// Delegate that handles weapon firing logic on a separate thread
        /// </summary>
        private void FiringDelegate()
        {
            while (true)
            {
                if (target != null /* && not paused here .. */)
                {
                    Thread.Sleep(1);

                    if (this.reloadProgress < this.reloadDelay)
                    {
                        this.reloadProgress += 1;
                    }
                    else
                    {
                        // Check if modifier is enabled
                        if (this.activeBoostEnabled)
                        {
                            if (this.activeSpeedBoostModifier != 1f)
                            {
                                this.activeSpeedBoostDuration -= reloadDelay;

                                if (this.activeSpeedBoostDuration < 0)
                                {
                                    this.activeSpeedBoostDuration = 0;
                                    this.activeSpeedBoostModifier = 1f;
                                    this.activeBoostEnabled = false;

                                    AdjustCardValues();
                                }
                            }
                            else if (this.activeDamageBoostModifier != 1f)
                            {
                                if (this.activeDamageBoostDuration < 0)
                                {
                                    this.activeDamageBoostDuration = 0;
                                    this.activeDamageBoostModifier = 1f;
                                    this.activeBoostEnabled = false;

                                    AdjustCardValues();
                                }
                            }
                        }

                        target.DeductHealth(damage);

                        this.reloadProgress = 0;
                    }
                    
                }
                else
                {
                    if (target == null)
                    {
                        this.activeSpeedBoostDuration = 0;
                        this.activeDamageBoostDuration = 0;
                        this.activeSpeedBoostModifier = 1f;
                        this.activeDamageBoostModifier = 1f;
                        this.activeBoostEnabled = false;

                        AdjustCardValues();
                    }

                    Thread.Sleep(pauseDelay);
                }
            }
        }

        /// <summary>
        /// Used to attach a card to this weapon
        /// </summary>
        /// <param name="card">The card to attach to this weapon</param>
        public void AddCard(CrewWeaponCard card)
        {
            if (currentCardAttached != null)
            {
                RemoveCard();
            }

            currentCardAttached = card;
            AdjustCardValues();
        }

        /// <summary>
        /// Used to remove a card from this weapon
        /// </summary>
        public void RemoveCard()
        {
            currentCardAttached = null;
        }

        /// <summary>
        /// Changes damage / reload speed depending on current card attached
        /// </summary>
        private void AdjustCardValues()
        {
            if (currentCardAttached == null)
            {
                this.damage = (int)(this.damageOriginal * activeDamageBoostModifier);
                this.reloadDelay = (int)(this.reloadDelayOriginal * activeSpeedBoostModifier);
            }
            else
            {
                this.damage = (int)(this.damageOriginal * currentCardAttached.DamageModifier * activeDamageBoostModifier);
                this.reloadDelay = (int)(this.reloadDelayOriginal * currentCardAttached.ReloadModifier * activeSpeedBoostModifier);
            }
        }

        /// <summary>
        /// Used to change which target to fire at
        /// </summary>
        /// <param name="newTarget">A ship to target with weapon firing effect</param>
        public void ChangeTarget(Ship newTarget)
        {
            this.target = newTarget;
        }

        /// <summary>
        /// Removes a target
        /// </summary>
        public void RemoveTarget()
        {
            this.target = null;
        }

        public void ApplyActiveDamageBoost (float modifier, int duration)
        {
            this.activeDamageBoostDuration = duration;
            this.activeDamageBoostModifier = modifier;
            this.activeBoostEnabled = true;
        }

        public void ApplyActiveSpeedBoost (float modifier, int duration)
        {
            this.activeSpeedBoostDuration = duration;
            this.activeSpeedBoostModifier = modifier;
            this.activeBoostEnabled = true;
        }
    }
}
