using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Task1.Combinations
{
    public class Flush : CombinationClass
    {
        public Rank CardRank { get; }

        public Flush(Rank r)
        {
            this.CardRank = r;
            this.Marker = Combination.Flush;
        }

        protected override int CompareWithSame(object obj)
        {
            Flush compObj = (Flush)obj;

            if (this.CardRank > compObj.CardRank) return 1;
            if (this.CardRank < compObj.CardRank) return -1;
            return 0;
        }

        public override string ToString()
        {
            return "FLUSH: RANK -> " + this.CardRank;
        }
        public override int GetHashCode()
        {
            return this.CardRank.GetHashCode() * 97;
        }

        public override bool Equals(object obj)
        {
            var other = obj as CombinationClass;
            if (this.Marker != other.Marker) return false;
            var otherEquals = obj as Flush;
            if (this.CardRank != otherEquals.CardRank) return false;
            return true;
        }

        public static bool IsFlush(IList<Card> combCards, Dictionary<Suit, int> suits)
        {
            return suits.ContainsValue(5);
        }

        public static Flush MakeFlush(IList<Card> combCards, Dictionary<Rank, int> ranks)
        {
            Rank highCard = ranks.Keys.Max();
            return new Flush(highCard);
        }
    }
}
