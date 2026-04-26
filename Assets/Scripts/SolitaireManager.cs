using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;


public class SolitiareManager : MonoBehaviour
{
    [Header("--------------Data----------------")]
    [Tooltip("card zone manager to manage cards in the card zone")]
    [SerializeField] SolitaireCardZone cardZone;

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
}