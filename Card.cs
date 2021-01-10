using System;
using System.Collections.Generic;
using System.Text;

namespace Task1.Combinations
{
    public enum Rank
    {
        Value2,
        Value3,
        Value4,
        Value5,
        Value6,
        Value7,
        Value8,
        Value9,
        ValueT,
        ValueJ,
        ValueQ,
        ValueK,
        ValueA
    }

    public enum Suit
    {
        ValueH,
        ValueD,
        ValueC,
        ValueS,
    }

    public class Card
    {
        private Rank _rank;
        public Rank Rank
        {
            get { return _rank; }
            set { _rank = value; }
        }

        private Suit _suit;
        public Suit Suit
        {
            get { return _suit; }
            set { _suit = value; }
        }

        public Card(Rank r, Suit s)
        {
            this._rank = r;
            this._suit = s;
        }

        public override string ToString()
        {
            return RanksToString(Rank) + SuitToString(Suit).ToString();
        }

        private static char SuitToString(Suit s)
        {
            switch (s)
            {
                case Suit.ValueH:
                    return 'h';
                case Suit.ValueD:
                    return 'd';
                case Suit.ValueC:
                    return 'c';
                case Suit.ValueS:
                    return 's';
                default:
                    return 'g';
            }

        }

        public static char RanksToString(Rank s)
        {
            switch (s)
            {
                case Rank.Value2:
                    return '2';
                case Rank.Value3:
                    return '3';
                case Rank.Value4:
                    return '4';
                case Rank.Value5:
                    return '5';
                case Rank.Value6:
                    return '6';
                case Rank.Value7:
                    return '7';
                case Rank.Value8:
                    return '8';
                case Rank.Value9:
                    return '9';
                case Rank.ValueT:
                    return 'T';
                case Rank.ValueJ:
                    return 'J';
                case Rank.ValueQ:
                    return 'Q';
                case Rank.ValueK:
                    return 'K';
                case Rank.ValueA:
                    return 'A';
                default:
                    return 'g';
            }

        }

        public static Rank ReturnRankEnum(char rank)
        {
            Rank returnRank = Rank.Value2;
            char[] charArray = { '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K', 'A' };
            int indexOfRank = Array.IndexOf(charArray, rank);

            foreach (int i in Enum.GetValues(typeof(Rank)))
            {
                if (indexOfRank == i)
                {
                    returnRank = (Rank)i;
                }
            }
            return returnRank;
        }

        public static Suit ReturnSuitEnum(char suit)
        {
            Suit returnSuit = Suit.ValueC;
            switch (suit)
            {
                case 'h':
                    returnSuit = Suit.ValueH;
                    break;
                case 'd':
                    returnSuit = Suit.ValueD;
                    break;
                case 'c':
                    returnSuit = Suit.ValueC;
                    break;
                case 's':
                    returnSuit = Suit.ValueS;
                    break;
                default:
                    break;
            }
            return returnSuit;
        }
    }
}
