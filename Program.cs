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

        private static Combination FindHandValue(List<Card> hand, List<Card> board)
        {
            List<Card> availableCards = new List<Card>();
            availableCards.AddRange(hand);
            availableCards.AddRange(board);

            Combinations<Card> variants = new Combinations<Card>(availableCards, 5);

            List<Combination> combinations = new List<Combination>();
            foreach (IList<Card> variant in variants)
            {

                Dictionary<Rank, int> rankGroups = GroupRanks(variant);
                Dictionary<Suit, int> suitGroups = GroupSuits(variant);
                Combination comb = FindCombination(variant, rankGroups, suitGroups);
                combinations.Add(FindCombination(variant, rankGroups, suitGroups));
                Console.WriteLine(String.Join("; ", variant));
                Console.WriteLine(comb);
            }

            Console.WriteLine("Combination " + combinations.Max().ToString());

            return combinations.Max();
        }

        private static Combination FindCombination(IList<Card> cards, Dictionary<Rank, int> ranks, Dictionary<Suit, int> suits)
        {
            if (IsStraightFlush(cards, ranks, suits)) { return Combination.StraightFLush; }
            else if (IsFourOfAKind(cards, ranks)) { return Combination.FourOfAKind; }
            else if (IsFullHouse(cards, ranks, suits)) { return Combination.FullHouse; }
            else if (IsFlush(cards, suits)) { return Combination.Flush; }
            else if (IsStraight(cards, ranks)) { return Combination.Straight; }
            else if (IsThreeOfAKind(cards, ranks)) { return Combination.ThreeOfAKind;}
            else if (IsTwoPairs(cards, ranks)) { return Combination.TwoPair; }
            else if (IsPair(cards, ranks)) { return Combination.Pair; }
            else return Combination.HighCard;
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

        static bool IsStraight(IList<Card> combCards, Dictionary<Rank, int> ranks)
        {
            if (ranks.Count == 5)
            {
                List<Rank> sortedRanks = ranks.Keys.ToList();
                sortedRanks.Sort();
               
                for (int count = 0; count < (sortedRanks.Count - 1); count++)
                {
                    var element1 = sortedRanks[count];
                    var element2 = sortedRanks[count + 1];
                    if (element2 != element1 + 1)
                    {
                        return false;
                    }
                }
                return true;
            } 
            return false;
        }

        static bool IsFlush(IList<Card> combCards, Dictionary<Suit, int> suits)
        {
            if(suits.ContainsValue(5))
            {
             //   Console.WriteLine($"{{{combCards[0].Suit} {combCards[1].Suit} {combCards[2].Suit} {combCards[3].Suit} {combCards[4].Suit}}}");
                return true;
            }
            return false;
        }

        static bool IsStraightFlush(IList<Card> combCards, Dictionary<Rank, int> ranks, Dictionary<Suit, int> suits)
        {
            if (IsStraight(combCards, ranks) && IsFlush(combCards, suits) )
            {
             //   Console.WriteLine($"{{{combCards[0].Rank} {combCards[1].Rank} {combCards[2].Rank} {combCards[3].Rank} {combCards[4].Rank}}}");
                return true;
            }
            else
                return false;
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

        static bool IsFullHouse(IList<Card> combCards, Dictionary<Rank, int> ranks, Dictionary<Suit, int> suits)
        {
            if (ranks.Count == 2 && (ranks.ContainsValue(2) && ranks.ContainsValue(3)))
            {
             //  Console.WriteLine($"{{{combCards[0].Rank} {combCards[1].Rank} {combCards[2].Rank} {combCards[3].Rank} {combCards[4].Rank}}}");
                return true;
            }
            return false;
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

        static bool IsTwoPairs(IList<Card> combCards, Dictionary<Rank, int> ranks)
        {
            if (ranks.Count == 3 && ranks.ContainsValue(2))
            {
              //  Console.WriteLine($"{{{combCards[0].Rank} {combCards[1].Rank} {combCards[2].Rank} {combCards[3].Rank} {combCards[4].Rank}}}");
                return true;
            }
            return false;
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

      /*  public static char RankToString(Rank r)
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
