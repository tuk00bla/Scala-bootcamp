using System;




namespace Task
{
    static class Program
    {
        static void Main(string[] args)
        {
            string line;
            while ((line = Console.ReadLine()) != null)
            {
                ActionsToSort.SortCards(line);
            }
        }
    }
}
