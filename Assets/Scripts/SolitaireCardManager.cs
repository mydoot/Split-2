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
        if (cardModel.image != null)
        {
            this.cardImage.sprite = cardModel.image;
        }
        model = cardModel;
    }

    public override void UpdateSelection(bool isSelected)
    {
        this.isSelected = isSelected;
    }

    public SolitaireCardModel getModel()
    {
        return model;
    }
}
