using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace day_10
{
    internal class Program
    {
        struct Point
        {
            public int x, y;
        }
        struct Direction
        {
            public bool up, right, down, left;
        }
        static Point StartPos(List<string> lines)
        {
            Point start = new Point { x = 0, y = 0 };
            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].Contains('S'))
                {
                    start.y = i;
                    start.x = lines[i].IndexOf('S');
                }
            }
            return start;
        }
        static long BretFest(List<string> lines, Point start)
        {
            long distance = 1;
            List<Point> stored = new List<Point>();
            stored.Add(start);
            Direction next1 = new Direction { down = false, up = false, right = false, left = false };
            Direction next2 = new Direction { down = false, up = false, right = false, left = false };
            if (start.y > 0)
            {
                if (lines[start.y - 1][start.x] == '7')
                {
                    stored.Add(new Point { x = start.x, y = start.y - 1 });
                    next1.left = true;
                    //Console.WriteLine("bu");
                }
            }
            //Console.WriteLine(lines.Count);
            if (start.y < lines.Count - 1)
            {
                //Console.WriteLine(lines[start.y + 1][start.x]);
                if (lines[start.y + 1][start.x] == 'J')
                {
                    stored.Add(new Point { x = start.x, y = start.y + 1 });
                    next2.left = true;
                    //Console.WriteLine("fe");
                }
            }

            for (int i = 1; i < stored.Count; i++)
            {
                distance++;
                Point quickStore = Step(lines, ref next1, stored[i]);
                //Console.WriteLine(stored[i].x + " " + stored[i].y);
                if (stored.Contains(quickStore))
                {
                    if (!part2)
                    {
                        return distance;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    stored.Add(quickStore);
                }
                i++;
                quickStore = Step(lines, ref next2, stored[i]);
                if (stored.Contains(quickStore))
                {
                    if (!part2)
                    {
                        return distance;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    stored.Add(quickStore);
                }
            }
            //for (int i = 0; i < stored.Count; i++)
            //{
            //    char replace = lines[stored[i].y][stored[i].x];
            //    Console.SetCursorPosition(stored[i].x, stored[i].y);
            //    Console.WriteLine(replace);
            //}
            next1 = new Direction { up = false, down = false, right = false, left = true };
            List<Point> newStored = new List<Point>();
            newStored.Add(start);
            newStored.Add(new Point { x = start.x, y = start.y - 1 });
            //stored.RemoveAt(0);
            Direction inside = new Direction() { down = false, left = false, right = true, up = true };
            for (int i = 1; i < newStored.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                //Console.WriteLine("x");
                Point quickStore = Step(lines, ref next1, newStored[i]);
                Console.SetCursorPosition(newStored[i].x, newStored[i].y);
                Console.WriteLine(lines[newStored[i].y][newStored[i].x]);
                //Console.WriteLine("stored[i]: " + newStored[i].x + " " + newStored[i].y);
                //Console.WriteLine(lines[quickStore.y][quickStore.x]);
                //Console.WriteLine("quick: " + quickStore.x + " " + quickStore.y);
                if (lines[quickStore.y][quickStore.x] == 'S')
                {
                    //Console.WriteLine("huh: " + quickStore.x + " " + quickStore.y);
                    Console.SetCursorPosition(8, lines.Count);
                    return nestStored.Count;
                }
                else
                {
                    newStored.Add(quickStore);
                }
                if (next1.up && inside.left)
                {
                    inside.left = false;
                    inside.right = true;
                }
                if (next1.down && inside.right)
                {
                    inside.right = false;
                    inside.left = true;
                }
                if (next1.right && inside.up)
                {
                    inside.up = false;
                    inside.down = true;
                }
                if (next1.left && inside.down)
                {
                    inside.down = false;
                    inside.up = true;
                }
                for (int j = 0; j < 4; j++)
                {
                    //Console.WriteLine("len " + (newStored.Count - 1));
                    //Console.WriteLine(i);
                    quickStore = new Point { x = newStored[i + 1].x, y = newStored[i + 1].y };
                    //Console.WriteLine("in: " + inside.up + " " + inside.down + " " + inside.right + " " + inside.left);
                    //Console.WriteLine("..: " + next1.left + " " + next1.right + " " + next1.up + " " + next1.down);
                    if (j == 0)
                    {
                        quickStore.y--;
                        //Console.WriteLine("b " + newStored[i].x + " " + newStored[i].y);
                        if (inside.up && !stored.Contains(quickStore) && !nestStored.Contains(quickStore))
                        {
                            NestLocation(lines, stored, quickStore);

                        }
                        //Console.WriteLine("c " + newStored[i].x + " " + newStored[i].y);
                    }
                    else if (j == 1)
                    {
                        quickStore.y++;
                        if (inside.down && !stored.Contains(quickStore) && !nestStored.Contains(quickStore))
                        {
                            NestLocation(lines, stored, quickStore);
                        }
                    }
                    else if (j == 2)
                    {
                        quickStore.x++;
                        if (inside.right && !stored.Contains(quickStore) && !nestStored.Contains(quickStore))
                        {
                            NestLocation(lines, stored, quickStore);
                        }
                    }
                    else if (j == 3)
                    {
                        quickStore.x--;
                        if (inside.left && !stored.Contains(quickStore) && !nestStored.Contains(quickStore))
                        {
                            NestLocation(lines, stored, quickStore);
                        }
                    }
                }
                //Console.WriteLine("quik: " + quickStore.x + " " + quickStore.y);
                
            }

            return -9;
        }
        static int NestLocation(List<string> lines, List<Point> stored, Point current)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            //Console.WriteLine("hello");
            nestStored.Add(current);
            for (int i = 0; i < nestStored.Count; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Point quickStore = nestStored[i];
                    //Console.WriteLine(":: " + quickStore.x + " " + quickStore.y);
                    if (j == 0)
                    {
                        quickStore.y--;
                    }
                    else if (j == 1)
                    {
                        quickStore.y++;
                    }
                    else if (j == 2)
                    {
                        quickStore.x++;
                    }
                    else if (j == 3)
                    {
                        quickStore.x--;
                    }
                    //Console.WriteLine(": " + quickStore.x + " " + quickStore.y);
                    if (!stored.Contains(quickStore) && !nestStored.Contains(quickStore))
                    {
                        //Console.WriteLine("done");
                        nestStored.Add(quickStore);
                        Console.SetCursorPosition(quickStore.x, quickStore.y);
                        Console.WriteLine(lines[quickStore.y][quickStore.x]);
                    }

                }
            }
            return nestStored.Count;
        }
        static List<Point> nestStored = new List<Point>();
        static Point Step(List<string> lines, ref Direction next, Point stored)
        {
            if (next.up)
            {
                next.up = false;
                char up = lines[stored.y - 1][stored.x];
                
                if (up == '7')
                {
                    next.left = true;
                }
                else if (up == '|')
                {
                    next.up = true;
                }
                else if (up == 'F')
                {
                    next.right = true;
                }
                return new Point { x = stored.x, y = stored.y - 1 };
            }
            else if (next.right)
            {
                next.right = false;
                char right = lines[stored.y][stored.x + 1];

                if (right == '7')
                {
                    next.down = true;
                }
                else if (right == '-')
                {
                    next.right = true;
                }
                else if (right == 'J')
                {
                    next.up = true;
                }
                return new Point { x = stored.x + 1, y = stored.y };
            }
            else if (next.down)
            {
                next.down = false;
                char down = lines[stored.y + 1][stored.x];
                if (down == 'L')
                {
                    next.right = true;
                }
                else if (down == '|')
                {
                    next.down = true;
                }
                else if (down == 'J')
                {
                    next.left = true;
                }
                return new Point { x = stored.x, y = stored.y + 1 };
            }
            else if (next.left)
            {
                next.left = false;
                char left = lines[stored.y][stored.x - 1];
                if (left == 'L')
                {
                    next.up = true;
                }
                else if (left == '-')
                {
                    next.left = true;
                }
                else if (left == 'F')
                {
                    next.down = true;
                }
                return new Point { x = stored.x - 1, y = stored.y };
            }
            Console.WriteLine("uh oh");
            Console.ReadKey();
            return new Point { x = 1, y = -2 };
        }
        static bool part2 = false;
        static void Main(string[] args)
        {
            List<string> lines = new List<string>();
            using (StreamReader sr = new StreamReader("input.txt"))
            {
                string line = "";
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    lines.Add(line);
                    Console.WriteLine(line);
                }
            }
            Point start = StartPos(lines);
            if (!part2)
            {
                Console.Write("part 1: ");
            }
            else
            {
                Console.Write("part 2: ");
            }
            Console.WriteLine(BretFest(lines, start));
            //for (int i = 0; i < nestStored.Count; i++)
            //{
            //    Console.WriteLine(nestStored[i].x + " " + nestStored[i].y);
            //}
            //Console.WriteLine(start.x + " " + start.y);
            Console.ReadKey();
        }
    }
}