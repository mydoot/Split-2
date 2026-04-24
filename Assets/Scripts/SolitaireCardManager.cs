using UnityEngine.UI;

    public class SolitaireCardManager : CardEase.CardManager<SolitaireCardModel>
    {
        Image cardImage;

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
        }

        public override void UpdateSelection(bool isSelected)
        {
            this.isSelected = isSelected;
        }
    }
