using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Task1.Combinations
{
    public class StraightFlush : CombinationClass
    {
        public Rank CardRank { get; }
        public StraightFlush(Rank r)
        {
            this.Marker = Combination.StraightFLush;
            this.CardRank = r;
        }

        protected override int CompareWithSame(object obj)
        {
            StraightFlush compObj = (StraightFlush)obj;

            if (this.CardRank > compObj.CardRank) return 1;
            if (this.CardRank < compObj.CardRank) return -1;
            return 0;
        }
        public override string ToString()
        {
            return "STRAIGHT FLUSH: RANK -> " + this.CardRank;
        }

        public override int GetHashCode()
        {
            return this.CardRank.GetHashCode() * 99;
        }

        public override bool Equals(object obj)
        {
            var other = obj as CombinationClass;
            if (this.Marker != other.Marker) return false;
            var otherEquals = obj as StraightFlush;
            if (this.CardRank != otherEquals.CardRank) return false;
            return true;
        }
        public static bool IsStraightFlush(IList<Card> combCards, Dictionary<Rank, int> ranks, Dictionary<Suit, int> suits)
        {
            return Straight.IsStraight(combCards, ranks) && Flush.IsFlush(combCards, suits);
        }
        public static StraightFlush MakeStraightFlush(IList<Card> combCards, Dictionary<Rank, int> ranks)
        {
            Rank highCard = ranks.Keys.Max();
            return new StraightFlush(highCard);
        }
    }
}
