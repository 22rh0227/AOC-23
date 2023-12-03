using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day_1
{
    internal class Program
    {
        static int LowNums(string line)
        {
            if (line.Length > 2 && line[0] == 'o')
            {
                if (line[1] != 'n')
                {
                    return 0;
                }
                if (line[2] != 'e')
                {
                    return 0;
                }
                return 1;
            }
            else if (line.Length > 2 && line[0] == 't' && line[1] == 'w')
            {
                if (line[2] != 'o')
                {
                    return 0;
                }
                return 2;
            }
            else if (line.Length > 4 && line[0] == 't' && line[1] == 'h' && line[2] == 'r' && line[3] == 'e' && line[4] == 'e')
            {
                return 3;
            }
            else if (line.Length > 3 && line[0] == 'f' && line[1] == 'o' && line[2] == 'u' && line[3] == 'r')
            {
                return 4;
            }
            else if (line.Length > 3 && line[0] == 'f' && line[1] == 'i' && line[2] == 'v' && line[3] == 'e')
            {
                return 5;
            }
            else if (line.Length > 2 && line[0] == 's' && line[1] == 'i' && line[2] == 'x')
            {
                return 6;
            }
            else if (line.Length > 4 && line[0] == 's' && line[1] == 'e' && line[2] == 'v' && line[3] == 'e' && line[4] == 'n')
            {
                return 7;
            }
            else if (line.Length > 4 && line[0] == 'e' && line[1] == 'i' && line[2] == 'g' && line[3] == 'h' && line[4] == 't')
            {
                return 8;
            }
            else if (line.Length > 3 && line[0] == 'n' && line[1] == 'i' && line[2] == 'n' && line[3] == 'e')
            {
                return 9;
            }
            return 0;
        }
        static void Main(string[] args)
        {
            int total = 0;
            string value = "";
            string tempValue = "";
            using (StreamReader sr = new StreamReader("input.txt"))
            {
                string line;
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    for (int i = 0; i < line.Length; i++)
                    {
                        if (line[i] - 48 < 10 && line[i] - 48 > -1)
                        {
                            tempValue += (int)line[i] - 48;
                        }
                        else 
                        {
                            tempValue += LowNums(line.Substring(i));
                            if (tempValue[tempValue.Length - 1] - 48 == 0)
                            {
                                tempValue = tempValue.Remove(tempValue.Length - 1);
                            }
                        }
                    }
                    value += tempValue[0];
                    value += tempValue[tempValue.Length - 1];
                    Console.WriteLine(tempValue);
                    Console.WriteLine(value);
                    total += int.Parse(value);
                    value = "";
                    tempValue = "";
                    
                    //Console.WriteLine(total);
                }
            }
            Console.WriteLine(total);
            Console.ReadKey();
        }
    }
}
