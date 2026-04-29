using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using Demo;


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

    [Tooltip("Card model to use for cards")]
    [SerializeField] SolitaireCardModel[] cards; // Array that contains all of the cards; Will add the entire 52 deck later


    // --------------------------MONO methods------------------------

    void Start()
    {
        SolitaireCardModel[] randomizeCards = cards.OrderBy(x => UnityEngine.Random.value).ToArray();

        List<SolitaireCardModel> cardDeck = randomizeCards.ToList();

        // cardZone adds a group and also adds a card into said group

        float RNG = getRandomNumber(0f, 3f);

        for (int i = 0; i < cardDeck.Count; i += RNG)
        {
            // Figure out how many cards to grab. Usually 3, but might be 1 or 2 at the very end!
            int cardsToGrab = Mathf.Min(3, deckToShuffle.Count - i);

            // GetRange extracts that specific chunk, and we add it as a new list to our master list
            List<SolitaireCardModel> newGroup = cardDeck.GetRange(i, cardsToGrab);
            cardZone.AddGroup(newGroup);

            RNG = getRandomNumber(0f, 3f);
        }



        cardZone.RefreshCardZone();



        // --------------------------HELPER METHODS------------------------

    }

    void Update()
    {
        checkZones();
    }

    private float getRandomNumber(float min, float max)
    {
        return Random.Range(min, max)
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
        }

        //Check Spade
        if (performCheck(changeCurrentCards(solitaireZone2), "Spade")){
            Debug.Log("Spade is ordered");
        }

        //Check Hearts
        if (performCheck(changeCurrentCards(solitaireZone3), "Hearts"))
        {
            Debug.Log("Hearts is ordered");
        }

        //Check Diamonds
        if (performCheck(changeCurrentCards(solitaireZone4), "Diamonds"))
        {
            Debug.Log("Diamonds is ordered");
        }

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
                //Debug.Log("Incorrect suit");
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