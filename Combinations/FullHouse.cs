using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Task1.Combinations
{
    public class FullHouse : CombinationClass
    {
        public Rank TwoCard { get; }
        public Rank ThreeCard { get; }

        public FullHouse(Rank th, Rank tw)
        {
            this.ThreeCard = th;
            this.TwoCard = tw;
            this.Marker = Combination.FullHouse;
        }

        protected override int CompareWithSame(object obj)
        {
            FullHouse compObj = (FullHouse)obj;
            if (this.ThreeCard > compObj.ThreeCard) return 1;
            if (this.ThreeCard < compObj.ThreeCard) return -1;
            if (this.TwoCard > compObj.TwoCard) return 1;
            if (this.TwoCard < compObj.TwoCard) return -1;
            return 0;
        }

        public override string ToString()
        {
            return "FULL HOUSE: THREE CARD -> " + this.ThreeCard + " TWO CARD ->" + this.TwoCard;
        }
        public override int GetHashCode()
        {
            return this.ThreeCard.GetHashCode() * 89 + this.TwoCard.GetHashCode() * 79;
        }

        public override bool Equals(object obj)
        {
            var other = obj as CombinationClass;
            if (this.Marker != other.Marker) return false;
            var otherEquals = obj as FullHouse;
            if (this.ThreeCard != otherEquals.ThreeCard) return false;
            if (this.TwoCard != otherEquals.TwoCard) return false;
            return true;
        }
        public static bool IsFullHouse(IList<Card> combCards, Dictionary<Rank, int> ranks, Dictionary<Suit, int> suits)
        {
            return ranks.Count == 2 && (ranks.ContainsValue(2) && ranks.ContainsValue(3));
        }

        public static FullHouse MakeFullHouse(IList<Card> combCards, Dictionary<Rank, int> ranks)
        {
            Rank threeCards = Rank.Value2; // Use unassigment variable
            Rank twoCards = Rank.Value2;
            foreach (KeyValuePair<Rank, int> entry in ranks)
            {
                if (entry.Value == 3)
                { threeCards = entry.Key; }
                if (entry.Value == 2)
                { twoCards = entry.Key; }
            }
            return new FullHouse(threeCards, twoCards);

        }
    }
}
