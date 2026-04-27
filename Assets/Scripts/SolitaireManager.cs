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
    [Tooltip("card zone manager for solitaire slots")]
    [SerializeField] SolitaireCardZone solitaireZone1;
    [SerializeField] SolitaireCardZone solitaireZone2;
    [SerializeField] SolitaireCardZone solitaireZone3;
    [SerializeField] SolitaireCardZone solitaireZone4;

    [Tooltip("Card model to use for cards")]
    [SerializeField] SolitaireCardModel[] cards; // Array that contains all of the cards; Will add the entire 52 deck later

    [Tooltip("force win by checking the box")]
    [SerializeField] public bool clubsOrdered;
    [SerializeField] public bool spadesOrdered;
    [SerializeField] public bool heartsOrdered;
    [SerializeField] public bool diamondsOrdered;

    //ivate bool beginConveyor = false;
    private float groupLength;
    private float spacing;




    // --------------------------MONO methods------------------------

    void Start()
    {
        // On game start and select one random card from cards[] array 
        SolitaireCardModel randomCard = this.cards[Random.Range(0, this.cards.Length)];
        //List<SolitaireCardModel> cards = new()
        //{
        //    randomCard
        //};

        List<SolitaireCardModel> cardDeck = cards.ToList();

        // cardZone adds a group and also adds a card into said group
        cardZone.AddGroup(cardDeck);
        cardZone.RefreshCardZone();



        // --------------------------HELPER METHODS------------------------

    }

    // Possibly create a checkZones() function and have it update per frame
    // Each zone is assigned a different suit and if the list lists them in numerical order (1 = Ace & 13 = King), it is valid
    // Once all 4 zones are complete, finish the mini-game
    public void checkZone1()
    {
        Debug.Log("checking cards");

        //AI assisted with the below code
        List<SolitaireCardModel> currentCards = solitaireZone1.GetAllCards()
            .OfType<SolitaireCardManager>()
            .Select(manager => manager.getModel())
            .ToList();

        foreach (SolitaireCardModel card in currentCards)
        {
            Debug.Log($"{card.Rank} & {card.Suit}");
        }

    }
}