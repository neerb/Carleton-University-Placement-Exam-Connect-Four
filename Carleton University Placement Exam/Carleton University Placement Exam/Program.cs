/*
 * Carleton Placement Exam for Computer Science: Connect Four
 * July 2017 Placement Exam
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace Carleton_University_Placement_Exam
{
    class Program
    {
        static void Main(string[] args)
        {
            //Placement Exam documentation:
            //http://cs.carleton.edu/faculty/dln/placement/problem.pdf

            var webRequest = WebRequest.Create(@"http://cs.carleton.edu/faculty/dln/placement/grid.txt");
            int width = 15, height = 10;
            int[,] nums = new int[width, height];
            int x1 = 0;
            int y1 = 0;
            using (var response = webRequest.GetResponse())
            using (var content = response.GetResponseStream())
            using (var reader = new StreamReader(content))
            {
                string strContent;

                while ((strContent = reader.ReadLine()) != null)
                {
                    string[] parts = strContent.Split(' ');

                    for (int i = 0; i < parts.Length; i++)
                    {
                        if (parts[i] != String.Empty)
                        {
                            nums[x1, y1] = Convert.ToInt32(parts[i]);
                            Console.Write(nums[x1, y1] + " ");
                            x1++;
                        }
                    }

                    y1++;
                    x1 = 0;
                    Console.WriteLine();
                }
            }

            /*
            //Randomized 2D array for case testing purposes
            Random r = new Random();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    nums[x, y] = r.Next(999) + 1;

                    if (r.Next(3) == 1)
                        nums[x, y] *= -1;

                    Console.Write(nums[x, y] + " ");
                }

                Console.WriteLine();
            }
            */

            //Horizontal
            long largest = 0;
            long product = 1;
            for(int y = 0; y < height; y++)
            {
                for(int x = 0; x < width - 3; x++)
                {
                    for(int x2 = x; x2 < x + 4; x2++)
                    {
                        product *= nums[x2, y];
                    }

                    if (product > largest)
                        largest = product;

                    product = 1;
                }
            }

            //Vertical
            for(int x = 0; x < width; x++)
            {
                for(int y = 0; y < height - 3; y++)
                {
                    for(int y2 = y; y2 < y + 4; y2++)
                    {
                        product *= nums[x, y2];
                    }

                    if (product > largest)
                        largest = product;

                    product = 1;
                }
            }

            //Negative Diagonal
            for (int y = 0; y < height - 3; y++)
            {
                for(int x = 0; x < width - 3; x++)
                {
                    for(int p = 0; p < 4; p++)
                    {
                        product *= nums[x + p, y + p];
                    }

                    if (product > largest)
                        largest = product;

                    product = 1;
                }
            }

            //Positive Diagonal
            for (int y = 3; y < height; y++)
            {
                for(int x = 0; x < width - 3; x++)
                {
                    for(int p = 0; p < 4; p++)
                    {
                        product *= nums[x + p, y - p];
                    }

                    if (product > largest)
                        largest = product;

                    product = 1;
                }
            }

            Console.WriteLine();
            Console.WriteLine("The largest quadsequence in this array is: ");
            Console.WriteLine(largest);

            Console.ReadKey();
        }
    }
}
