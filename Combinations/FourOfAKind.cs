using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Task1.Combinations
{
    public class FourOfAKind : CombinationClass
    {
        public Rank CardRank { get; }
        public Rank Kicker { get; }

        public FourOfAKind(Rank r, Rank k)
        {
            this.CardRank = r;
            this.Kicker = k;
            this.Marker = Combination.FourOfAKind;
        }

        protected override int CompareWithSame(object obj)
        {
            FourOfAKind compObj = (FourOfAKind)obj;
            if (this.CardRank > compObj.CardRank) return 1;
            if (this.CardRank < compObj.CardRank) return -1;
            if (this.Kicker > compObj.Kicker) return 1;
            if (this.Kicker < compObj.Kicker) return -1;
            return 0;
        }

        public override string ToString()
        {
            return "FOUR OF A KIND: RANK -> " + this.CardRank + " KICKER -> " + this.Kicker;
        }
        public override int GetHashCode()
        {
            return this.CardRank.GetHashCode() * 96 + this.Kicker.GetHashCode() * 66;
        }

        public override bool Equals(object obj)
        {
            var other = obj as CombinationClass;
            if (this.Marker != other.Marker) return false;
            var otherEquals = obj as FourOfAKind;
            if (this.CardRank != otherEquals.CardRank) return false;
            if (this.Kicker != otherEquals.Kicker) return false;
            return true;
        }
        public static bool IsFourOfAKind(IList<Card> combCards, Dictionary<Rank, int> ranks)
        {
            return ranks.ContainsValue(4);
        }

        public static FourOfAKind MakeFourOfAKind(IList<Card> combCards, Dictionary<Rank, int> ranks)
        {
            Rank highCard = Rank.Value2; // Use unassigment variable
            Rank kicker = Rank.Value2;
            foreach (KeyValuePair<Rank, int> entry in ranks)
            {
                if (entry.Value == 4)
                { highCard = entry.Key; }
                if (entry.Value == 1)
                { kicker = entry.Key; }
            }
            return new FourOfAKind(highCard, kicker);
        }
    }
}
