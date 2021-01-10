using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Task1.Combinations
{
    public class ThreeOfAKind : CombinationClass
    {
        public Rank CardRank { get; }
        public Rank Kicker { get; }

        public ThreeOfAKind(Rank cr, Rank k)
        {
            this.CardRank = cr;
            this.Kicker = k;
            this.Marker = Combination.ThreeOfAKind;
        }

        protected override int CompareWithSame(object obj)
        {
            ThreeOfAKind other = (ThreeOfAKind)obj;
            if (this.CardRank > other.CardRank) return 1;
            if (this.CardRank < other.CardRank) return -1;
            if (this.Kicker > other.Kicker) return 1;
            if (this.Kicker < other.Kicker) return -1;
            return 0;
        }

        public override string ToString()
        {
            return "THREE OF A KIND: RANK -> " + this.CardRank + " KICKER -> " + this.Kicker;
        }
        public override int GetHashCode()
        {
            return this.CardRank.GetHashCode() * 94 + this.Kicker.GetHashCode() * 59;
        }

        public override bool Equals(object obj)
        {
            var other = obj as CombinationClass;
            if (this.Marker != other.Marker) return false;
            var otherEquals = obj as ThreeOfAKind;
            if (this.CardRank != otherEquals.CardRank) return false;
            if (this.Kicker != otherEquals.Kicker) return false;
            return true;
        }
        public static bool IsThreeOfAKind(IList<Card> combCards, Dictionary<Rank, int> ranks)
        {
            return ranks.ContainsValue(3);
        }

        public static ThreeOfAKind MakeThreeOfAKind(IList<Card> combCards, Dictionary<Rank, int> ranks)
        {
            Rank highCard = Rank.Value2;
            Rank kicker = Rank.Value2;

            foreach (KeyValuePair<Rank, int> entry in ranks)
            {
                if (entry.Value == 3)
                {
                    highCard = entry.Key;
                }
            }

            List<Rank> sortedRanks = ranks.Keys.ToList();
            sortedRanks.Remove(highCard);
            kicker = sortedRanks.Max();
            return new ThreeOfAKind(highCard, kicker);

        }
    }
}
