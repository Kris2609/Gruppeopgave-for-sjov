using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SevenDRL
{
    public class CardRewardScene : GameState
    {
        private List<GameObject> gameObjects;
        private static CardRewardScene instance;
        private Stack<GameObject> rewardCards;

        private GameObject visibleCard;
        private GameObject acceptButton;
        private GameObject declineButton;
        private GameObject continueButton;

        private List<GameObject> gameObjectsToRemove;
        private List<GameObject> gameObjectsToAdd;

        public List<GameObject> StateList
        {
            get
            {
                return gameObjects;
            }
            set
            {
                gameObjects = value;
            }
        }

        public static CardRewardScene Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CardRewardScene();
                }
                return instance;
            }
        }

        private CardRewardScene()
        {
            gameObjects = new List<GameObject>();
            rewardCards = new Stack<GameObject>();
            gameObjectsToRemove = new List<GameObject>();
            gameObjectsToAdd = new List<GameObject>();

            // Create buttons with factory
            acceptButton = ButtonFactory.Instance.Create(new Vector2(100, 350), OnAcceptCardButtonClicked, "cr_accept");
            declineButton = ButtonFactory.Instance.Create(new Vector2(800, 350), OnDeclineCardButtonClicked, "cr_decline");
            continueButton = ButtonFactory.Instance.Create(new Vector2(500, 350), OnNoMoreCardsLeftButtonClicked, "cr_continue");
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (GameObject item in gameObjects)
            {
                item.Draw(spriteBatch);
            }
        }
        public void Execute(GameTime gameTime)
        {
            foreach (GameObject item in gameObjects)
            {
                item.Update(gameTime);
            }



            foreach (GameObject card in gameObjectsToRemove)
            {
                this.gameObjects.Remove(card);
            }
            gameObjectsToRemove.Clear();

            foreach (GameObject card in gameObjectsToAdd)
            {
                this.gameObjects.Add(card);
            }
            gameObjectsToAdd.Clear();
        }

        public void Enter()
        {
            SetupRandomListOfRewardCards();

            PickNextCard();
            gameObjects.Add(acceptButton);
            gameObjects.Add(declineButton);
        }

        public void Exit()
        {
            this.gameObjects.Clear();
            this.gameObjectsToAdd.Clear();
            this.gameObjectsToRemove.Clear();
            this.rewardCards.Clear();
        }


        public void OnAcceptCardButtonClicked(object sender, EventArgs e)
        {
            if (GameWorld.Instance.CurrentState == this && this.visibleCard != null)
            {
                Rectangle rec = new Rectangle(100, 350, 200, 100);

                if (rec.Contains(new Point(((Point)sender).X, ((Point)sender).Y)))
                {
                    // How to add cards to player hand decently??
                    PlayerManager.Instance.AddCardToPlayerHand(this.visibleCard);

                    GameWorld.Instance.ChangeState(Map.MapInstance);
                }
            }
        }

        public void OnDeclineCardButtonClicked(object sender, EventArgs e)
        {
            if (GameWorld.Instance.CurrentState == this && this.visibleCard != null)
            {
                Rectangle rec = new Rectangle(800, 350, 200, 100);

                if (rec.Contains(new Point(((Point)sender).X, ((Point)sender).Y)))
                {
                    PickNextCard();
                }
            }
        }

        public void OnNoMoreCardsLeftButtonClicked(object sender, EventArgs e)
        {
            if (GameWorld.Instance.CurrentState == this && this.visibleCard == null)
            {
                Rectangle rec = new Rectangle(500, 350, 200, 100);

                if (rec.Contains(new Point(((Point)sender).X, ((Point)sender).Y)))
                {
                    // Change to map scene
                    GameWorld.Instance.ChangeState(Map.MapInstance);
                }
            }
        }

        private void PickNextCard()
        {
            // If a card already exists on screen
            if (this.visibleCard != null)
            {
                gameObjectsToRemove.Add(this.visibleCard);
                this.visibleCard = null;
            }

            if (rewardCards.Count > 0)
            {
                this.visibleCard = rewardCards.Pop();
                this.gameObjectsToAdd.Add(this.visibleCard);
            }
            else
            {
                gameObjectsToAdd.Add(continueButton);
                gameObjectsToRemove.Add(acceptButton);
                gameObjectsToRemove.Add(declineButton);
                // NOR MORE CARDS BUDDYYYY!!!! :( :( :(
            }
        }

        private void SetupRandomListOfRewardCards()
        {
            Random randomizer = new Random();

            int rewardCount = randomizer.Next(3, 6); // between 3 & 5 cards (upper bound exclusive)

            // Create each card
            for (int i = 0; i < rewardCount; i++)
            {
                int cardPhotoNumber = randomizer.Next(1, 10); // Random between number of card assets

                // MIDLERTIDIGE VÆRDIER INDTIL KORT ER OPFUNDET
                GameObject rewardCard = ActiveCardFactory.Instance.Create(ActiveCardType.InstantDamage, 1f, 0, 20, "card" + cardPhotoNumber.ToString(), "woopie kortet");

                // Temporarily center card in middle of screen
                rewardCard.Transform.SetPosition(new Vector2(475, 225));

                rewardCards.Push(rewardCard);
            }
        }
    }
}
