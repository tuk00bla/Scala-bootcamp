using System;
using System.Collections.Generic;
using System.Text;
using Combinatorics.Collections;
using System.Linq;



namespace Task1
{
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

        public static char SuitToString(Suit s)
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

    }

    public  enum Rank
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




    static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter a string:");
            string enter = Console.ReadLine();
            CheckEnter(enter);
            Console.WriteLine(enter);
            Console.ReadKey();
        }

        private static string CheckEnter(string checkedString)
        {
            var tokens = checkedString.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in tokens)
            {
                Console.Write("'{0}' ", s);
                Console.WriteLine();
            }

            switch (tokens[0])
            {
                case "texas-holdem":
                    List<Card> board = ParseCards(tokens[1]);
                    List<List<Card>> hands = new List<List<Card>>();
                    for (int i = 2; i < tokens.Length; i++)
                    {
                        List<Card> newCards = ParseCards(tokens[i]);
                        hands.Add(newCards);
                    }
                    foreach (var hand in hands)
                    {
                        FindHandValue(hand, board);
                    }

                    break;
                case "omaha-holdem":

                    break;
                case "five-cards-draw":

                    break;
                default:
                    Console.WriteLine("You entered the wrong gametype, please re-enter");
                    break;
            }
            return checkedString;
        }

        private static List<Card> ParseCards(string hand)
        {
            List<Card> handList = new List<Card>();
            for (int i = 0; i < hand.Length; i += 2)
            {
                Card newCard = new Card(ReturnRankEnum(hand[i]), ReturnSuitEnum(hand[i + 1]));
                handList.Add(newCard);
            }
            return handList;
        }

        private static ICombination FindHandValue(List<Card> hand, List<Card> board)
        {
            List<Card> availableCards = new List<Card>();
            availableCards.AddRange(hand);
            availableCards.AddRange(board);

            Combinations<Card> variants = new Combinations<Card>(availableCards, 5);

            List<ICombination> combinations = new List<ICombination>();
            foreach (IList<Card> variant in variants)
            {

                Dictionary<Rank, int> rankGroups = GroupRanks(variant);
                Dictionary<Suit, int> suitGroups = GroupSuits(variant);
                ICombination comb = FindCombination(variant, rankGroups, suitGroups);
                combinations.Add(FindCombination(variant, rankGroups, suitGroups));
                Console.WriteLine(String.Join("; ", variant));
                Console.WriteLine(comb);
            }

            Console.WriteLine("Combination " + combinations.Max().ToString());
            return combinations.Max();
        }

        private static ICombination FindCombination(IList<Card> cards, Dictionary<Rank, int> ranks, Dictionary<Suit, int> suits)
        {
            if (IsStraightFlush(cards, ranks, suits)) { return MakeStraightFlush(cards, ranks, suits); }
            else if (IsFourOfAKind(cards, ranks)) { return MakeFourOfAKind(cards, ranks); }
            else if (IsFullHouse(cards, ranks, suits)) { return MakeFullHouse(cards, ranks, suits); }
            else if (IsFlush(cards, suits)) { return MakeFlush(cards, ranks, suits); }
            else if (IsStraight(cards, ranks)) { return MakeStraight(cards, ranks, suits); }
            else if (IsThreeOfAKind(cards, ranks)) { return MakeThreeOfAKind(cards, ranks, suits); }
            else if (IsTwoPairs(cards, ranks)) { return MakeTwoPairs(cards, ranks, suits); }
            else if (IsPair(cards, ranks)) { return MakePair(cards, ranks, suits); }
            else return MakeHighCard(cards, ranks, suits) ;
        }

        static Dictionary<Rank, int> GroupRanks(IList<Card> cards)
        {
            Dictionary<Rank, int> entries = new Dictionary<Rank, int>();
            foreach (Card card in cards)
            {
                if (entries.ContainsKey(card.Rank))
                {
                    entries[card.Rank] += 1;
                }
                else
                {
                    entries.Add(card.Rank, 1);
                }
            }
            return entries;
        }

        static Dictionary<Suit, int> GroupSuits(IList<Card> cards)
        {
            Dictionary<Suit, int> entries = new Dictionary<Suit, int>();
            foreach (Card card in cards)
            {
                if (entries.ContainsKey(card.Suit))
                {
                    entries[card.Suit] += 1;
                }
                else
                {
                    entries.Add(card.Suit, 1);
                }
            }
            return entries;
        }

        static Rank ReturnRankEnum(char rank)
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

        static Suit ReturnSuitEnum(char suit)
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
        static bool IsStraightFlush(IList<Card> combCards, Dictionary<Rank, int> ranks, Dictionary<Suit, int> suits)
        {
            if (IsStraight(combCards, ranks) && IsFlush(combCards, suits))
            {
                //   Console.WriteLine($"{{{combCards[0].Rank} {combCards[1].Rank} {combCards[2].Rank} {combCards[3].Rank} {combCards[4].Rank}}}");
                return true;
            }
            else
                return false;
        }
        static StraightFlush MakeStraightFlush(IList<Card> combCards, Dictionary<Rank, int> ranks, Dictionary<Suit, int> suits)
        {
           Rank highCard = ranks.Keys.Max();
           return new StraightFlush(highCard);
            
        }

        static bool IsFourOfAKind(IList<Card> combCards, Dictionary<Rank, int> ranks)
        {
            if (ranks.ContainsValue(4))
            {
                //   Console.WriteLine($"{{{combCards[0].Rank} {combCards[1].Rank} {combCards[2].Rank} {combCards[3].Rank} {combCards[4].Rank}}}");
                return true;
            }
            return false;
        }

        static FourOfAKind MakeFourOfAKind(IList<Card> combCards, Dictionary<Rank, int> ranks)
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

        static bool IsFullHouse(IList<Card> combCards, Dictionary<Rank, int> ranks, Dictionary<Suit, int> suits)
        {
            if (ranks.Count == 2 && (ranks.ContainsValue(2) && ranks.ContainsValue(3)))
            {
                //  Console.WriteLine($"{{{combCards[0].Rank} {combCards[1].Rank} {combCards[2].Rank} {combCards[3].Rank} {combCards[4].Rank}}}");
                return true;
            }
            return false;
        }

        static FullHouse MakeFullHouse(IList<Card> combCards, Dictionary<Rank, int> ranks, Dictionary<Suit, int> suits)
        {
            Rank highPair = Rank.Value2; // Use unassigment variable
            Rank lowPair = Rank.Value2;
            foreach (KeyValuePair<Rank, int> entry in ranks)
            {
                if (entry.Value == 3)
                { highPair = entry.Key; }
                if (entry.Value == 2)
                {  lowPair = entry.Key; }
            }
            return new FullHouse(highPair, lowPair); 

        }

        static bool IsFlush(IList<Card> combCards, Dictionary<Suit, int> suits)
        {
            if (suits.ContainsValue(5))
            {
                //   Console.WriteLine($"{{{combCards[0].Suit} {combCards[1].Suit} {combCards[2].Suit} {combCards[3].Suit} {combCards[4].Suit}}}");
                return true;
            }
            return false;
        }

        static Flush MakeFlush(IList<Card> combCards, Dictionary<Rank, int> ranks, Dictionary<Suit, int> suits)
        {
            Rank highCard = ranks.Keys.Max();
            return new Flush(highCard);
        }

        static bool IsStraight(IList<Card> combCards, Dictionary<Rank, int> ranks)
        {
            if (ranks.Count == 5)
            {
                List<Rank> sortedRanks = ranks.Keys.ToList();
                sortedRanks.Sort();
                List<Rank> Astraight = new List<Rank>() {Rank.Value2, Rank.Value3, Rank.Value4, Rank.Value5, Rank.ValueA};
                for (int count = 0; count < (sortedRanks.Count - 1); count++)
                {
                    var element1 = sortedRanks[count];
                    var element2 = sortedRanks[count + 1];
                    if (element2 != element1 + 1)
                    {
                        return false;
                    }
                    if (Enumerable.SequenceEqual(sortedRanks, Astraight))
                    {
                        return true;
                    }
                }
                return true;
            }
            return false;
        }

        static Straight MakeStraight(IList<Card> combCards, Dictionary<Rank, int> ranks, Dictionary<Suit, int> suits)
        {
            Rank highCard = ranks.Keys.Max();
            List<Rank> sortedRanks = ranks.Keys.ToList();
            sortedRanks.Sort();
            List<Rank> Astraight = new List<Rank>() {Rank.Value2, Rank.Value3, Rank.Value4, Rank.Value5, Rank.ValueA};
            
            if(Enumerable.SequenceEqual(sortedRanks, Astraight))
            {
                sortedRanks.Remove(highCard);
                sortedRanks.Sort();
                highCard = sortedRanks.Max();
            }
            return new Straight(highCard);

        }

        static bool IsThreeOfAKind(IList<Card> combCards, Dictionary<Rank, int> ranks)
        {
            if (ranks.ContainsValue(3))
            {
                //  Console.WriteLine($"{{{combCards[0].Rank} {combCards[1].Rank} {combCards[2].Rank} {combCards[3].Rank} {combCards[4].Rank}}}");
                return true;
            }
            return false;
        }

        static ThreeOfAKind MakeThreeOfAKind(IList<Card> combCards, Dictionary<Rank, int> ranks, Dictionary<Suit, int> suits)
        {
            Rank highCard = Rank.Value2; // Use unassigment variable
            Rank kicker = Rank.Value2;
    
            foreach (KeyValuePair<Rank, int> entry in ranks)
            {
                if (entry.Value == 3)
                { 
                    highCard = entry.Key;
                    List<Rank> sortedRanks = ranks.Keys.ToList();
                    sortedRanks.Remove(entry.Key);
                    sortedRanks.Sort();
                    kicker = sortedRanks.Max();
                }
            }
             return new ThreeOfAKind(highCard, kicker); 

        }

        static bool IsTwoPairs(IList<Card> combCards, Dictionary<Rank, int> ranks)
        {
            if (ranks.Count == 3 && ranks.ContainsValue(2))
            {
                //  Console.WriteLine($"{{{combCards[0].Rank} {combCards[1].Rank} {combCards[2].Rank} {combCards[3].Rank} {combCards[4].Rank}}}");
                return true;
            }
            return false;
        }

        static TwoPairs MakeTwoPairs(IList<Card> combCards, Dictionary<Rank, int> ranks, Dictionary<Suit, int> suits)
        {
            Rank highPair = Rank.Value2; // Use unassigment variable
            Rank lowPair = Rank.Value2;
            Rank kicker = Rank.Value2;
            List<Rank> highPairs = new List<Rank>();
            foreach (KeyValuePair<Rank, int> entry in ranks)
            {
                
                if (entry.Value == 2) 
                {
                    highPairs.Add(entry.Key);
                    highPairs.Sort();
                    highPair = highPairs.Max();
                    lowPair = highPairs.Min();                 
                }
                if (entry.Value == 1)
                { kicker = entry.Key; }
            }
            return new TwoPairs(highPair, lowPair, kicker); 

        }

        static bool IsPair(IList<Card> combCards, Dictionary<Rank, int> ranks)
        {
            if (ranks.ContainsValue(2))
            {
                // Console.WriteLine($"{{{combCards[0].Rank} {combCards[1].Rank} {combCards[2].Rank} {combCards[3].Rank} {combCards[4].Rank}}}");
                return true;
            }
            return false;
        }

        static Pair MakePair(IList<Card> combCards, Dictionary<Rank, int> ranks, Dictionary<Suit, int> suits)
        {
            Rank highCard = Rank.Value2; // Use unassigment variable
            Rank kicker = Rank.Value2;
            foreach (KeyValuePair<Rank, int> entry in ranks)
            {
                if (entry.Value == 2)
                {   highCard = entry.Key;
                    List<Rank> sortedRanks = ranks.Keys.ToList();
                    sortedRanks.Remove(entry.Key);
                    sortedRanks.Sort();
                    kicker = sortedRanks.Max();
                }
            }
            return new Pair(highCard, kicker);

        }

        static HighCard MakeHighCard(IList<Card> combCards, Dictionary<Rank, int> ranks, Dictionary<Suit, int> suits)
        {
            Rank highCard = ranks.Keys.Max();
            return new HighCard(highCard);

        }
       
        public interface ICombination : IComparable
        {}

        public class StraightFlush : ICombination, IComparable
        {
            public Rank HighCard { get; }

            public StraightFlush(Rank r)
            {
                this.HighCard = r;
            }

            public int CompareTo(object obj)
            {
                StraightFlush compObj = (StraightFlush)obj;

                if (this.HighCard > compObj.HighCard)
                    return 1;

                if (this.HighCard < compObj.HighCard)
                    return -1;

                else
                    return 0;
            }
            public override string ToString()
            {
                return "STRAIGHT FLUSH: RANK -> " + this.HighCard;
            }
        }

        public class  FullHouse: ICombination, IComparable
        {
            public Rank TwoCard { get; }
            public Rank ThreeCard { get; }

            public FullHouse(Rank th, Rank tw)
            {
                this.ThreeCard = th;
                this.TwoCard = tw;
            }

            public int CompareTo(object obj)
            {
                FullHouse compObj = (FullHouse)obj;
                if (this.ThreeCard > compObj.ThreeCard && this.TwoCard > compObj.TwoCard)
                    return 1;

                if (this.ThreeCard < compObj.ThreeCard && this.TwoCard < compObj.TwoCard)
                    return -1;

                else
                    return 0;
            }

            public override string ToString()
            {
                return "FULL HOUSE: THREE CARD -> " + this.ThreeCard +  "TWO CARD ->" + this.TwoCard;
            }

        }

        public class Straight : ICombination, IComparable
        {
           public Rank Rank { get; }

            public Straight(Rank r)
            {
                this.Rank = r;
            }

            public int CompareTo(object obj)
            {
                Straight compObj = (Straight)obj;

                if (this.Rank > compObj.Rank)
                    return 1;

                if (this.Rank < compObj.Rank)
                    return -1;

                else
                    return 0;
            }

            public override string ToString()
            {
                return "STRAIGHT: RANK -> " + this.Rank;
            }
        }



        public class Flush : ICombination, IComparable
        {
            public Rank Rank { get; }

            public Flush(Rank r)
            {
                this.Rank = r;
            }

            public int CompareTo(object obj)
            {
                Flush compObj = (Flush)obj;

                if (this.Rank > compObj.Rank)
                    return 1;

                if (this.Rank < compObj.Rank)
                    return -1;

                else
                    return 0;
            }

            public override string ToString()
            {
                return "FLUSH: RANK -> " + this.Rank;
            }
        }

        public class FourOfAKind : ICombination, IComparable
        {
            public Rank HighCard { get; }
            public Rank Kicker { get; }

            public FourOfAKind(Rank r, Rank k)
            {
                this.HighCard = r;
                this.Kicker = k;
            }

            public int CompareTo(object obj)
            {
                FourOfAKind compObj = (FourOfAKind)obj;
                if (this.HighCard > compObj.HighCard)
                    return 1;

                if (this.HighCard < compObj.HighCard)
                    return -1;

                else
                    return 0;
            }

            public override string ToString()
            {
                return "FOUR OF A KIND: RANK -> " + this.HighCard + " KICKER -> " + this.Kicker;
            }
        }

        public class ThreeOfAKind : ICombination, IComparable
        {
            public Rank HighCard { get; }
            public Rank Kicker { get; }

            public ThreeOfAKind(Rank r, Rank k)
            {
                this.HighCard = r;
                this.Kicker = k;
            }

            public int CompareTo(object obj)
            { return 0; }

            public override string ToString()
            {
                return "THREE OF A KIND: RANK -> " + this.HighCard + " KICKER -> " + this.Kicker;
            }
        }

        public class TwoPairs : ICombination, IComparable
        {
            public Rank HighPair { get; }
            public Rank LowPair { get; }
            public Rank Kicker { get; }

            public TwoPairs(Rank hp, Rank lp, Rank k)
            {
                this.HighPair = hp;
                this.LowPair = lp;
                this.Kicker = k; 
            }

            public int CompareTo(object obj)
            { return 0; }
            public override string ToString()
            {
                return "TWO PAIRS: HIGH PAIR -> " + this.HighPair + " LOW PAIR -> " + this.LowPair + "KICKER -> " + this.Kicker;
            }
        }

        public class Pair : ICombination, IComparable
        {
            public Rank HighPair { get; }
            public Rank Kicker { get; }
            public Pair(Rank hp, Rank k)
            {
                this.HighPair = hp;
                this.Kicker = k;
            }
            public int CompareTo(object obj)
            { return 0; }

            public override string ToString()
            {
                return "PAIR: PAIR RANK -> " + this.HighPair + " KICKER -> " + this.Kicker;
            }
        }

        public class HighCard : ICombination, IComparable
        {
            public Rank Kicker { get; }
            public HighCard(Rank r)
            {
                this.Kicker = r;
            }

            public int CompareTo(Object obj)
            {
                HighCard compObj = (HighCard)obj;

                if (this.Kicker > compObj.Kicker)
                    return 1;

                if (this.Kicker < compObj.Kicker)
                    return -1;

                else
                    return 0;
            }
            public override string ToString()
            {
                return "HIGH CARD -> " + this.Kicker;
            }

        }

        public class CombinationComparer : IComparer<Combination>
        {
            public int Compare(Combination x, Combination y)
            {
                // TODO: Handle x or y being null, or them not having names
                return x.CompareTo(y);
            }
        }

        /*  public static ICombination SortHands(List <Combination> comb)
           {
               foreach (var c in comb)
               {
                   switch (c)
                   {
                       case Combination.StraightFLush:
                           return new StraightFlush(c,);
                       case Combination.FourOfAKind:
                           return new FourOfAKind();
                       case Combination.FullHouse:
                           return new FullHouse();
                       case Combination.Flush:
                           return new Flush();
                       case Combination.Straight:
                           return new Straight();
                       case Combination.ThreeOfAKind:
                           return new ThreeOfAKind();
                       case Combination.TwoPair:
                           return new TwoPair();
                       case Combination.Pair:
                           return new Pair();
                       default:
                           Console.WriteLine("Not found");
                           break;
                   }
               }
               return new HighCard();
           }

           public static char RankToString(Rank r)
             {
                 switch (r)
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
             } */



    }
}
