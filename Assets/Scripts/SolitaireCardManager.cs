using UnityEngine.UI;
using UnityEngine;
using System.Linq;

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
    }

    public SolitaireCardModel getModel()
    {
        return model;
    }
}