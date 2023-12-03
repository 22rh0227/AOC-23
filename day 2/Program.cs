using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace day_2
{
    internal class Program
    {
        static int reds = 0;
        static int greens = 0;
        static int blues = 0;
        static int FindReds(string line)
        {
            //Console.WriteLine(line);
            int total = 0;
            for (int i = 0; i < line.Length - 2; i++)
            {
                if (line[i] == 'r' && line[i + 1] == 'e' && line[i + 2] == 'd')
                {
                    if (char.IsDigit(line[i - 3]) && char.IsDigit(line[i - 2]))
                    {
                        total = (int.Parse(line[i - 3].ToString() + line[i - 2].ToString()));
                    }
                    else
                    {
                        total = (int.Parse(line[i - 2].ToString()));
                    }
                    return total;
                }
            }
            return total;
        }
        static bool NumOfReds(int num)
        {
            if (num > 12)
            {
                return true;
            }
            return false;
        }
        static void NeededReds(int num)
        {
            if (num > reds)
            {
                reds = num;
            }
            Console.WriteLine("reds num " + num);
        }
        static void NeededGreens(int nums)
        {
            if (nums > greens)
            {
                greens = nums;
            }
            Console.WriteLine("greens num " + nums);
        }
        static void NeededBlues(int nums)
        {
            if (nums > blues)
            {
                blues = nums;
            }
            Console.WriteLine("blues nums " + nums);
        }
        static int FindGreens(string line)
        {
            //Console.WriteLine(line);
            int total = 0;
            for (int i = 0; i < line.Length - 4; i++)
            {
                if (line[i] == 'g' && line[i + 1] == 'r' && line[i + 2] == 'e' )
                {
                    //Console.WriteLine(i);
                    //Console.WriteLine(line);
                    if (char.IsDigit(line[i - 2]) && char.IsDigit(line[i - 2]))
                    {
                        total = (int.Parse(line[i - 3].ToString() + line[i - 2].ToString()));
                    }
                    else
                    {
                        total = (int.Parse(line[i - 2].ToString()));
                    }
                    return total;
                }
            }
            return total;
        }
        static bool NumOfGreens(int num)
        {
            if (num > 13)
            {
                return true;
            }
            return false;
        }
        static int FindBlues(string line)
        {
            //Console.WriteLine(line);
            int total = 0;
            for (int i = 0; i < line.Length - 3; i++)
            {
                if (line[i] == 'b' && line[i + 1] == 'l' && line[i + 2] == 'u')
                {
                    if (char.IsDigit(line[i - 3]) && char.IsDigit(line[i - 2]))
                    {
                        total = (int.Parse(line[i - 3].ToString() + line[i - 2].ToString()));
                    }
                    else
                    {
                        total = (int.Parse(line[i - 2].ToString()));
                    }
                    return total;
                }
            }
            return total;
        }
        static bool NumOfBlues(int num)
        {
            if (num > 14)
            {
                return true;
            }
            return false;
        }
        static void Main(string[] args)
        {
            //string g = "great";
            //Console.WriteLine(g.Substring(2));
            int total = 0;
            int semiIndex = 5;
            int powerTotal = 0;
            using (StreamReader sr = new StreamReader("input.txt"))
            {
                string line;

                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    semiIndex = 5;
                    while (true)
                    {
                        int length;
                        //Console.WriteLine(line.Substring(semiIndex + 1));
                        if (line.Substring(semiIndex + 1).IndexOf(';') != -1)
                        {
                            length = line.Substring(semiIndex + 1).IndexOf(';');
                            NeededReds(FindReds(line.Substring(semiIndex + 1, length)));
                            NeededGreens(FindGreens(line.Substring(semiIndex + 1, length)));
                            NeededBlues(FindBlues(line.Substring(semiIndex + 1, length)));
                            //if (NumOfReds(FindReds(line.Substring(semiIndex + 1, length))))
                            //{
                            //    break;
                            //}
                            //else if (NumOfGreens(FindGreens(line.Substring(semiIndex + 1, length))))
                            //{
                            //    break;
                            //}
                            //else if (NumOfBlues(FindBlues(line.Substring(semiIndex + 1, length))))
                            //{
                            //    break;
                            //}
                            
                        }
                        else
                        {
                            NeededReds(FindReds(line.Substring(semiIndex + 1)));
                            NeededGreens(FindGreens(line.Substring(semiIndex + 1)));
                            NeededBlues(FindBlues(line.Substring(semiIndex + 1)));
                            //if (NumOfReds(FindReds(line.Substring(semiIndex + 1))))
                            //{
                            //    break;
                            //}
                            //else if (NumOfGreens(FindGreens(line.Substring(semiIndex + 1))))
                            //{
                            //    break;
                            //}
                            //else if (NumOfBlues(FindBlues(line.Substring(semiIndex + 1))))
                            //{
                            //    break;
                            //}
                            
                        }
                        //Console.WriteLine(line);
                        if (line.IndexOf(';') == -1)
                        {
                            if (char.IsDigit(line[7]))
                            {
                                total += int.Parse(line[5].ToString() + line[6].ToString() + line[7].ToString());
                            }
                            else if (char.IsDigit(line[6]))
                            {
                                total += int.Parse(line[5].ToString() + line[6].ToString());
                            }
                            else
                            {
                                total += int.Parse(line[5].ToString());
                            }
                            break;
                        }
                        if (semiIndex != 5)
                        {
                            line = line.Remove(semiIndex, 1);
                        }
                        //Console.WriteLine(line);
                        semiIndex = line.IndexOf(';');
                        //Console.WriteLine(semiIndex);
                        //Console.ReadKey();
                    }
                    Console.WriteLine(line);
                    Console.WriteLine(reds);
                    Console.WriteLine(greens);
                    Console.WriteLine(blues);
                    powerTotal += reds * greens * blues;
                    reds = 0;
                    greens = 0;
                    blues = 0;
                }
            }
            Console.WriteLine("power " + powerTotal);
            Console.WriteLine(total);
            Console.ReadKey();


        }
    }
}
