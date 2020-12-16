using System;
using System.Collections.Generic;
using System.Text;
using Combinatorics.Collections;
using System.Linq;


namespace Task1
{
    public class Card
    {
        private Rank rank;
        public Rank Rank
        {
            get { return rank; }
            set { rank = value; }
        }

        private Suit suit;
        public Suit Suit
        {
            get { return suit; }
            set { suit = value; }
        }

        public Card(Rank r, Suit s)
        {
            this.rank = r;
            this.suit = s;
        }
    }
    
  public  enum Rank
  {
       value2,
       value3,
       value4,
       value5,
       value6,
       value7,
       value8,
       value9,
       valueT,
       valueJ,
       valueQ, 
       valueK, 
       valueA 
  }

   public enum Suit
   {
        valueH,
        valueD,
        valueC,
        valueS,
   }

   public enum Combination
    {
        HIGH_CARD,
        PAIR,
        TWO_PAIRS,
        THREE_OF_A_KIND,
        STRAIGHT,
        FLUSH,
        FULL_HOUSE,
        FOUR_OF_A_KIND,
        STRAIGHT_FLUSH
    }
    class Program
    {
        static void Main(string[] args)
        {
            string enter;
            Console.WriteLine("Please enter a string:");
            enter = Console.ReadLine();
            CheckEnter(enter);
            Console.WriteLine(enter);
            Console.ReadKey();
        }

        static string CheckEnter(string checkedString)
        {
            string[] tokens;
            tokens = checkedString.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
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

        public static List<Card> ParseCards(string hand)
        {
            List<Card> handList = new List<Card>();
            for (int i = 0; i < hand.Length; i += 2)
            {
                Card newCard = new Card(ReturnRankEnum(hand[i]), ReturnSuitEnum(hand[i + 1]));
                handList.Add(newCard);
            }
            return handList;
        }

        public static Combination FindHandValue(List<Card> hand, List<Card> board)
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

                combinations.Add(FindCombination(variant, rankGroups, suitGroups));
            }
            foreach (var combination in combinations)
            {
                Console.Write("Combination " + combination.ToString());
            }

            return combinations[0];
        }

       public static Combination FindCombination(IList<Card> cards, Dictionary<Rank, int> ranks, Dictionary<Suit, int> suits)
        {
            if (IsStraightFlush(cards, ranks, suits))
                { 
                return Combination.STRAIGHT_FLUSH; 
                }
            else if (IsFourOfAKind(cards, ranks))
                { 
                return Combination.FOUR_OF_A_KIND; 
                }
            else if (IsFullHouse(cards, ranks, suits))
                { 
                return Combination.FULL_HOUSE; 
                }
            else if (IsFlush(cards, suits))
                { 
                return Combination.FLUSH; 
                }
            else if (IsStraight(cards, ranks))
                { 
                return Combination.STRAIGHT; 
                }
            else if (IsThreeOfAKind(cards, ranks))
                { 
                return Combination.THREE_OF_A_KIND; 
                }
            else if (IsTwoPairs(cards, ranks))
                { 
                return Combination.TWO_PAIRS; 
                }
            else if (IsPair(cards, ranks))
                { 
                return Combination.PAIR; 
                }
            else return Combination.HIGH_CARD;
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
           Rank returnRank = Rank.value2;
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
            Suit returnSuit = Suit.valueC;
            switch (suit)
            {
                case 'h':
                    returnSuit = Suit.valueH;
                    break;
                case 'd':
                    returnSuit = Suit.valueD;
                    break;
                case 'c':
                    returnSuit = Suit.valueC;
                    break;
                case 's':
                    returnSuit = Suit.valueS;
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
                int comb = 1;
                for (int count = 0; count < (ranks.Count - 1); count++)
                {
                    var element1 = ranks.ElementAt(count);
                    var element2 = ranks.ElementAt(count + 1);
                    if (element2.Key == (element1.Key + 1))
                    {  
                        if (comb == 5)
                        {
                            Console.WriteLine(String.Format("{{{0} {1} {2} {3} {4}}}", combCards[0].Rank, combCards[1].Rank, combCards[2].Rank, combCards[3].Rank, combCards[4].Rank));
                            return true;
                        }
                        comb++;
                    }
                }
           }
            return false;
        }

        static bool IsFlush(IList<Card> combCards, Dictionary<Suit, int> suits)
        {
            if(suits.ContainsValue(5))
            {
                Console.WriteLine(String.Format("{{{0} {1} {2} {3} {4}}}", combCards[0].Suit, combCards[1].Suit, combCards[2].Suit, combCards[3].Suit, combCards[4].Suit));
                return true;
            }
            return false;
        }

        static bool IsStraightFlush(IList<Card> combCards, Dictionary<Rank, int> ranks, Dictionary<Suit, int> suits)
        {
            if (IsStraight(combCards, ranks) && IsFlush(combCards, suits) )
            {
                Console.WriteLine(String.Format("{{{0} {1} {2} {3} {4}}}", combCards[0].Rank, combCards[1].Rank, combCards[2].Rank, combCards[3].Rank, combCards[4].Rank));
                return true;
            }
            else
                return false;
        }

        static bool IsFourOfAKind(IList<Card> combCards, Dictionary<Rank, int> ranks)
        {
            if (ranks.ContainsValue(4))
            {
                Console.WriteLine(String.Format("{{{0} {1} {2} {3} {4}}}", combCards[0].Rank, combCards[1].Rank, combCards[2].Rank, combCards[3].Rank, combCards[4].Rank));
                return true;
            }
            return false;
        }

        static bool IsFullHouse(IList<Card> combCards, Dictionary<Rank, int> ranks, Dictionary<Suit, int> suits)
        {
            if (ranks.Count == 2 && (ranks.ContainsValue(2) && ranks.ContainsValue(3)))
            {
                Console.WriteLine(String.Format("{{{0} {1} {2} {3} {4}}}", combCards[0].Rank, combCards[1].Rank, combCards[2].Rank, combCards[3].Rank, combCards[4].Rank));
                return true;
            }
            return false;
        }

        static bool IsThreeOfAKind(IList<Card> combCards, Dictionary<Rank, int> ranks)
        {
            if (ranks.ContainsValue(3))
            {
                Console.WriteLine(String.Format("{{{0} {1} {2} {3} {4}}}", combCards[0].Rank, combCards[1].Rank, combCards[2].Rank, combCards[3].Rank, combCards[4].Rank));
                return true;
            }
            return false;
        }

        static bool IsTwoPairs(IList<Card> combCards, Dictionary<Rank, int> ranks)
        {
            if (ranks.Count == 3 && ranks.ContainsValue(2))
            {
                Console.WriteLine(String.Format("{{{0} {1} {2} {3} {4}}}", combCards[0].Rank, combCards[1].Rank, combCards[2].Rank, combCards[3].Rank, combCards[4].Rank));
                return true;
            }
            return false;
        }

        static bool IsPair(IList<Card> combCards, Dictionary<Rank, int> ranks)
        {
            if (ranks.ContainsValue(2))
            {
                Console.WriteLine(String.Format("{{{0} {1} {2} {3} {4}}}", combCards[0].Rank, combCards[1].Rank, combCards[2].Rank, combCards[3].Rank, combCards[4].Rank));
                return true;
            }
            return false;
        }

    }
}
