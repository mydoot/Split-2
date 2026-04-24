using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


    public class SolitiareManager : MonoBehaviour
    {
        [Header("--------------Data----------------")]
        [Tooltip("card zone manager to manage cards in the card zone")]
        [SerializeField] SolitaireCardZone cardZone;

        [Tooltip("Card model to use for cards")]
        [SerializeField] SolitaireCardModel[] cards;


        // --------------------------MONO methods------------------------

        void Start()
        {
        SolitaireCardModel randomCard = this.cards[Random.Range(0, this.cards.Length)];
            List<SolitaireCardModel> cards = new()
        {
            randomCard
        };
            cardZone.AddGroup(cards);
            cardZone.RefreshCardZone();
        }



        // --------------------------HELPER METHODS------------------------
      
    }