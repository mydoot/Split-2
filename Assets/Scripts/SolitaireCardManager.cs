using UnityEngine.UI;
using UnityEngine;
using System.Linq;
using DG.Tweening;
using Demo;

public class SolitaireCardManager : CardEase.CardManager<SolitaireCardModel>
{
    Image cardImage;
    private SolitaireCardModel model;

    void Awake()
    {
        cardImage = GetComponent<Image>();
    }

    public override void SetData(SolitaireCardModel cardModel)
    {
        if (Random.Range(0, 2) == 0)
        {
            this.cardImage.sprite = cardModel.backsideImage;
        }
        else
        {
            this.cardImage.sprite = cardModel.image;
        }
        model = cardModel;
    }

    public override void UpdateSelection(bool isSelected)
    {
        this.isSelected = isSelected;
    }

    public override void CardPicked()
    {
        CardEase.EventManager<SolitaireCardModel, SolitaireCardManager>.CARD_PICKED.Invoke(this);
        this.cardImage.sprite = model.image;
        this.transform.DOScale(1.15f, 0.15f);
    }
    public override void CardDropped()
    {
        CardEase.EventManager<SolitaireCardModel, SolitaireCardManager>.CARD_DROOPED.Invoke(this);
        this.transform.DOScale(1f, 0.15f);
    }

    public SolitaireCardModel getModel()
    {
        return model;
    }
}