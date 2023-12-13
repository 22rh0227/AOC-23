using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day_13
{
    internal class Program
    {
        static List<int> LeftCols(List<string> lines)
        {
            int currLen = 1;
            List<int> reflections = new List<int>();
            int lastR = 0;
            List<char> currCol = new List<char>();
            //int latestC = 0;
            for (int c = 0; c < currLen; c++)
            {
                bool matching = true;
                for (int r = lastR; lines[r] != ""; r++)
                {
                    currLen = lines[r].Length;
                    if (currCol.Count < lines.IndexOf("", lastR) - lastR)
                    {
                        currCol.Add(lines[r][c]);
                    }
                    //Console.WriteLine(lines.IndexOf(""));
                    if (lines[r][c] != currCol[r - lastR])
                    {
                        matching = false;
                    }
                    currCol[r - lastR] = lines[r][c];
                    //Console.WriteLine(":: " + (r - lastR));
                    //Console.WriteLine(currCol[r - lastR]);
                }
                //Console.WriteLine();
                //if (matching && c != 0)
                //{
                //    latestC = c;
                //}
                //if (c == currLen - 1)
                //{
                //    reflections.Add(latestC);
                //    c = -1;
                //    latestC = 0;
                //    lastR = lines.IndexOf("", lastR) + 1;
                //    if (lastR >= lines.Count)
                //    {
                //        return reflections;
                //    }
                //}
                if (matching && c != 0)
                {
                    //Console.WriteLine("c: " + c);
                    reflections.Add(c);
                    c = -1;
                    lastR = lines.IndexOf("", lastR) + 1;
                    if (lastR >= lines.Count)
                    {
                        return reflections;
                    }
                }
                else if (c == currLen - 1)
                {
                    //Console.WriteLine("c: " + c);
                    reflections.Add(0);
                    c = -1;
                    lastR = lines.IndexOf("", lastR) + 1;
                    if (lastR >= lines.Count)
                    {
                        return reflections;
                    }
                }
            }
            return reflections;
        }
        static List<int> AboveRows(List<string> lines)
        {
            int lastR = 0;
            List<int> reflections = new List<int>();
            //int latestR = 0;
            //List<char> check = new List<char>();
            while (true)
            {
                for (int r = lastR; lines[r] != ""; r++)
                {

                    if (r != 0 && lines[r] == lines[r - 1])
                    {
                        //latestR = r;
                        reflections.Add(r - lastR);
                        //Console.WriteLine(lastR);
                        break;
                    }
                    if (r == lines.IndexOf("", lastR) - 1)
                    {

                        reflections.Add(0);
                        //latestR = 0;
                        //Console.WriteLine(lastR);
                    }
                }
                lastR = lines.IndexOf("", lastR) + 1;
                //check.Add('a');
                if (lastR >= lines.Count)
                {
                    //Console.WriteLine(check.Count(x => x == 'a') + "VV");
                    return reflections;
                }
            }
        }
        static void Main(string[] args)
        {
            List<string> lines = new List<string>();
            //lines.Add("o");
            using (StreamReader sr = new StreamReader("input.txt"))
            {
                string line;
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    lines.Add(line);
                }
            }
            lines.Add("");
            //Console.WriteLine("f: " + lines.Count(x => x == ""));
            List<int> reflectionCols = LeftCols(lines);
            List<int> reflectionRows = AboveRows(lines);
            for (int i = 0; i < reflectionCols.Count; i++)
            {
                Console.WriteLine(reflectionCols[i]);
                Console.WriteLine(": " + reflectionRows[i]);
            }
            Console.WriteLine(reflectionCols.Sum());
            List<int> rowPos = new List<int>();
            for (int i = 0; i < reflectionCols.Count; i++)
            {
                if (reflectionCols[i] == 0)
                {
                    rowPos.Add(i);
                }
            }
            //Console.WriteLine(reflectionRows.Sum());
            List<int> newRows = new List<int>();
            for (int i = 0; i < rowPos.Count; i++)
            {
                //Console.WriteLine(rowPos[i]);
                newRows.Add(reflectionRows[rowPos[i]]);
            }
            Console.WriteLine(newRows.Sum());
            //long total = 0;
            //for (int i = 0; i < reflectionRows.Count; i++)
            //{
            //    if (i % 2 == 0)
            //    {
            //        total += reflectionCols[i];
            //        continue;
            //    }
            //    total += reflectionRows[i] * 100;
            //}
            //Console.WriteLine(total);
            Console.WriteLine(newRows.Select(x => x * 100).ToList().Sum() + reflectionCols.Sum());
            Console.ReadLine();
        }
    }
}
