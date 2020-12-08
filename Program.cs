using System;
using System.Collections.Generic;
using System.Text;


namespace Task1
{
    public class Card
    {
        public char rank;
        public char Rank
        {
            get { return rank; }
            set { rank = value; }
        }

        public char suit;
        public char Suit
        {
            get { return suit; }
            set { suit = value; }
        }

        public Card(char r, char s)
        {
            this.rank = r;
            this.suit = s;
        }
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
                    for (int i = 1; i < tokens.Length; i++)
                    {
                        List<Card> newCards = new List<Card>();
                        newCards = ParseCards(tokens[i]); 
                        hands.Add(newCards);
                    }
                    Console.WriteLine(hands.Count);
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

        static string CheckSubStrings(string subStr)
        {
            while (subStr.Length > 10)
            {
                Console.WriteLine("You entered the wrong number of characters, please re-enter");
                subStr = Console.ReadLine();
            }
            char[] cardRank = { 'A', 'K', 'Q', 'J', 'T', '9', '8', '7', '6', '5', '4', '3', '2' };
            char[] cardSuit = { 'h', 'd', 'c', 's' };
            StringBuilder sb = new StringBuilder(subStr.Length / 2 + 1);

            for (int i = 0; i < subStr.Length; i += 2)  // Card Rank
            {
                foreach (char item in cardRank)
                {
                    if (subStr[i] == item)
                    {
                        sb.Append(subStr[i]);
                    }
                }

            }
            string resultRank = sb.ToString(); // string Rank

            StringBuilder sb2 = new StringBuilder(subStr.Length / 2 + 1);

            for (int i = 1; i < subStr.Length; i += 1)  // Card Suit
            {
                foreach (char item in cardSuit)
                {
                    if (subStr[i] == item)
                    {
                        sb2.Append(subStr[i]);
                    }
                }
            }
            string resultSuit = sb2.ToString(); // string suit
            return subStr;
        }

        public static List<Card> ParseCards(string hand)
        {
            List<Card> handList = new List<Card>();
            for (int i = 0; i < hand.Length; i += 2)
            {
                Card newCard = new Card(hand[i], hand[i + 1]);
                handList.Add(newCard);
            }
                return handList;
        }

    }
}
