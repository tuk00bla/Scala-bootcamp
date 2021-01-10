using System;
using System.Collections.Generic;
using System.Text;
using Task1.Combinations;

namespace Task
{
    public class Hand
    {
        public CombinationClass Combination { get; set; }
        public List<Card> Cards { get; set; }

        public override string ToString()
        {
            string stringHand = "";
            foreach (var card in Cards)
            {
                stringHand += card.ToString();
            }
        return stringHand;
        }
    }
}
