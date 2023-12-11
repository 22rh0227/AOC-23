using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace day_11
{
    internal class Program
    {
        struct Point
        {
            public int x, y;
        }

        static void AddingCols(List<string> lines, List<int> spaceCols)
        {
            for (int i = 0; i < lines[0].Length; i++)
            {
                for (int j = 0; j < lines.Count; j++)
                {
                    if (lines[j][i] == '#')
                    {
                        break;
                    }
                    if (j == lines.Count - 1)
                    {
                        spaceCols.Add(i);
                    }
                }
            }
        }
        static List<Point> GalaxyLocations(List<string> lines, List<int> spaceRows, List<int> spaceCols)
        {
            List<Point> galaxies = new List<Point>();
            int rowsPassed = 0;
            for (int i = 0; i < lines.Count; i++)
            {
                if (rowsPassed < spaceRows.Count && i > spaceRows[rowsPassed])
                {
                    rowsPassed++;
                }
                if (lines[i].Contains('#'))
                {
                    string afterGalaxy = lines[i].Substring(0, lines[i].IndexOf('#'));
                    int colsPassed = 0;
                    for (int j = 0; j < lines[i].Length; j++)
                    {
                        if (colsPassed < spaceCols.Count && j > spaceCols[colsPassed])
                        {
                            colsPassed++;
                        }
                        if (lines[i][j] == '#')
                        {
                            if (!part2)
                            {
                                galaxies.Add(new Point { x = j + colsPassed, y = i + rowsPassed });
                            }
                            else
                            {
                                galaxies.Add(new Point { x = j + colsPassed * 999999, y = i + rowsPassed * 999999 });
                            }
                        }
                    }
                }
            }
            return galaxies;
        }
        static long ShortPathPairs(List<string> lines, List<Point> galaxies)
        {
            Point galaxy = galaxies[0];
            galaxies.RemoveAt(0);
            long totalDistances = 0;
            Point currPoint = galaxy;
            for (int i = 0; i < galaxies.Count; i++)
            {
                totalDistances += Math.Abs(galaxies[i].x - galaxy.x);
                totalDistances += Math.Abs(galaxies[i].y - galaxy.y);
            }
            return totalDistances;
        }
        static bool part2 = true;
        static void Main(string[] args)
        {
            List<int> spaceRows = new List<int>();
            List<int> spaceCols = new List<int>();
            List<string> lines = new List<string>();
            using (StreamReader sr = new StreamReader("input.txt"))
            {
                string line;
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    lines.Add(line);
                    if (!line.Contains('#'))
                    {
                        spaceRows.Add(lines.Count - 1);
                    }
                }

            }
            AddingCols(lines, spaceCols);
            List<Point> galaxies = GalaxyLocations(lines, spaceRows, spaceCols);

            for (int i = 0; i < lines.Count; i++)
            {
                //Console.WriteLine(lines[i]);
            }
            long total = 0;
            int numOfGals = galaxies.Count - 1;
            for (int i = 0; i < numOfGals; i++)
            {
                //Console.WriteLine(": " + galaxies[0].x + " " + galaxies[0].y);
                total += ShortPathPairs(lines, galaxies);
                //Console.WriteLine("total: " + total);
            }
            Console.WriteLine(total);
            Console.ReadLine();
        }
    }
}
