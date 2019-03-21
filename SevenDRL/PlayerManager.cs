using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenDRL
{
    public class PlayerManager
    {
        private static PlayerManager instance;
        private GameObject playerShip;
        private List<GameObject> playerCards;

        public static PlayerManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PlayerManager();
                }
                return instance;
            }
        }

        public GameObject PlayerShip
        {
            get => playerShip;
        }

        public List<GameObject> PlayerCards
        {
            get => playerCards;
        }

        private PlayerManager ()
        {
            playerCards = new List<GameObject>();
        }


        public void LoadContent()
        {
            // Opretter spilleren her:
            playerShip = new GameObject(new Vector2(50, 100));

            SpriteRenderer playerSprite = new SpriteRenderer("player_ship");
            playerSprite.LoadContent();
            playerShip.AddComponent(playerSprite);
            playerShip.AddComponent(new Ship(100));
            playerShip.AddComponent(new Weapon(10, 400));

            // Opsæt kort her:
            GivePlayerStartingCards();
        }


        public void GivePlayerStartingCards()
        {              
            playerCards.Add(ActiveCardFactory.Instance.Create(ActiveCardType.InstantDamage, 1f, 0, 30, "card1", "Volapyk"));
            playerCards.Add(ActiveCardFactory.Instance.Create(ActiveCardType.InstantHeal, 1f, 0, 30, "card3", "Ren card"));
            playerCards.Add(ActiveCardFactory.Instance.Create(ActiveCardType.DamageOverTime, 1.15f, 1000, 0, "card5", "Wo card"));
            playerCards.Add(ActiveCardFactory.Instance.Create(ActiveCardType.SpeedOverTime, 1.25f, 1000, 0, "card9", "Yi Er San card"));

            // Give cards a temporary position in hand
            for (int i = 0; i < playerCards.Count; i++)
            { // 800-350 = 450 // 1200 - 250
                playerCards[i].Transform.SetPosition(new Vector2((i * 260)+80, 450));
            }
        }
        public void AddCardToPlayerHand(GameObject card)
        {
            playerCards.Add(card);

            // Give cards a temporary position in hand
            for (int i = 0; i < playerCards.Count; i++)
            { // 800-350 = 450 // 1200 - 250
                playerCards[i].Transform.SetPosition(new Vector2((i * 260) + 80, 450));
            }
        }

        public void RemoveCardFromPlayerHand(GameObject card)
        {
            playerCards.Remove(card);

            // Give cards a temporary position in hand
            for (int i = 0; i < playerCards.Count; i++)
            { // 800-350 = 450 // 1200 - 250
                playerCards[i].Transform.SetPosition(new Vector2((i * 260) + 80, 450));
            }
        }

        // KAN IKKE TESTES FØR VI HAR KORT ASSETS LAVET
        public void AddCard(GameObject newCardObject)
        {
            if (newCardObject.GetComponent("Card") == null)
            {
                Console.WriteLine("ksk");
            }
            Console.WriteLine("kssj");
        }

        public void RemoveCard(GameObject oldCardObject)
        {
            if (oldCardObject.GetComponent("Card") == null)
            {
                Console.WriteLine("ksk");
            }
            Console.WriteLine("kssj");
        }
    }
}
