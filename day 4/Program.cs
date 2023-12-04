using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Schema;

namespace day_4
{
    internal class Program
    {
        static List<string> WinningNumbers(string line)
        {
            List<string> winners = new List<string>();
            for (int i = 0; i < 10; i++)
            {
                if (char.IsDigit(line[3 * i + 10]))
                {
                    winners.Add(line[3 * i + 10].ToString() + line[3 * i + 11].ToString());
                }
                else
                {
                    winners.Add(line[3 * i + 11].ToString());
                }
            }
            return winners;
        }
        static int CardPoints(List<string> goodNumbers, string line)
        {
            line = line.Substring(line.IndexOf('|'));
            int points = 0;
            for (int i = 0; i < goodNumbers.Count; i++)
            {
                if (goodNumbers[i].Length == 1)
                {
                    for (int j = 0; j < 25; j++)
                    {
                        if (line[3 * j + 2] == ' ')
                        {
                            if (line[3 * j + 3].ToString() == goodNumbers[i])
                            {
                                points++;
                            }
                        }
                    }
                }
                else if (line.Contains(goodNumbers[i]))
                {
                    points++;
                }
            }
            return points;
        }
        static int TotalPoints(int currentPoints)
        {
            return (int)Math.Pow(2, currentPoints - 1);
        }
        static void CardRepeats(List<string> cards)
        {
            int winnings;
            double total = 0;
            List<int> numCopies = new List<int>();
            for (int i = 0; i < 198; i++)
            {
                numCopies.Add(1);
            }
            for (int i = 0; i < cards.Count; i++)
            {
                winnings = CardPoints(WinningNumbers(cards[i]), cards[i]);
                for (int j = 1; j < winnings + 1 && j + i < cards.Count; j++)
                {
                    numCopies[i + j] += numCopies[i];
                }
            }
            total = numCopies.Sum();
            Console.WriteLine("number of scratch cards: " + total);
        }
        static void Main(string[] args)
        {
            int total = 0;
            using (StreamReader sr = new StreamReader("input.txt"))
            {
                string line;
                List<string> cards = new List<string>();
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    cards.Add(line);
                    total += TotalPoints(CardPoints(WinningNumbers(line), line));
                    
                }
                CardRepeats(cards);
            }
            Console.WriteLine("total points: " + total);
            Console.ReadLine();
        }
    }
}
