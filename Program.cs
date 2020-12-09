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
                    List<Card> newCards = new List<Card>();
                    for (int i = 2; i < tokens.Length; i++)
                    {
                        newCards = ParseCards(tokens[i]);
                        hands.Add(newCards);
                    }
                    foreach (var hand in hands)
                    {
                        FindHandValue(hand);
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

        public static List<Card> FindHandValue(List<Card> cards)
        {
            Dictionary<char, int> entries = new Dictionary<char, int>();
            List<Card> handList = new List<Card>();
            foreach (Card card in cards)
            {
                entries.Add(card.Rank, 0);  ///if char repeats, program stops
            }
            foreach (Card card in cards) 
            {
                if (card.rank != entries[card.Rank])
                    {
                    entries[card.rank] = 1;
                    } 
                else
                    {
                    entries[card.rank] += 1;
                    }
            }
            foreach (KeyValuePair<char, int> keyValue in entries)
            {
                Console.WriteLine(keyValue.Key + " - " + keyValue.Value);
            }
            return handList;
        }

    }
}
