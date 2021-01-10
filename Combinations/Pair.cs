using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Task1.Combinations
{
    public class Pair : CombinationClass
    {
        public Rank PairRank { get; }
        public Rank Kicker { get; }
        public Pair(Rank pr, Rank k)
        {
            this.PairRank = pr;
            this.Kicker = k;
            this.Marker = Combination.Pair;
        }
        protected override int CompareWithSame(object obj)
        {
            Pair other = (Pair)obj;
            if (this.PairRank > other.PairRank) return 1;
            if (this.PairRank < other.PairRank) return -1;
            if (this.Kicker > other.Kicker) return 1;
            if (this.Kicker < other.Kicker) return -1;
            return 0;
        }

        public override string ToString()
        {
            return "PAIR: PAIR RANK -> " + this.PairRank + " KICKER -> " + this.Kicker;
        }
        public override int GetHashCode()
        {
            return this.PairRank.GetHashCode() * 22 + this.Kicker.GetHashCode() * 33;
        }

        public override bool Equals(object obj)
        {
            var other = obj as CombinationClass;
            if (this.Marker != other.Marker) return false;
            var otherEquals = obj as Pair;
            if (this.PairRank != otherEquals.PairRank) return false;
            if (this.Kicker != otherEquals.Kicker) return false;
            return true;
        }

       public static bool IsPair(IList<Card> combCards, Dictionary<Rank, int> ranks)
        {
            return ranks.ContainsValue(2);
        }

        public static Pair MakePair(IList<Card> combCards, Dictionary<Rank, int> ranks)
        {
            Rank highCard = Rank.Value2;
            Rank kicker = Rank.Value2;
            List<Rank> potentialKickers = ranks.Keys.ToList();
            foreach (KeyValuePair<Rank, int> entry in ranks)
            {
                if (entry.Value == 2)
                { highCard = entry.Key; }
            }
            potentialKickers.Remove(highCard);
            potentialKickers.Sort();
            kicker = potentialKickers.Max();
            return new Pair(highCard, kicker);

        }
    }
}
