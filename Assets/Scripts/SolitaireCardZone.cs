using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;



public class SolitaireCardZone : CardEase.CardZoneManager<SolitaireCardModel>
{
    // --------------------------MONO methods------------------------
    [SerializeField] bool Spade;
    [SerializeField] bool Club;
    [SerializeField] bool Hearts;
    [SerializeField] bool Diamonds;

    private float groupLength;
    private float spacing;
    [SerializeField] float loopThreshold;

    private RectTransform rectTransform;
    private HorizontalLayoutGroup layoutGroup;
    [SerializeField] private float distanceMoved = 0f;
    [SerializeField] public float speed = 200f;

    private bool initialized = false;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        layoutGroup = GetComponent<HorizontalLayoutGroup>();

    }

    // AI HELPED WITH THE BELOW
    private void Update()
    {
        if (initialized)
        {
            if (transform.childCount == 0) return;



            float moveAmount = speed * Time.deltaTime;
            rectTransform.anchoredPosition -= new Vector2(moveAmount, 0);

            distanceMoved += moveAmount;

            if (distanceMoved >= loopThreshold)
            {

                Transform groupToRecycle = transform.GetChild(0);

                groupToRecycle.SetAsLastSibling();

                rectTransform.anchoredPosition += new Vector2(loopThreshold, 0);

                distanceMoved -= loopThreshold;

                startConveyor();
            }
        }
    }

    public void startConveyor()
    {
        /*if (!initialized)*/

        RectTransform groups = transform.GetChild(0).GetComponent<RectTransform>();
        /* foreach (RectTransform child in rectTransform)
         {
             groupLength += child.rect.width;
         }*/

        LayoutRebuilder.ForceRebuildLayoutImmediate(groups);

        spacing = layoutGroup.spacing;

        foreach (Transform child in transform)
        {
            if (child.TryGetComponent<SolitaireCardGroup>(out SolitaireCardGroup group))
            {
                if (!group.isEmpty)
                {
                    if (child.childCount <= 0)
                    {
                        group.isEmpty = true;
                        if (speed < 1200f) speed += 50f;
                    }

                }
            }

        }

        loopThreshold = groups.rect.width + layoutGroup.spacing;


        initialized = true;
    }

}

