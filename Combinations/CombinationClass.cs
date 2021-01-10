using System;
using System.Collections.Generic;
using System.Text;

namespace Task1.Combinations
{
    public enum Combination
    {
        HighCard,
        Pair,
        TwoPair,
        ThreeOfAKind,
        Straight,
        Flush,
        FullHouse,
        FourOfAKind,
        StraightFLush
    }

    public abstract class CombinationClass : IComparable
    {
        public Combination Marker { get; set; }
        protected abstract int CompareWithSame(object obj);

        public virtual int CompareTo(object obj)
        {
            CombinationClass other = (CombinationClass)obj;
            if (this.Marker > other.Marker) return 1;
            if (this.Marker < other.Marker) return -1;
            return this.CompareWithSame(other);
        }
    }
}
