using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Task1.Combinations
{
    public class HighCard : CombinationClass
    {
        public Rank Kicker { get; }
        public HighCard(Rank r)
        {
            this.Kicker = r;
            this.Marker = Combination.HighCard;
        }

        protected override int CompareWithSame(object obj)
        {
            HighCard compObj = (HighCard)obj;

            if (this.Kicker > compObj.Kicker) return 1;
            if (this.Kicker < compObj.Kicker) return -1;
            return 0;
        }
        public override string ToString()
        {
            return "HIGH CARD -> " + this.Kicker;
        }
        public override int GetHashCode()
        {
            return this.Kicker.GetHashCode() * 91;
        }

        public override bool Equals(object obj)
        {
            var other = obj as CombinationClass;
            if (this.Marker != other.Marker) return false;
            var otherEquals = obj as HighCard;
            if (this.Kicker != otherEquals.Kicker) return false;
            return true;
        }

        public static HighCard MakeHighCard(IList<Card> combCards, Dictionary<Rank, int> ranks)
        {
            Rank highCard = ranks.Keys.Max();
            return new HighCard(highCard);
        }
    }
}
