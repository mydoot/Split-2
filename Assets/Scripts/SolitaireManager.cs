using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using DG.Tweening;
using Demo;
using UnityEngine.UI;


public class SolitiareManager : MonoBehaviour
{
    [Header("--------------Data----------------")]
    [Tooltip("card zone manager to manage cards in the card zone")]
    [SerializeField] SolitaireCardZone cardZone;

    [Tooltip("card zone managers for solitaire slots")]
    [SerializeField] SolitaireCardZone solitaireZone1;
    [SerializeField] SolitaireCardZone solitaireZone2;
    [SerializeField] SolitaireCardZone solitaireZone3;
    [SerializeField] SolitaireCardZone solitaireZone4;

    [SerializeField] GameObject Completion1;
    [SerializeField] GameObject Completion2;
    [SerializeField] GameObject Completion3;
    [SerializeField] GameObject Completion4;


    [Tooltip("Card model to use for cards")]
    [SerializeField] SolitaireCardModel[] cards; // Array that contains all of the cards; Will add the entire 52 deck later

    [Header("--------------Debug----------------")]
    [Tooltip("force win by checking the box")]
    [SerializeField] public bool clubsOrdered;
    [SerializeField] public bool spadesOrdered;
    [SerializeField] public bool heartsOrdered;
    [SerializeField] public bool diamondsOrdered;

    // --------------------------MONO methods------------------------

    void Start()
    {
        //DOTween.init();

        SolitaireCardModel[] randomizeCards = cards.OrderBy(x => UnityEngine.Random.value).ToArray();

        List<SolitaireCardModel> cardDeck = randomizeCards.ToList();

        // cardZone adds a group and also adds a card into said group

        // Used AI belows
        int i = 0;

        while (i < cardDeck.Count)
        {
            // Random.Range is max exclusive when using ints
            int randomGroupSize = Random.Range(1, 4);

            int cardsToGrab = Mathf.Min(randomGroupSize, cardDeck.Count - i);

            List<SolitaireCardModel> newGroup = cardDeck.GetRange(i, cardsToGrab);
            cardZone.AddGroup(newGroup);

            i += cardsToGrab;
        }

        cardZone.transform.DOMoveX(50, 10).From().OnComplete(() => cardZone.startConveyor());


        cardZone.RefreshCardZone();


    }

    // --------------------------HELPER METHODS------------------------

    void Update()
    {
        checkZones();
    }



    private int getRandomNumber(int min, int max)
    {
        return Random.Range(min, max);
    }

    // Possibly create a checkZones() function and have it update per frame
    // Each zone is assigned a different suit and if the list lists them in numerical order (1 = Ace & 13 = King), it is valid
    // Once all 4 zones are complete, finish the mini-game
    public void checkZones()
    {
        //Debug.Log("checking cards");

        //Check Club
        if (performCheck(changeCurrentCards(solitaireZone1), "Club"))
        {
            Debug.Log("Clubs is ordered");
            clubsOrdered = true;
            Completion1.SetActive(true);
        }
        

        //Check Spade
        if (performCheck(changeCurrentCards(solitaireZone2), "Spade"))
        {
            Debug.Log("Spade is ordered");
            spadesOrdered = true;
            Completion2.SetActive(true);
        }
       

        //Check Hearts
        if (performCheck(changeCurrentCards(solitaireZone3), "Hearts"))
        {
            Debug.Log("Hearts is ordered");
            heartsOrdered = true;
            Completion3.SetActive(true);
        }
       

        //Check Diamonds
        if (performCheck(changeCurrentCards(solitaireZone4), "Diamonds"))
        {
            Debug.Log("Diamonds is ordered");
            diamondsOrdered = true;
            Completion4.SetActive(true);
        }
      

    }

    public bool checkWin()
    {
        if (clubsOrdered && spadesOrdered && heartsOrdered && diamondsOrdered)
        {
            return true;
        }

        return false;
    }

    private bool performCheck(List<SolitaireCardModel> deck, string suit)
    {
        if (deck == null)
        {
            //Debug.Log("this zone has no cards");
            return false;
        }


        SolitaireCardModel prevCard = null;

        foreach (SolitaireCardModel card in deck)
        {

            //Debug.Log($"Card: {card.Rank} & {card.Suit}");

            if (card.Suit != suit)
            {
                Debug.Log("Incorrect suit");
                break;
            }
            else
            {
                if (card.Rank == 1)
                {
                    prevCard = card;
                    //immediately goes to the next card, we already know the first card should be 1
                    continue;
                }
                else if (prevCard == null)
                {
                    //Debug.Log("first card is not 1");
                    break;
                }
                if (card.Rank != (prevCard.Rank + 1))
                {
                    //Debug.Log("rank is not in order");
                    break;
                }
                else
                {
                    prevCard = card;
                }

                if (card.Rank == 13)
                {
                    //Debug.Log("deck is ordered");
                    return true;
                }
            }

        }

        return false; //false is standard bool to return
    }

    private List<SolitaireCardModel> changeCurrentCards(SolitaireCardZone zone)
    {
        //AI assisted with the below code
        List<SolitaireCardModel> currentCards = zone.GetAllCards()
            .OfType<SolitaireCardManager>()
            .Select(manager => manager.getModel())
            .ToList();

        return currentCards;
    }
}