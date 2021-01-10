using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Task1.Combinations
{
    public class TwoPairs : CombinationClass
    {
        public Rank PairRank { get; }
        public Rank SecondPairRank { get; }
        public Rank Kicker { get; }

        public TwoPairs(Rank pr, Rank spr, Rank k)
        {
            this.PairRank = pr;
            this.SecondPairRank = spr;
            this.Kicker = k;
            this.Marker = Combination.TwoPair;
        }

        protected override int CompareWithSame(object obj)
        {
            TwoPairs other = (TwoPairs)obj;
            if (this.PairRank > other.PairRank) return 1;
            if (this.PairRank < other.PairRank) return -1;
            if (this.SecondPairRank > other.SecondPairRank) return 1;
            if (this.SecondPairRank < other.SecondPairRank) return -1;
            if (this.Kicker > other.Kicker) return 1;
            if (this.Kicker < other.Kicker) return -1;
            return 0;
        }
        public override string ToString()
        {
            return "TWO PAIRS: HIGH PAIR -> " + this.PairRank + " LOW PAIR -> " + this.SecondPairRank + " KICKER -> " + this.Kicker;
        }
        public override int GetHashCode()
        {
            return this.PairRank.GetHashCode() * 59 + this.SecondPairRank.GetHashCode() * 69 + this.Kicker.GetHashCode() * 49;
        }

        public override bool Equals(object obj)
        {
            var other = obj as CombinationClass;
            if (this.Marker != other.Marker) return false;
            var otherEquals = obj as TwoPairs;
            if (this.PairRank != otherEquals.PairRank) return false;
            if (this.SecondPairRank != otherEquals.SecondPairRank) return false;
            if (this.Kicker != otherEquals.Kicker) return false;
            return true;
        }
        public static bool IsTwoPairs(IList<Card> combCards, Dictionary<Rank, int> ranks)
        {
            return ranks.Count == 3 && ranks.ContainsValue(2);
        }

        public static TwoPairs MakeTwoPairs(IList<Card> combCards, Dictionary<Rank, int> ranks)
        {
            Rank highPair = Rank.Value2;
            Rank lowPair = Rank.Value2;
            Rank kicker = Rank.Value2;
            List<Rank> pairs = new List<Rank>();
            foreach (KeyValuePair<Rank, int> entry in ranks)
            {
                if (entry.Value == 2)
                {
                    pairs.Add(entry.Key);

                }
                if (entry.Value == 1)
                { kicker = entry.Key; }
            }

            highPair = pairs.Max();
            lowPair = pairs.Min();
            return new TwoPairs(highPair, lowPair, kicker);
        }
    }
}
