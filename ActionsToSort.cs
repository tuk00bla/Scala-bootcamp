    using System;
    using System.Collections.Generic;
    using System.Text;
    using Combinatorics.Collections;
    using System.Linq;
    using Task1.Combinations;

    namespace Task
    {
        class ActionsToSort
        {
            public static void SortCards(string checkedString)
            {
                var tokens = checkedString.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                switch (tokens[0])
                {
                    case "texas-holdem":
                        List<Card> board = ParseCards(tokens[1]);
                        var hands = new List<List<Card>>();
                        for (int i = 2; i < tokens.Length; i++)
                        {
                            List<Card> newCards = ParseCards(tokens[i]);
                            hands.Add(newCards);
                        }
                        var pairTxsHands = new List<Hand>();
                        foreach (var hand in hands)
                        {
                            var combination = FindTexasHandValue(hand, board);
                            var handObj = new Hand
                            {
                                Cards = hand,
                                Combination = combination
                            };
                            pairTxsHands.Add(handObj);
                        }

                        var query = pairTxsHands.GroupBy(combination => combination.Combination).OrderBy(z => z.Key).ToList();

                        string strTexasHands = string.Empty;
                        foreach (var handDict in query)
                        {
                            List<string> handsList = handDict.ToList().Select(x => x.ToString()).ToList();
                            handsList.Sort();
                            Console.Write(String.Join("=", handsList));
                            Console.Write(" ");
                        }
                        Console.WriteLine();
                        break;
                    case "omaha-holdem":
                        List<Card> omhBoard = ParseCards(tokens[1]);
                        var omhHands = new List<List<Card>>();
                        for (int i = 2; i < tokens.Length; i++)
                        {
                            List<Card> newCards = ParseCards(tokens[i]);
                            omhHands.Add(newCards);
                        }
                        var pairOmahaHands = new List<Hand>();
                        foreach (var hand in omhHands)
                        {
                            var combination = FindOmahaHandValue(hand, omhBoard);
                            var handObj = new Hand
                            {
                                Cards = hand,
                                Combination = combination
                            };
                            pairOmahaHands.Add(handObj);
                        }
                        var queryOmaha = pairOmahaHands.GroupBy(combination => combination.Combination).OrderBy(z => z.Key).ToList();
                        string strOmahaHands = string.Empty;
                        foreach (var handDict in queryOmaha)
                        {
                            List<string> handsList = handDict.ToList().Select(x => x.ToString()).ToList();
                            handsList.Sort();
                            Console.Write(String.Join("=", handsList));
                            Console.Write(" ");
                        }
                        Console.WriteLine();
                        break;
                    case "five-card-draw":
                        var fiveCardsList = new List<List<Card>>();
                        for (int i = 1; i < tokens.Length; i++)
                        {
                            List<Card> newCards = ParseCards(tokens[i]);
                            fiveCardsList.Add(newCards);
                        }
                        var pairFcdHands = new List<Hand>();
                        foreach (var hand in fiveCardsList)
                        {
                            var combination = FindFiveCardsHandValue(hand);
                            var handObj = new Hand
                            {
                                Cards = hand,
                                Combination = combination
                            };
                            pairFcdHands.Add(handObj);
                        }
                        var queryFcd = pairFcdHands.GroupBy(combination => combination.Combination).OrderBy(z => z.Key).ToList();
                        string strFcdHands = string.Empty;
                        foreach (var handDict in queryFcd)
                        {
                            List<string> handsList = handDict.ToList().Select(x => x.ToString()).ToList();
                            handsList.Sort();
                            Console.Write(String.Join("=", handsList));
                            Console.Write(" ");
                        }
                        Console.WriteLine();
                        break;
                    default:
                        break;
                }
            }

            private static List<Card> ParseCards(string hand)
            {
                List<Card> handList = new List<Card>();
                for (int i = 0; i < hand.Length; i += 2)
                {
                    var newCard = new Card(Card.ReturnRankEnum(hand[i]), Card.ReturnSuitEnum(hand[i + 1]));
                    handList.Add(newCard);
                }
                return handList;
            }

            private static CombinationClass FindTexasHandValue(List<Card> hand, List<Card> board)
            {
                List<Card> availableCards = new List<Card>();
                availableCards.AddRange(hand);
                availableCards.AddRange(board);

                Combinations<Card> variants = new Combinations<Card>(availableCards, 5);

                List<CombinationClass> combinations = new List<CombinationClass>();
                foreach (IList<Card> variant in variants)
                {

                    Dictionary<Rank, int> rankGroups = GroupRanks(variant);
                    Dictionary<Suit, int> suitGroups = GroupSuits(variant);
                    CombinationClass comb = FindCombination(variant, rankGroups, suitGroups);
                    combinations.Add(FindCombination(variant, rankGroups, suitGroups));
                }
                combinations.Sort();
                return combinations.Last();
            }

            private static CombinationClass FindOmahaHandValue(List<Card> hand, List<Card> board)
            {
                List<Card> availableCards = new List<Card>();
                var variantsOfHand = new Combinations<Card>(hand, 2);
                var variantsOfBoard = new Combinations<Card>(board, 3);

                List<CombinationClass> combinations = new List<CombinationClass>();

                foreach (IList<Card> variantOfTwo in variantsOfHand)
                {
                    foreach (IList<Card> variantOfThree in variantsOfBoard)
                    {
                        var variantThreeList = variantOfThree.ToList();
                        var variantTwoList = variantOfTwo.ToList();
                        variantThreeList.AddRange(variantTwoList);
                        Dictionary<Rank, int> rankGroups = GroupRanks(variantThreeList);
                        Dictionary<Suit, int> suitGroups = GroupSuits(variantThreeList);
                        combinations.Add(FindCombination(variantThreeList, rankGroups, suitGroups));
                    }
                }
                combinations.Sort();
                return combinations.Last();
            }

            private static CombinationClass FindFiveCardsHandValue(List<Card> hand)
            {
                List<Card> availableCards = new List<Card>();
                availableCards.AddRange(hand);
                Combinations<Card> variants = new Combinations<Card>(availableCards, 5);

                List<CombinationClass> combinations = new List<CombinationClass>();
                foreach (IList<Card> variant in variants)
                {

                    Dictionary<Rank, int> rankGroups = GroupRanks(variant);
                    Dictionary<Suit, int> suitGroups = GroupSuits(variant);
                    CombinationClass comb = FindCombination(variant, rankGroups, suitGroups);
                    combinations.Add(FindCombination(variant, rankGroups, suitGroups));
                }
                combinations.Sort();
                return combinations.Last();
            }

            private static CombinationClass FindCombination(IList<Card> cards, Dictionary<Rank, int> ranks, Dictionary<Suit, int> suits)
            {
                if (StraightFlush.IsStraightFlush(cards, ranks, suits)) { return StraightFlush.MakeStraightFlush(cards, ranks); }
                else if (FourOfAKind.IsFourOfAKind(cards, ranks)) { return FourOfAKind.MakeFourOfAKind(cards, ranks); }
                else if (FullHouse.IsFullHouse(cards, ranks, suits)) { return FullHouse.MakeFullHouse(cards, ranks); }
                else if (Flush.IsFlush(cards, suits)) { return Flush.MakeFlush(cards, ranks); }
                else if (Straight.IsStraight(cards, ranks)) { return Straight.MakeStraight(cards, ranks); }
                else if (ThreeOfAKind.IsThreeOfAKind(cards, ranks)) { return ThreeOfAKind.MakeThreeOfAKind(cards, ranks); }
                else if (TwoPairs.IsTwoPairs(cards, ranks)) { return TwoPairs.MakeTwoPairs(cards, ranks); }
                else if (Pair.IsPair(cards, ranks)) { return Pair.MakePair(cards, ranks); }
                else return HighCard.MakeHighCard(cards, ranks);
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
        }
}
