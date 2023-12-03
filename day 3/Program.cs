using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace day_3
{
    internal class Program
    {
        static List<int> FindParts(List<string> lines)
        {
            string storePart = "";
            List<int> indexs = new List<int>();
            List<int> realParts = new List<int>();
            for (int i = 0; i < lines.Count; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    if (lines[i][j] == '*')
                    {
                        GearRatios(lines, i, j);
                    }
                    if (char.IsDigit(lines[i][j]))
                    {
                        storePart += lines[i][j].ToString();
                        indexs.Add(j);
                    }
                    else if (storePart != "")
                    {
                        if (CheckPart(lines, indexs, i))
                        {
                            realParts.Add(int.Parse(storePart));
                        }
                        storePart = "";
                        indexs.RemoveRange(0, indexs.Count);
                    }
                    if (j == lines[i].Length - 1 && storePart != "")
                    {
                        if (CheckPart(lines, indexs, i))
                        {
                            realParts.Add(int.Parse(storePart));
                        }
                        storePart = "";
                        indexs.RemoveRange(0, indexs.Count);
                    }
                }
            }
            return realParts;
        }
        static bool CheckPart(List<string> lines, List<int> locations, int line)
        {
            bool lineS = false;
            bool lineF = false;
            if (line == 0)
            {
                lineS = true;
            }
            if (line == lines.Count - 1)
            {
                lineF = true;
            }
            if (locations[0] != 0)
            {
                if (lines[line][locations[0] - 1] != '.')
                {
                    return true;
                }
                if (!lineS)
                {
                    if (lines[line - 1][locations[0] - 1] != '.')
                    {
                        return true;
                    }
                }
                if (!lineF)
                {
                    if (lines[line + 1][locations[0] - 1] != '.')
                    {
                        return true;
                    }
                }
            }
            if (!lineS)
            {
                for (int i = 0; i < locations.Count; i++)
                {
                    if (lines[line - 1][locations[i]] != '.')
                    {
                        return true;
                    }
                }
            }
            if (!lineF)
            {
                for (int i = 0; i < locations.Count; i++)
                {
                    if (lines[line + 1][locations[i]] != '.')
                    {
                        return true;
                    }
                }
            }
            if (locations[locations.Count - 1] != lines[line].Length - 1)
            {
                if (lines[line][locations[locations.Count - 1] + 1] != '.')
                {
                    return true;
                }
                if (!lineS)
                {
                    if (lines[line - 1][locations[locations.Count - 1] + 1] != '.')
                    {
                        return true;
                    }
                }
                if (!lineF)
                {
                    if (lines[line + 1][locations[locations.Count - 1] + 1] != '.')
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        static double gearRatioSum = 0;
        static void GearRatios(List<string> lines, int line, int location)
        {
            bool lineS = false;
            bool lineF = false;
            bool upDone = false;
            bool up1Done = false;
            bool downDone = false;
            bool down1Done = false;
            int startIndex = 0;
            string part1 = "";
            string part2 = "";
            if (line == 0)
            {
                lineS = true;
            }
            if (line == lines.Count - 1)
            {
                lineF = true;
            }
            if (location != 0)
            {
                if (char.IsDigit(lines[line][location - 1]))
                {
                    for (int i = location - 1; ; i--)
                    {
                        if (!char.IsDigit(lines[line][i]))
                        {
                            startIndex = i + 1;
                            break;
                        }
                        if (i == 0)
                        {
                            startIndex = i;
                            break;
                        }
                    }
                    if (part1 == "")
                    {
                        for (int i = startIndex; ; i++)
                        {
                            if (!char.IsDigit(lines[line][i]))
                            {
                                break;
                            }
                            part1 += lines[line][i];
                        }
                    }
                    else
                    {
                        for (int i = startIndex; i < location; i++)
                        {
                            if (!char.IsDigit(lines[line][i]))
                            {
                                break;
                            }
                            part2 += lines[line][i];
                        }
                    }
                    startIndex = 0;
                }
                if (!lineS)
                {
                    if (char.IsDigit(lines[line - 1][location - 1]))
                    {
                        up1Done = true;
                        for (int i = location - 1; ; i--)
                        {
                            if (!char.IsDigit(lines[line - 1][i]))
                            {
                                startIndex = i + 1;
                                break;
                            }
                            if (i == 0)
                            {
                                startIndex = i;
                                break;
                            }
                        }
                        if (part1 == "")
                        {
                            for (int i = startIndex; ; i++)
                            {
                                if (!char.IsDigit(lines[line - 1][i]))
                                {
                                    break;
                                }
                                if (i == location)
                                {
                                    upDone = true;
                                }
                                part1 += lines[line - 1][i];
                            }
                        }
                        else
                        {
                            for (int i = startIndex; ; i++)
                            {
                                if (!char.IsDigit(lines[line - 1][i]))
                                {
                                    break;
                                }
                                if (i == location)
                                {
                                    upDone = true;
                                }
                                part2 += lines[line - 1][i];
                            }
                        }
                        startIndex = 0;
                    }
                }
                if (!lineF)
                {
                    if (char.IsDigit(lines[line + 1][location - 1]))
                    {
                        down1Done = true;
                        for (int i = location - 1; ; i--)
                        {
                            if (!char.IsDigit(lines[line + 1][i]))
                            {
                                startIndex = i + 1;
                                break;
                            }
                            if (i == 0)
                            {
                                startIndex = i;
                                break;
                            }
                        }
                        if (part1 == "")
                        {
                            for (int i = startIndex; ; i++)
                            {
                                if (!char.IsDigit(lines[line + 1][i]))
                                {
                                    break;
                                }
                                if (i == location)
                                {
                                    downDone = true;
                                }
                                part1 += lines[line + 1][i];
                            }
                        }
                        else
                        {
                            for (int i = startIndex; ; i++)
                            {
                                if (!char.IsDigit(lines[line + 1][i]))
                                {
                                    break;
                                }
                                if (i == location)
                                {
                                    downDone = true;
                                }
                                part2 += lines[line + 1][i];
                            }
                        }
                        startIndex = 0;
                    }
                }
            }
            if (!lineS && !upDone && !up1Done)
            {
                if (char.IsDigit(lines[line - 1][location]))
                {
                    upDone = true;
                    for (int i = location; ; i--)
                    {
                        if (!char.IsDigit(lines[line - 1][i]))
                        {
                            startIndex = i + 1;
                            break;
                        }
                    }
                    if (part1 == "")
                    {
                        for (int i = startIndex; ; i++)
                        {
                            if (!char.IsDigit(lines[line - 1][i]))
                            {
                                break;
                            }
                            part1 += lines[line - 1][i];
                        }
                    }
                    else
                    {
                        for (int i = startIndex; ; i++)
                        {
                            if (!char.IsDigit(lines[line - 1][i]))
                            {
                                break;
                            }
                            part2 += lines[line - 1][i];
                        }
                    }
                    startIndex = 0;
                }
            }
            if (!lineF && !downDone && !down1Done)
            {
                if (char.IsDigit(lines[line + 1][location]))
                {
                    downDone = true;
                    for (int i = location; ; i--)
                    {
                        if (!char.IsDigit(lines[line + 1][i]))
                        {
                            startIndex = i + 1;
                            break;
                        }
                    }
                    if (part1 == "")
                    {
                        for (int i = startIndex; ; i++)
                        {
                            if (!char.IsDigit(lines[line + 1][i]))
                            {
                                break;
                            }
                            part1 += lines[line + 1][i];
                        }
                    }
                    else
                    {
                        for (int i = startIndex; ; i++)
                        {
                            if (!char.IsDigit(lines[line + 1][i]))
                            {
                                break;
                            }
                            part2 += lines[line + 1][i];
                        }
                    }
                    startIndex = 0;
                }
            }
            if (location != lines[line].Length - 1)
            {
                if (char.IsDigit(lines[line][location + 1]))
                {
                    if (part1 == "")
                    {
                        for (int i = location + 1; i < lines[line].Length; i++)
                        {
                            if (!char.IsDigit(lines[line][i]))
                            {
                                break;
                            }
                            part1 += lines[line][i];
                        }
                    }
                    else
                    {
                        for (int i = location + 1; i < lines[line].Length; i++)
                        {
                            if (!char.IsDigit(lines[line][i]))
                            {
                                break;
                            }
                            part2 += lines[line][i];
                        }
                    }
                }
                if (!lineS && !upDone)
                {
                    if (char.IsDigit(lines[line - 1][location + 1]))
                    {
                        if (part1 == "")
                        {
                            for (int i = location + 1; i < lines[line - 1].Length; i++)
                            {
                                if (!char.IsDigit(lines[line - 1][i]))
                                {
                                    break;
                                }
                                part1 += lines[line - 1][i];
                            }
                        }
                        else
                        {
                            for (int i = location + 1; i < lines[line - 1].Length; i++)
                            {
                                if (!char.IsDigit(lines[line - 1][i]))
                                {
                                    break;
                                }
                                part2 += lines[line - 1][i];
                            }
                        }
                    }
                }
                if (!lineF && !downDone)
                {
                    if (char.IsDigit(lines[line + 1][location + 1]))
                    {
                        if (part1 == "")
                        {
                            for (int i = location + 1; i < lines[line + 1].Length; i++)
                            {
                                if (!char.IsDigit(lines[line + 1][i]))
                                {
                                    break;
                                }
                                part1 += lines[line + 1][i];
                            }
                        }
                        else
                        {
                            for (int i = location + 1; i < lines[line + 1].Length; i++)
                            {
                                if (!char.IsDigit(lines[line + 1][i]))
                                {
                                    break;
                                }
                                part2 += lines[line + 1][i];
                            }
                        }
                    }
                }
            }
            if (part1 == "" || part2 == "")
            {
                return;
            }
            gearRatioSum += int.Parse(part1) * int.Parse(part2);
        }
        static void Main(string[] args)
        {
            List<string> lines = new List<string>();
            int total = 0;
            using (StreamReader sr = new StreamReader("input.txt"))
            {
                string line;
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    lines.Add(line);
                }
            }
            List<int> parts = FindParts(lines);
            for (int i = 0; i < parts.Count; i++)
            {
                total += parts[i];
                //Console.WriteLine(parts[i]);
                //Console.WriteLine("-");
            }
            Console.WriteLine(total);
            Console.WriteLine("gearR: " + gearRatioSum);
            Console.ReadKey();
        }
    }
}
