using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenDRL
{
    public enum ActiveCardType { InstantHeal, InstantDamage, DamageOverTime, SpeedOverTime }

    class ActiveCard : Card
    {
        private ActiveCardType activeType;
        int instantDamageAmount, instantHealAmount, timedDamageDuration, timedSpeedDuration;
        float timedDamageModifier, timedSpeedModifier;

        public ActiveCardType ActiveType
        {
            get => activeType;
        }

        /// <summary>
        /// Constructor for creating a new ActiveCard. The card values are set based on the card type
        /// </summary>
        /// <param name="type">ActiveCardType for this card</param>
        /// <param name="modifier">How much this modifies its appropriate weapon value</param>
        /// <param name="duration">How long this boost last if it was a boost card</param>
        /// <param name="amount">Amount of heal/damage for instant cards</param>
        /// <param name="name">Name of this card</param>
        public ActiveCard(ActiveCardType type, float modifier, int duration, int amount, string name) : base (name, CardType.ActiveCard)
        {
            this.activeType = type;
            this.instantHealAmount = 0;
            this.instantDamageAmount = 0;
            this.timedDamageDuration = 0;
            this.timedSpeedDuration = 0;
            this.timedDamageModifier = 1f;
            this.timedSpeedModifier = 1f;

            switch (type)
            {
                case ActiveCardType.InstantHeal:
                    this.instantHealAmount = amount;

                    break;
                case ActiveCardType.InstantDamage:
                    this.instantDamageAmount = amount;

                    break;
                case ActiveCardType.DamageOverTime:
                    this.timedDamageModifier = modifier;
                    this.timedDamageDuration = duration;

                    break;
                case ActiveCardType.SpeedOverTime:
                    this.timedSpeedModifier = modifier;
                    this.timedSpeedDuration = duration;
                    break;
                default:
                    break;
            }
        }


        public void ActivateCard()
        {
            if (activeType == ActiveCardType.DamageOverTime)
            {
                ActivateDamageOverTime();
            }
            else if (activeType == ActiveCardType.SpeedOverTime)
            {
                ActivateSpeedOverTime();
            }
        }

        public void ActivateCard(GameObject target)
        {
            if (activeType == ActiveCardType.InstantDamage)
            {
                ActivateInstantDamage(target);
            }
            else if (activeType == ActiveCardType.InstantHeal)
            {
                ActivateInstantHeal(target);
            }
        }

        public void CardClick(object sender, EventArgs e)
        {
            if (GameWorld.Instance.CurrentState == BattleScene.Instance && GameWorld.Instance.StateChanged == false)
            {
                SpriteRenderer spriteRenderer = (SpriteRenderer)this.GameObject.GetComponent("SpriteRenderer");

                Rectangle cardRec = new Rectangle(new Point((int)this.GameObject.Transform.Position.X, (int)this.GameObject.Transform.Position.Y), spriteRenderer.SpriteRectangle.Size);

                if (cardRec.Contains(new Point(((Point)sender).X, ((Point)sender).Y)))
                {
                    if (!BattleScene.Instance.CardAlreadyPlayed)
                    {
                        if (activeType == ActiveCardType.SpeedOverTime)
                        {

                        }
                        else if (activeType == ActiveCardType.DamageOverTime)
                        {

                        }
                        else if (activeType == ActiveCardType.InstantDamage)
                        {

                        }
                        else if (activeType == ActiveCardType.InstantHeal)
                        {

                        }

                        DestroyCard();
                    }
                }
            }
        }

        private void ActivateDamageOverTime()
        {
            // Temporarily get the first weapon from player:
            Weapon playerWep = (Weapon)PlayerManager.Instance.PlayerShip.GetComponent("Weapon");

            // TJEK OM BOOST ER AKTIVERET ALLEREDE!
            if(! playerWep.ActiveBoostEnabled)
            {
                playerWep.ApplyActiveDamageBoost(this.timedDamageModifier, this.timedDamageDuration);

                DestroyCard();
            }
            else
            {
                // KAN IKKE SPILLE KORTET LIGE NU SPADSER!
            }

        }

        private void ActivateSpeedOverTime()
        {
            // Temporarily get the first weapon from player:
            Weapon playerWep = (Weapon)PlayerManager.Instance.PlayerShip.GetComponent("Weapon");

            // TJEK OM BOOST ER AKTIVERET ALLEREDE!
            if (!playerWep.ActiveBoostEnabled)
            {
                playerWep.ApplyActiveSpeedBoost(this.timedSpeedModifier, this.timedSpeedDuration);

                DestroyCard();
            }
            else
            {
                // KAN IKKE SPILLE KORTET LIGE NU SPADSER!
            }

            DestroyCard();
        }

        private void ActivateInstantHeal(GameObject target)
        {
            Ship targetShip = (Ship)target.GetComponent("Ship");

            targetShip.AddHealth(instantHealAmount);
            DestroyCard();
        }

        private void ActivateInstantDamage(GameObject target)
        {
            Ship targetShip = (Ship)target.GetComponent("Ship");

            DestroyCard();
        }

        private void DestroyCard()
        {
            // HVORDAN FUCK FJERNER VI NU LIGE DE SKODKORT HER FRA LISTEN!?!=!=!)!("#()#¤5u9ert8h9dfglknj

            PlayerManager.Instance.RemoveCardFromPlayerHand(this.GameObject);
            BattleScene.Instance.RemoveGameObject(this.GameObject);

            // Remove onclick listener
            Button btn = (Button) this.GameObject.GetComponent("Button");

            btn.RemoveOnClickEvent();
        }
    }
}
