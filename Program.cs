using System;
using System.Collections.Generic;
using System.Text;


namespace Task1
{
    public class Card
    {
        public Rank rank;
        public Rank Rank
        {
            get { return rank; }
            set { rank = value; }
        }

        public Suit suit;
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

        public static List<Card> FindHandValue(List<Card> cards, List<Card> board)
        {
            Dictionary<Rank, int> entries = new Dictionary<Rank, int>();
            List<Card> maxHandValue = new List<Card>();
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
            foreach (KeyValuePair<Rank, int> keyValue in entries)
            {
                Console.WriteLine(keyValue.Key + " - " + keyValue.Value);
                Console.WriteLine((Rank)(keyValue.Key));
            }
            maxHandValue.AddRange(cards);
            maxHandValue.AddRange(board);
            bool kek = CheckForFlush(maxHandValue, entries);
            Console.Write("kek " + kek);
            return maxHandValue;
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

        static bool CheckForStraight(List<Card> combCards)
        {
            int count = 0;
            for (int i = 0; i < combCards.Count; i++)
            {
                int cardValue = (int)(combCards[i+1].Rank);
                foreach (Card card in combCards)
                {
                    if ((int)(combCards[i].Rank) < cardValue)
                    {
                        if (count == 4)
                        {
                            return true;
                        }
                        count++;
                    }
                }
            }
            return false;
        }

        static bool CheckForFlush(List<Card> combCards, Dictionary<Rank, int> entries)
        {   
            for (int i = 0; i < combCards.Count; i++)
            {
                int count = 0;
                Suit firstSuit = combCards[i].suit;
                foreach (Card card in combCards)
                {
                    if (card.suit == firstSuit)
                    {
                        if (count == 4)
                        {
                            return true;
                        }
                        count++;
                    }
                }
            }
            return false;
        }

        static bool CheckForStraightFlush(List<Card> combCards, Dictionary<Rank, int> entries)
        {
            if (CheckForStraight(combCards) && CheckForFlush(combCards, entries))
            {
                return true;

            }
            else
                return false;
        }
    }
}
