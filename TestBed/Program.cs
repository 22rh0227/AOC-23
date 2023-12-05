using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Assessments
{
    class Program
    {
        struct range
        {
            public double start1;
            public double start2;
            public double end1;
            public double end2;
        }
        static List<List<range>> maps = new List<List<range>>();
        static void FindingFunction(string line, string process)
        {
            switch (process)
            {
                case "seed-to-soil map:":
                    FindingRanges(line, 0);
                    break;
                case "soil-to-fertilizer map:":
                    FindingRanges(line, 1);
                    break;
                case "fertilizer-to-water map:":
                    FindingRanges(line, 2);
                    break;
                case "water-to-light map:":
                    FindingRanges(line, 3);
                    break;
                case "light-to-temperature map:":
                    FindingRanges(line, 4);
                    break;
                case "temperature-to-humidity map:":
                    FindingRanges(line, 5);
                    break;
                case "humidity-to-location map:":
                    FindingRanges(line, 6);
                    break;
            }
        }
        static void FindingRanges(string line, int indexList)
        {
            range theRange = new range();
            theRange.start1 = double.Parse(line.Substring(0, line.IndexOf(' ')));
            theRange.start2 = double.Parse(line.Substring(line.IndexOf(' ') + 1, line.Substring(line.IndexOf(' ') + 1).IndexOf(' ')));
            theRange.end1 = double.Parse(line.Substring(line.LastIndexOf(' ') + 1)) + theRange.start1 - 1;
            theRange.end2 = double.Parse(line.Substring(line.LastIndexOf(' ') + 1)) + theRange.start2 - 1;
            maps[indexList].Add(theRange);
        }
        static List<double> StartingSeeds(string line)
        {
            string currentSeed = "";
            List<double> seeds = new List<double>();
            for (int i = 0; i < line.Length; i++)
            {
                if (char.IsDigit(line[i]))
                {
                    currentSeed += line[i];
                }
                else if (line[i] == ' ' && currentSeed != "")
                {
                    seeds.Add(double.Parse(currentSeed));
                    currentSeed = "";
                }
                if (i == line.Length - 1)
                {
                    seeds.Add(double.Parse(currentSeed));
                }
            }
            return seeds;
        }
        static List<List<double>> AllSeeds(string line)
        {
            int seedRange = 0;
            string currentSeed = "";
            string currentRange = "";
            List<List<double>> seeds = new List<List<double>>() { new List<double>(), new List<double>() };
            for (int i = 0; i < line.Length; i++)
            {
                if (char.IsDigit(line[i]))
                {
                    seedRange++;
                }
                if (seedRange % 2 == 0)
                {
                    if (char.IsDigit(line[i]))
                    {
                        currentRange += line[i];
                    }
                    else if (line[i] == ' ' && currentRange != "")
                    {
                        seeds[1].Add(double.Parse(currentSeed) + double.Parse(currentRange) - 1);
                        currentRange = "";
                    }
                    if (i == line.Length - 1)
                    {
                        seeds[1].Add(double.Parse(currentSeed) + double.Parse(currentRange) - 1);
                    }
                }
                else
                {
                    if (char.IsDigit(line[i]))
                    {
                        currentSeed += line[i];
                    }
                    else if (line[i] == ' ' && currentSeed != "")
                    {
                        seeds[0].Add(double.Parse(currentSeed));
                        currentSeed = "";
                    }
                }
            }
            return seeds;
        }
        static double LowestInRange(List<double> seeds)
        {
            for (int i = 0; i < maps.Count; i++)
            {
                for (int k = 0; k < seeds.Count; k++)
                {
                    for (int j = 0; j < maps[i].Count; j++)
                    {
                        if (seeds[k] >= maps[i][j].start2 && seeds[k] <= maps[i][j].end2)
                        {
                            double aboveBound = seeds[k] - maps[i][j].start2;
                            seeds[k] = maps[i][j].start1 + aboveBound;
                            break;
                        }
                    }
                }
            }
            return LowestLocation(seeds);
        }
        static double LowestInBigRange(List<List<double>> seeds)
        {
            for (int i = 0; i < maps.Count; i++)
            {
                for (int k = 0; k < seeds[0].Count; k++)
                {
                    for (int j = 0; j < maps[i].Count; j++)
                    {
                        if (seeds[0][k] >= maps[i][j].start2 && seeds[1][k] <= maps[i][j].end2)
                        {
                            double aboveBound = seeds[0][k] - maps[i][j].start2;
                            double belowBound = maps[i][j].end2 - seeds[1][k];
                            seeds[0][k] = maps[i][j].start1 + aboveBound;
                            seeds[1][k] = maps[i][j].end1 - belowBound;
                            break;
                        }
                        else if (seeds[0][k] >= maps[i][j].start2 && seeds[0][k] <= maps[i][j].end2)
                        {
                            seeds[1].Add(seeds[1][k]);
                            for (int m = 0; m < maps[i].Count; m++)
                            {
                                if (seeds[1][k] >= maps[i][j].start2 && seeds[1][k] <= maps[i][j].end2)
                                {
                                    double aboveBound = seeds[1][k] - maps[i][j].start2;
                                    seeds[0].Add(maps[i][j].start1 + aboveBound);
                                    break;
                                }
                            }
                            seeds[1][k] = maps[i][j].end2;
                            break;
                        }
                    }
                }
            }
            return BigLowestLocation(seeds);
        }
        static double BigLowestLocation(List<List<double>> allSeeds)
        {
            List<double> lowests = new List<double>();
            for (int i = 0; i < allSeeds.Count; i++)
            {
                lowests.Add(allSeeds[i].Min());
            }
            return lowests.Min();
        }
        static double LowestLocation(List<double> lowests)
        {
            return lowests.Min();
        }
        static void Main(string[] args)
        {
            for (int i = 0; i < 7; i++)
            {
                maps.Add(new List<range>());
            }


            using (StreamReader sr = new StreamReader("input.txt"))
            {
                string line = "";
                string process = "";
                line = sr.ReadLine();
                List<double> seedsOld = StartingSeeds(line);
                List<List<double>> allSeeds = AllSeeds(line);

                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    if (line == "")
                    {
                        continue;
                    }
                    else if (char.IsDigit(line[0]))
                    {
                        FindingFunction(line, process);
                    }
                    else
                    {
                        process = line;
                    }
                }
                Console.WriteLine("Part 1: " + LowestInRange(seedsOld));
                Console.WriteLine("Part 2: " + LowestInBigRange(allSeeds));
            }
            Console.ReadLine();
        }


    }
}
