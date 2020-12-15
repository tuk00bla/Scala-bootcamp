using System;
using System.Collections.Generic;
using System.Text;
using Combinatorics.Collections;


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

        public static List<Card> FindHandValue(List<Card> hand, List<Card> board)
        {
            Dictionary<Rank, int> entriesR = new Dictionary<Rank, int>();
            Dictionary<Suit, int> entriesS = new Dictionary<Suit, int>();
            List<Card> maxHandValue = new List<Card>();
            maxHandValue.AddRange(hand);
            maxHandValue.AddRange(board);
            Combinations<Card> combinations = new Combinations<Card>(maxHandValue, 5);
            string cformat = "Combinations of cards choose 5: size = {0}";
            Console.WriteLine(String.Format(cformat, combinations.Count));
            /*  for (int i = 0; i < combinations.Count; i++)
              {

                  for (int j = 0; i < combinations[i].Count; j++)
                  {      
                            if (entriesR.ContainsKey(c[i].Rank))  //КОД НИЖИ НЕ ДОЛЖЕН РАБОТАТЬ С МАХХЭНДВЭЛЬЮ
                            {
                                entriesR[c[i].Rank] += 1;
                            }
                            else
                            {
                                entriesR.Add(c[i].Rank, 1);
                            }
                  }
              }  */

            foreach (IList<Card> c in combinations)
            {
                    Console.WriteLine(String.Format("{{{0} {1} {2} {3} {4}}}", c[0].Rank, c[1].Rank, c[2].Rank, c[3].Rank, c[4].Rank));
                    bool kek = CheckForFlush(c);
                    bool cec = CheckForStraight(c);
                    Console.WriteLine("kek " + kek);
                    Console.WriteLine("cec " + cec);
            }
            
            foreach (Card card in maxHandValue)
            {
                if (entriesR.ContainsKey(card.Rank))
                {
                    entriesR[card.Rank] += 1;
                }
                else
                {
                    entriesR.Add(card.Rank, 1);
                }
            }
            foreach (Card card in maxHandValue)
            {
                if (entriesS.ContainsKey(card.Suit))
                {
                    entriesS[card.Suit] += 1;
                }
                else
                {
                    entriesS.Add(card.Suit, 1);
                }
            }
            foreach (KeyValuePair<Rank, int> keyValue in entriesR)
                      {
                          Console.WriteLine("RANKS");
                          Console.WriteLine(keyValue.Key + " - " + keyValue.Value);
                          Console.WriteLine((Rank)(keyValue.Key));
                     }

            foreach (KeyValuePair<Suit, int> keyValue in entriesS)
            {
                Console.WriteLine("SUITS");
                Console.WriteLine(keyValue.Key + " - " + keyValue.Value);
                Console.WriteLine((Suit)(keyValue.Key));
            }
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

        static bool CheckForStraight(IList<Card> combCards)
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

        static bool CheckForFlush(IList<Card> combCards)
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
            if (CheckForStraight(combCards) && CheckForFlush(combCards) )
            {
                return true;

            }
            else
                return false;
        }
    }
}
