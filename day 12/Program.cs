using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day_12
{
    internal class Program
    {
        static List<List<string>> Springs(List<string> lines)
        {
            List<List<string>> springRows = new List<List<string>>();
            string currSpring = "";
            for (int r = 0; r < lines.Count; r++)
            {
                springRows.Add(new List<string>());
                for (int c = 0; c < lines[r].Length; c++)
                {
                    if (lines[r][c] == '#' || lines[r][c] == '?')
                    {
                        currSpring += lines[r][c];
                    }
                    else
                    {
                        springRows[r].Add(currSpring);
                    }
                }
            }
            return springRows;
        }
        static List<List<int>> SpringWidths(List<string> lines)
        {
            List<List<int>> springLens = new List<List<int>>();
            string currLens;

            for (int r = 0; r < lines.Count; r++)
            {
                currLens = lines[r].Substring(lines[r].IndexOf(' ') + 1);
                springLens.Add(currLens.Split(',').Select(x => int.Parse(x)).ToList());
            }
            return springLens;
        }
        static int CheckPositions()
        {
            for (int i = 0; i < springLens.Count; i++)
            {
                if (i == 0)
                {
                    pos.Add(springLens[i] - 1);
                }
                if (line[springLens[i] + pos[i - 1] + springLens[i - 1] - 1] == '.')
                {
                    continue;
                }
                pos.Add(springLens[i] + pos[i - 1] + springLens[i - 1] - 1);

            }
        }
        static int Permutations(List<string> springs, List<int> springLens)
        {
            int totalPerms = 1;
            string line = "";
            for (int i = 0; i < springs.Count; i++)
            {
                line += springs[i];
                line += '.';
            }
            List<int> pos = new List<int>();
            for (int i = 0; i < springLens.Count; i++)
            {
                if (i == 0)
                {
                    pos.Add(springLens[i] - 1);
                }
                if (line[springLens[i] + pos[i - 1] + springLens[i - 1] - 1] == '.')
                {
                    continue;
                }
                pos.Add(springLens[i] + pos[i - 1] + springLens[i - 1] - 1);
                
            }
            for (int i = 0; pos[pos.Count - 1] < line.Length; i++)
            {

            }















            if (springs.Count == springLens.Count)
            {
                for (int i = 0; i < springs.Count; i++)
                {
                    totalPerms *= springs[i].Length - springLens[i] + 1;
                }
                return totalPerms;
            }
            else if (springs.Count > springLens.Count)
            {

            }
        }
        static void Main(string[] args)
        {
            List<string> lines = new List<string>();
            long total = 0;
            using (StreamReader sr = new StreamReader("input.txt"))
            {
                string line;
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    lines.Add(line);
                }
            }
            List<List<string>> springRows = Springs(lines);
            List<List<int>> springLens = SpringWidths(lines);
            for (int i = 0; i < springLens.Count; i++)
            {
                total += Permutations(springRows[i], springLens[i]);
            }
            Console.WriteLine("rememeber to check for #'s");
            Console.ReadLine();
        }
    }
}
