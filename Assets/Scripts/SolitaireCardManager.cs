using UnityEngine.UI;

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
            this.cardImage.sprite = cardModel.backsideImage;
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
