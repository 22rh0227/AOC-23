using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace day_18
{
    internal class Program
    {
        struct Point
        {
            public int x, y;
        }
        struct Range
        {
            public int xS, xE;
            public int yS, yE;
        }
        static int HexToDecimal(string hexValue)
        {
            return Convert.ToInt32(hexValue, 16);
        }
        static List<Point> MakeTrench(List<string> lines)
        {
            List<Point> trench = new List<Point>();
            trench.Add(new Point() { x = 0, y = 0 });
            for (int i = 0; i < lines.Count; i++)
            {
                int len;
                if (char.IsDigit(lines[i][3]))
                {
                    len = int.Parse(lines[i][2] + lines[i][3].ToString());
                }
                else
                {
                    len = int.Parse(lines[i][2].ToString());
                }
                if (lines[i][0] == 'U')
                {
                    for (int j = 0; j < len; j++)
                    {
                        if (trench[trench.Count - 1].x == 0 && trench[trench.Count - 1].y - 1 == 0)
                        {
                            continue;
                        }
                        trench.Add(new Point() { x = trench[trench.Count - 1].x, y = trench[trench.Count - 1].y - 1 });
                    }
                }
                else if (lines[i][0] == 'R')
                {
                    for (int j = 0; j < len; j++)
                    {
                        if (trench[trench.Count - 1].x + 1 == 0 && trench[trench.Count - 1].y == 0)
                        {
                            continue;
                        }
                        trench.Add(new Point() { x = trench[trench.Count - 1].x + 1, y = trench[trench.Count - 1].y });
                    }
                }
                else if (lines[i][0] == 'D')
                {
                    for (int j = 0; j < len; j++)
                    {
                        if (trench[trench.Count - 1].x == 0 && trench[trench.Count - 1].y + 1 == 0)
                        {
                            continue;
                        }
                        trench.Add(new Point() { x = trench[trench.Count - 1].x, y = trench[trench.Count - 1].y + 1 });
                    }
                }
                else if (lines[i][0] == 'L')
                {
                    for (int j = 0; j < len; j++)
                    {
                        if (trench[trench.Count - 1].x - 1 == 0 && trench[trench.Count - 1].y == 0)
                        {
                            continue;
                        }
                        trench.Add(new Point() { x = trench[trench.Count - 1].x - 1, y = trench[trench.Count - 1].y });
                    }
                }
            }
            return trench;
        }
        static int BFS(List<Point> trench)
        {
            Point start = new Point();
            if (!trench.Contains(new Point() { x = 1, y = 1 }))
            {
                start = new Point() { x = 1, y = 1 };
            }
            else if (!trench.Contains(new Point() { x = 1, y = 0 }))
            {
                start = new Point() { x = 1, y = 0 };
            }
            else if (!trench.Contains(new Point() { x = 0, y = -1 }))
            {
                start = new Point() { x = 0, y = -1 };
            }
            else if (!trench.Contains(new Point() { x = -1, y = 0 }))
            {
                start = new Point() { x = -1, y = 0 };
            }
            else
            {
                Console.WriteLine(System.Environment.StackTrace);
                Console.ReadLine();
            }
            //BFS start
            List<Point> stored = new List<Point>();
            stored.Add(new Point() { x = 1, y = -1});
            for (int i = 0; i < stored.Count; i++)
            {
                Point next = new Point() { x = stored[i].x, y = stored[i].y - 1 };
                if (!stored.Contains(next) && !trench.Contains(next))
                {
                    stored.Add(next);
                }
                next = new Point() { x = stored[i].x + 1, y = stored[i].y };
                if (!stored.Contains(next) && !trench.Contains(next))
                {
                    stored.Add(next);
                }
                next = new Point() { x = stored[i].x, y = stored[i].y + 1 };
                if (!stored.Contains(next) && !trench.Contains(next))
                {
                    stored.Add(next);
                }
                next = new Point() { x = stored[i].x - 1, y = stored[i].y };
                if (!stored.Contains(next) && !trench.Contains(next))
                {
                    stored.Add(next);
                }
                if (stored.Count > 90000)
                {
                    Console.WriteLine(System.Environment.StackTrace);
                    for (int j = 0; j < stored.Count; j++)
                    {
                        Console.WriteLine(stored[j].x + " " + stored[j].y);
                    }
                    Console.ReadLine();
                }
            }
            return stored.Count + trench.Count;
        }
        static List<Range> KindaHexTrench(List<string> lines)
        {
            List<Range> trench = new List<Range>();
            trench.Add(new Range() { xS = 0, xE = 0, yS = 0, yE = 0 });
            for (int i = 0; i < lines.Count; i++)
            {
                bool digits = false;
                if (char.IsDigit(lines[i][3]))
                {
                    digits = true;
                }
                if (lines[i][0] == 'R')
                {
                    if (!digits)
                    {
                        trench.Add(new Range() { xS = trench[trench.Count - 1].xE, xE = trench[trench.Count - 1].xE + int.Parse(lines[i][2].ToString()), yS = trench[trench.Count - 1].yE, yE = trench[trench.Count - 1].yE });
                    }
                    else
                    {
                        trench.Add(new Range() { xS = trench[trench.Count - 1].xE, xE = trench[trench.Count - 1].xE + int.Parse(lines[i][2].ToString() + lines[i][3]), yS = trench[trench.Count - 1].yE, yE = trench[trench.Count - 1].yE });
                    }
                }
                else if (lines[i][0] == 'D')
                {
                    if (!digits)
                    {
                        trench.Add(new Range() { xS = trench[trench.Count - 1].xE, xE = trench[trench.Count - 1].xE, yS = trench[trench.Count - 1].yE, yE = trench[trench.Count - 1].yE + int.Parse(lines[i][2].ToString()) });
                    }
                    else
                    {
                        trench.Add(new Range() { xS = trench[trench.Count - 1].xE, xE = trench[trench.Count - 1].xE, yS = trench[trench.Count - 1].yE, yE = trench[trench.Count - 1].yE + int.Parse(lines[i][2].ToString() + lines[i][3]) });
                    }
                }
                else if (lines[i][0] == 'L')
                {
                    if (!digits)
                    {
                        trench.Add(new Range() { xS = trench[trench.Count - 1].xE, xE = trench[trench.Count - 1].xE - int.Parse(lines[i][2].ToString()), yS = trench[trench.Count - 1].yE, yE = trench[trench.Count - 1].yE });
                    }
                    else
                    {
                        trench.Add(new Range() { xS = trench[trench.Count - 1].xE, xE = trench[trench.Count - 1].xE - int.Parse(lines[i][2].ToString() + lines[i][3]), yS = trench[trench.Count - 1].yE, yE = trench[trench.Count - 1].yE });
                    }
                }
                else if (lines[i][0] == 'U')
                {
                    if (!digits)
                    {
                        trench.Add(new Range() { xS = trench[trench.Count - 1].xE, xE = trench[trench.Count - 1].xE, yS = trench[trench.Count - 1].yE, yE = trench[trench.Count - 1].yE - int.Parse(lines[i][2].ToString()) });
                    }
                    else
                    {
                        trench.Add(new Range() { xS = trench[trench.Count - 1].xE, xE = trench[trench.Count - 1].xE, yS = trench[trench.Count - 1].yE, yE = trench[trench.Count - 1].yE - int.Parse(lines[i][2].ToString() + lines[i][3]) });
                    }
                }
                if (!digits)
                {
                    total += int.Parse(lines[i][2].ToString());
                }
                else
                {
                    total += int.Parse(lines[i][2].ToString() + lines[i][3]);
                }
            }
            return trench;
        }
        static List<Range> HexTrench(List<string> lines)
        {
            List<Range> trench = new List<Range>();
            trench.Add(new Range() { xS = 0, xE = HexToDecimal(lines[0].Substring(6, 5)), yS = 0, yE = 0 });
            total = HexToDecimal(lines[0].Substring(6, 5));
            for (int i = 1; i < lines.Count; i++)
            {
                if (lines[i][lines[i].Length - 2] == '0')
                {
                    trench.Add(new Range() { xS = trench[trench.Count - 1].xE, xE = trench[trench.Count - 1].xE + HexToDecimal(lines[i].Substring(lines[i].IndexOf('#') + 1, 5)), yS = trench[trench.Count - 1].yE, yE = trench[trench.Count - 1].yE });
                }
                else if (lines[i][lines[i].Length - 2] == '1')
                {
                    trench.Add(new Range() { xS = trench[trench.Count - 1].xE, xE = trench[trench.Count - 1].xE, yS = trench[trench.Count - 1].yE, yE = trench[trench.Count - 1].yE + HexToDecimal(lines[i].Substring(lines[i].IndexOf('#') + 1, 5)) });
                }
                else if (lines[i][lines[i].Length - 2] == '2')
                {
                    trench.Add(new Range() { xS = trench[trench.Count - 1].xE, xE = trench[trench.Count - 1].xE - HexToDecimal(lines[i].Substring(lines[i].IndexOf('#') + 1, 5)), yS = trench[trench.Count - 1].yE, yE = trench[trench.Count - 1].yE });
                }
                else if (lines[i][lines[i].Length - 2] == '3')
                {
                    trench.Add(new Range() { xS = trench[trench.Count - 1].xE, xE = trench[trench.Count - 1].xE, yS = trench[trench.Count - 1].yE, yE = trench[trench.Count - 1].yE - HexToDecimal(lines[i].Substring(lines[i].IndexOf('#') + 1, 5)) });
                }
                total += HexToDecimal(lines[i].Substring(lines[i].IndexOf('#') + 1, 5));
            }
            return trench;
        }
        static void MeasureLines(List<Range> trench)
        {
            int currHighest = 0;
            int currLowest = 0;
            for (int i = 0; i < trench.Count; i++)
            {
                if (trench[i].yS > currLowest)
                {
                    currLowest = trench[i].yS;
                    continue;
                }
                if (trench[i].yS < currHighest)
                {
                    currHighest = trench[i].yS;
                }
            }
            for (int r = currHighest + 1; r < currLowest; r++)
            {
                List<int> cols = new List<int>();
                List<Range> currAdds = new List<Range>();
                List<Range> toCheck = new List<Range>();
                List<Range> pits = new List<Range>();
                int ends = 0;
                for (int i = 0; i < trench.Count; i++)
                {
                    if (trench[i].yS < r && trench[i].yE < r)
                    {
                        trench.RemoveAt(i);
                        i--;
                        continue;
                    }
                    if (trench[i].yS > r && trench[i].yE > r || trench[i].yS == trench[i].yE)
                    {
                        continue;
                    }
                    if (trench[i].yS == r || trench[i].yE == r)
                    {
                        ends++;
                    }
                    else
                    {
                        ends = 0;
                    }
                    if (ends == 2)
                    {
                        if ((currAdds[currAdds.Count - 1].yS > currAdds[currAdds.Count - 1].yE && trench[i].yS > trench[i].yE) || (currAdds[currAdds.Count - 1].yS < currAdds[currAdds.Count - 1].yE && trench[i].yS < trench[i].yE))
                        {
                            toCheck.Add(currAdds[currAdds.Count - 1]);
                            toCheck.Add(trench[i]);
                        }
                        else
                        {
                            pits.Add(currAdds[currAdds.Count - 1]);
                            pits.Add(trench[i]);
                            if (trench[i].xS > currAdds[currAdds.Count - 1].xS)
                            {
                                pits.Add(new Range() { xS = trench[i].xS + 1 });
                            }
                            else
                            {
                                pits.Add(new Range() { xS = currAdds[currAdds.Count - 1].xS + 1 });
                            }
                        }
                        ends = 0;
                    }
                    else if (r == 0 && i == trench.Count - 1)
                    {
                        toCheck.Add(trench[1]);
                        toCheck.Add(trench[i]);
                        ////if loop start makes pit, switch above and below
                        //pits.Add(currAdds[1]);
                        //pits.Add(trench[i]);
                        //if (trench[i].xS > currAdds[1].xS)
                        //{
                        //    pits.Add(new Range() { xS = trench[i].xS + 1 });
                        //}
                        //else
                        //{
                        //    pits.Add(new Range() { xS = currAdds[currAdds.Count - 1].xS + 1 });
                        //}
                        //Console.WriteLine("baa");
                    }
                    currAdds.Add(trench[i]);
                    cols.Add(trench[i].xS);
                }
                currAdds = currAdds.OrderBy(x => x.xS).ToList();
                pits = pits.OrderBy(x => x.xS).ToList();
                cols = cols.OrderBy(x => x).ToList();
                if (toCheck.Count > 0 || pits.Count > 0)
                {
                    if (r == 0)
                    {
                        for (int i = 0; i < currAdds.Count; i++)
                        {
                            Console.WriteLine(": " + currAdds[i].yS + " " + currAdds[i].yE);
                        }
                    }
                    for (int i = 0; i < currAdds.Count; i++)
                    {
                        if (pits.Contains(currAdds[i]))
                        {
                            if (i % 2 == 0 && pits[pits.IndexOf(currAdds[i]) + 1].Equals(currAdds[i + 1]))
                            {
                                cols.RemoveAt(i);
                                cols.RemoveAt(i);
                                currAdds.RemoveAt(i);
                                currAdds.RemoveAt(i);
                                i--;
                                continue;
                            }
                        }
                        if (toCheck.Contains(currAdds[i]))
                        {
                            if (i % 2 != 0)
                            {
                                currAdds.RemoveAt(i + 1);
                                cols.RemoveAt(i + 1);
                            }
                            else
                            {
                                currAdds.RemoveAt(i);
                                cols.RemoveAt(i);
                            }
                        }
                    }
                }
                for (int j = 0; j < cols.Count; j += 2)
                {
                    if (cols[j] < 0 && cols[j + 1] < 0)
                    {
                        cols[j] *= -1;
                        cols[j + 1] *= -1;
                        total += cols[j] - cols[j + 1] - 1;
                    }
                    else
                    {
                        total += cols[j + 1] - cols[j] - 1;
                    }
                }

            }

        }
        static bool part2 = true;
        static long total;
        static void Main(string[] args)
        {
            List<string> lines = new List<string>();
            using (StreamReader sr = new StreamReader("input.txt"))
            {
                string line;
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    lines.Add(line);
                }
            }
            if (!part2)
            {
                List<Point> trench = MakeTrench(lines);
                Console.WriteLine("Part 1: " + BFS(trench));
            }
            else
            {
                List<Range> trench = HexTrench(lines);
                //List<Range> trench = KindaHexTrench(lines);
                MeasureLines(trench);
                Console.WriteLine("Part 2: " + total);
            }
            Console.ReadLine();
        }
    }
}
