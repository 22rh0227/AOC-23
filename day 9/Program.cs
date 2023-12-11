using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day_9
{
    internal class Program
    {
        static List<decimal> SplitList(string line)
        {
            List<decimal> sequence = new List<decimal>();
            string currentNum = "";
            for (int i = 0; i < line.Length; i++)
            {
                if (char.IsDigit(line[i]) || line[i] == '-')
                {
                    currentNum += line[i];
                }
                else if (line[i] == ' ')
                {
                    sequence.Add(decimal.Parse(currentNum));
                    currentNum = "";
                }
                if (i == line.Length - 1)
                {
                    sequence.Add(decimal.Parse(currentNum));
                }
            }
            //Console.WriteLine(sequence.Count);
            //Console.ReadKey();
            return sequence;
        }
        static decimal NextStep(List<decimal> sequence)
        {
            List<List<decimal>> nTerm = new List<List<decimal>>();
            nTerm.Add(new List<decimal>());
            for (int i = 0; i < sequence.Count - 1; i++)
            {
                nTerm[0].Add(sequence[i + 1] - sequence[i]);
                //Console.WriteLine(nTerm[0][i]);
                //Console.WriteLine(sequence[i + 1] + " " + sequence[i]);
            }
            //Console.WriteLine("-");
            bool all0s = false;
            for (int j = 1; !all0s; j++)
            {
                if (j == nTerm.Count)
                {
                    nTerm.Add(new List<decimal>());
                }
                all0s = true;
                for (int i = 0; i < nTerm[j - 1].Count - 1; i++)
                {
                    
                    nTerm[j].Add(nTerm[j - 1][i + 1] - nTerm[j - 1][i]);
                    //Console.WriteLine(nTerm[j][i]);
                    if (nTerm[j][i] != 0)
                    {
                        all0s = false;
                    }
                }
                //Console.WriteLine("---------");
            }
            if (!part2)
            {
                nTerm[nTerm.Count - 1].Add(0);
                for (int i = nTerm.Count - 2; i > -1; i--)
                {
                    //Console.WriteLine(nTerm[i][nTerm[i].Count - 1]);
                    //Console.WriteLine(": " + nTerm[i + 1].Count);
                    //Console.WriteLine(nTerm[i + 1][nTerm[i + 1].Count - 1]);
                    nTerm[i].Add(nTerm[i][nTerm[i].Count - 1] + nTerm[i + 1][nTerm[i + 1].Count - 1]);
                    //Console.WriteLine(nTerm[i][nTerm[i].Count - 1]);
                }
                //Console.WriteLine(sequence[sequence.Count - 1] + nTerm[0][nTerm[0].Count - 1]);
                return sequence[sequence.Count - 1] + nTerm[0][nTerm[0].Count - 1];
            }
            else
            {
                for (int i = nTerm.Count - 2; i > -1; i--)
                {
                    //Console.WriteLine(nTerm[i][0] + " - " + nTerm[i + 1][0]);
                    //decimal num = nTerm[i][0] - nTerm[i + 1][0];
                    //Console.WriteLine(nTerm[i][0] - nTerm[i + 1][0]);
                    nTerm[i].Add(nTerm[i][0] - nTerm[i + 1][nTerm[i + 1].Count - 1]);
                    //nTerm[i].Prepend(num);
                    //Console.WriteLine(nTerm[i][nTerm[i].Count - 1]);
                    //Console.WriteLine(": " + nTerm[i]);
                }
                //Console.WriteLine(sequence[0] - nTerm[0][0]);
                return sequence[0] - nTerm[0][nTerm[0].Count - 1];
            }
        }
        static bool part2 = true;
        static void Main(string[] args)
        {
            decimal total = 0;
            List<string> lines = new List<string>();
            using (StreamReader sr = new StreamReader("input.txt"))
            {
                string line;
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    lines.Add(line);
                    //Console.WriteLine(line);
                }
            }
            for (int i = 0; i < lines.Count; i++)
            {
                total += NextStep(SplitList(lines[i]));
                //Console.WriteLine(total);
                //Console.WriteLine("---");
                //if (total < 0)
                //{
                //    Console.WriteLine("awfoiejahwufjneo'wnfeaw");
                //    Console.WriteLine(total);
                //}
                Console.WriteLine("---");
            }
            Console.WriteLine(total);
            Console.ReadLine();
        }
    }
}
