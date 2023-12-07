using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.AccessControl;
using System.IO;
using System.Data.SqlTypes;
using System.Text.RegularExpressions;

namespace day_7
{
    internal class Program
    {
        static List<int> Ranks(List<string> hands)
        {
            List<int> strengths = new List<int>();
            for (int i = 0; i < hands.Count; i++)
            {
                hands[i] = hands[i].Replace('A', 'Z');
                hands[i] = hands[i].Replace('K', 'Y');
                hands[i] = hands[i].Replace('Q', 'X');
                if (!part2)
                {
                    hands[i] = hands[i].Replace('J', 'W');
                }
                else
                {
                    hands[i] = hands[i].Replace('J', '1');
                }
                int jCount = 0;
                int currentStrength = 0;
                char match = 'n';
                
                for (int k = 0; k < hands[i].Length; k++)
                {
                    if (hands[i][k] == '1')
                    {
                        jCount++;
                    }
                    //Console.WriteLine(hands[i][j]);
                }
                for (int j = 0; j < hands[i].Length; j++)
                {
                    
                    //Console.WriteLine(jCount + " <----");
                    //if (hands[i][j] == 'J')
                    //{
                    //    jCount++;
                    //}

                    int matches = 0;
                    if (hands[i][j] != match)
                    {
                        for (int k = 0; k < hands[i].Length; k++)
                        {
                            if (hands[i][j] == hands[i][k])
                            {
                                //if (hands[i][k] == 'J')
                                //{
                                //    jCount++;
                                //}
                                matches++;
                                //Console.WriteLine(hands[i][j]);
                                if (matches > 1)
                                {
                                    match = hands[i][j];
                                }
                            }
                        }
                    }
                    else
                    {
                        if (hands[i].Length - 1 == j)
                        {
                            strengths.Add(currentStrength);
                        }
                        continue;
                    }
                    //Console.WriteLine(matches);
                    //Console.ReadKey();

                    if (matches == 5)
                    {
                        strengths.Add(6);
                        break;
                    }
                    else if (matches == 4)
                    {
                        strengths.Add(5);
                        break;
                    }
                    else if (matches == 3)
                    {
                        if (currentStrength == 1)
                        {
                            strengths.Add(4);
                            break;
                        }
                        currentStrength = 3;
                        //Console.ReadKey();
                    }
                    else if (matches == 2)
                    {
                        if (currentStrength == 3)
                        {
                            strengths.Add(4);
                            break;
                        }
                        else if (currentStrength == 1)
                        {
                            strengths.Add(2);
                            break;
                        }
                        currentStrength = 1;
                    }
                    else if (currentStrength == 3)
                    {
                        strengths.Add(3);
                        break;
                    }
                    if (j == hands[i].Length - 1)
                    {
                        strengths.Add(currentStrength);
                        break;
                    }
                }
                //Console.WriteLine("---");
                
                if (strengths[strengths.Count - 1] == 6 || jCount == 0)
                {
                    //Console.WriteLine(hands[i] + " : " + jCount + " : " + strengths[strengths.Count - 1]);
                    continue;
                }
                if (jCount == 1)
                {
                    if (strengths[strengths.Count - 1] == 0 || strengths[strengths.Count - 1] == 5)
                    {
                        strengths[strengths.Count - 1]++;
                    }
                    else
                    {
                        //Console.WriteLine(strengths[strengths.Count - 1]);
                        strengths[strengths.Count - 1] += 2;
                        //Console.WriteLine(strengths[strengths.Count - 1]);
                        //Console.WriteLine("---");
                    }
                }
                else if (jCount == 2)
                {
                    if (strengths[strengths.Count - 1] == 2)
                    {
                        
                        strengths[strengths.Count - 1] += 3;
                        //Console.WriteLine(strengths[strengths.Count - 1]);
                        //Console.WriteLine("----");
                    }
                    else
                    {
                        strengths[strengths.Count - 1] += 2;
                    }
                }
                else if (jCount == 3)
                {
                    strengths[strengths.Count - 1] += 2;
                }
                else if (jCount == 4)
                {
                    strengths[strengths.Count - 1]++;
                }
                //Console.WriteLine(hands[i] + " : " + jCount + " : " + strengths[strengths.Count - 1]);
            }
            return OrderRank(hands, strengths);
        }
        static List<int> OrderRank(List<string> hands, List<int> strengths)
        {


            List<int> ranks = new List<int>();
            for (int i = 0; i < hands.Count; i++)
            {
                ranks.Add(0);
            }
            List<List<int>> sepRanks = new List<List<int>>();
            for (int i = 0; i < 7; i++)
            {
                sepRanks.Add(new List<int>());
            }

            for (int j = 6; j > -1; j--)
            {
                List<int> equalStrengthIndex = new List<int>();
                for (int i = 0; i < strengths.Count; i++)
                {
                    //Console.WriteLine(strengths[i]);
                    if (strengths[i] == j)
                    {
                        equalStrengthIndex.Add(i);
                        //Console.Write(strengths[i] + " ");
                        //Console.WriteLine(hands[i]);
                        sepRanks[j].Add(strengths[i]);
                    }
                }
                if (sepRanks[j].Count > 0)
                {
                    //Console.WriteLine("---");
                    sepRanks[j] = Ordering(hands, equalStrengthIndex, sepRanks[j]);

                    for (int i = 0; i < equalStrengthIndex.Count; i++)
                    {
                        ranks[equalStrengthIndex[i]] = sepRanks[j][i];
                    }
                }
            }
            return ranks;
        }
        static int lessThanHands = 0;
        static List<int> Ordering(List<string> hands, List<int> equalStrengthIndex, List<int> ranks)
        {
            List<string> handsOrdered = new List<string>();
            //Console.WriteLine(ranks[0]);
            for (int i = 0; i < hands.Count; i++)
            {
                handsOrdered.Add(hands[i]);
            }
            handsOrdered.Sort();
            //handsOrdered.Reverse();
            //Console.WriteLine(handsOrdered[0] + "  " + handsOrdered[1]);
            int highestRank = 0;
            for (int i = 0; i < equalStrengthIndex.Count; i++)
            {
                for (int j = 0; j < handsOrdered.Count; j++)
                {
                    if (hands[equalStrengthIndex[i]] == handsOrdered[j])
                    {
                        ranks[i] = j;
                        highestRank++;
                    }

                }

            }
            //Console.WriteLine(": " + ranks[0]);
            int totalHands = hands.Count - lessThanHands;
            List<bool> checks = new List<bool>();
            for (int i = 0; i < ranks.Count; i++)
            {
                checks.Add(false);
            }
            for (int i = hands.Count; i > -1; i--)
            {
                for (int j = 0; j < ranks.Count; j++)
                {
                    if (ranks[j] == i && !checks[j])
                    {
                        Console.WriteLine("i: " + i);
                        Console.WriteLine("ranks[j]B " + ranks[j]);
                        ranks[j] = totalHands;
                        checks[j] = true;
                        Console.WriteLine("totalHands " + totalHands);
                        Console.WriteLine("ranks[j] " + ranks[j]);

                        totalHands--;
                        lessThanHands++;
                        //Console.WriteLine("break");
                        break;
                    }
                }
                if (!checks.Contains(false))
                {
                    break;
                }
            }
            //Console.WriteLine("ranks[0] " + ranks[0]);
            return ranks;
        }
        static bool part2 = true;
        static void Main(string[] args)
        {
            long total = 0;
            List<string> hands = new List<string>();
            using (StreamReader sr = new StreamReader("Input.txt"))
            {
                string line;
                List<int> ranks = new List<int>();
                List<int> bids = new List<int>();
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    hands.Add(line.Substring(0, line.IndexOf(' ')));
                    bids.Add(int.Parse(line.Substring(line.IndexOf(' '))));
                }
                ranks = Ranks(hands);
                for (int i = 0; i < bids.Count; i++)
                {
                    //Console.WriteLine(ranks[i] + "j " + bids[i]);
                    total += ranks[i] * bids[i];
                    //Console.WriteLine(total);
                }
                Console.WriteLine(total);
            }
            Console.ReadLine();
        }
    }
}
