using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Task1.Combinations
{
    public class Straight : CombinationClass
    {
        public Rank CardRank { get; }
        public Straight(Rank r)
        {
            this.CardRank = r;
            this.Marker = Combination.Straight;
        }

        protected override int CompareWithSame(object obj)
        {
            Straight compObj = (Straight)obj;

            if (this.CardRank > compObj.CardRank) return 1;
            if (this.CardRank < compObj.CardRank) return -1;
            return 0;
        }

        public override string ToString()
        {
            return "STRAIGHT: RANK -> " + this.CardRank;
        }
        public override int GetHashCode()
        {
            return this.CardRank.GetHashCode() * 98;
        }

        public override bool Equals(object obj)
        {
            var other = obj as CombinationClass;
            if (this.Marker != other.Marker) return false;
            var otherEquals = obj as Straight;
            if (this.CardRank != otherEquals.CardRank) return false;
            return true;
        }
       public static bool IsStraight(IList<Card> combCards, Dictionary<Rank, int> ranks)
        {
            if (ranks.Count == 5)
            {
                List<Rank> sortedRanks = ranks.Keys.ToList();
                sortedRanks.Sort();
                List<Rank> astraight = new List<Rank>() { Rank.Value2, Rank.Value3, Rank.Value4, Rank.Value5, Rank.ValueA };
                for (int count = 0; count < (sortedRanks.Count - 1); count++)
                {
                    var element1 = sortedRanks[count];
                    var element2 = sortedRanks[count + 1];
                    if (element2 != element1 + 1)
                    {
                        return false;
                    }
                    if (sortedRanks.SequenceEqual(astraight))
                    {
                        return true;
                    }
                }
                return true;
            }
            return false;
        }

       public static Straight MakeStraight(IList<Card> combCards, Dictionary<Rank, int> ranks)
        {
            Rank highCard = ranks.Keys.Max();
            List<Rank> sortedRanks = ranks.Keys.ToList();
            sortedRanks.Sort();
            List<Rank> astraight = new List<Rank>() { Rank.Value2, Rank.Value3, Rank.Value4, Rank.Value5, Rank.ValueA };

            if (sortedRanks.SequenceEqual(astraight))
            {
                sortedRanks.Remove(highCard);
                highCard = sortedRanks.Max();
            }
            return new Straight(highCard);
        }
    }
}
