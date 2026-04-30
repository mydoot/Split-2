using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
public class SolitaireCardGroup : CardEase.CardGroupManager<SolitaireCardModel>
{

    [SerializeField] public bool isEmpty = false;
    // --------------------------MONO methods------------------------
    protected override void Awake()
        {
            base.Awake();
        }

        void Update()
        {

        }
    }
