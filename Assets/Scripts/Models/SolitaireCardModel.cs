using UnityEngine;

    [System.Serializable]
    public class SolitaireCardModel : CardEase.CardModel
    {
        public string Suit;
        public int Rank;
        public Sprite image;

        public SolitaireCardModel(string suit, int rank, Sprite img)
        {
            this.Suit = suit;
            this.Rank = rank;
            this.image = img;
        }

        public SolitaireCardModel getSolitaireCardData()
    {
        return this;
    }
    }