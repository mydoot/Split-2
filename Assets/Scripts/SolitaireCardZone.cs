using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class SolitaireCardZone : CardEase.CardZoneManager<SolitaireCardModel>
{
    // --------------------------MONO methods------------------------
    [SerializeField] bool Spade;
    [SerializeField] bool Club;
    [SerializeField] bool Hearts;
    [SerializeField] bool Diamonds;

    protected override void Awake()
    {
        base.Awake();
    }




}

