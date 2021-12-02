using System;
using System.Collections.Generic;
using System.Linq;

namespace Practice1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Get array of nums
            string[] strings = File.ReadAllLines("input.txt");
            int[] nums = Array.ConvertAll(strings, str => int.Parse(str));

            // Part 1
            for (int i = 0; i < nums.Length; i++)
            {
                for (int j = i + 1; j < nums.Length; j++)
                {
                    if (nums[i] + nums[j] == 2020)
                    {
                        Console.WriteLine("Found 2 nums...\nMultiplied: " + nums[i] * nums[j]);
                        break;
                    }
                }
            }

            // Part 2
            for (int i = 0; i < nums.Length; i++)
            {
                for (int j = i + 1; j < nums.Length; j++)
                {
                    for (int k = j + 1; k < nums.Length; k++)
                    {
                        if (nums[i] + nums[j] + nums[k] == 2020)
                        {
                            Console.WriteLine("Found 3 nums...\nMultiplied: " + nums[i] * nums[j] * nums[k]);
                            return;
                        }
                    }
                }
            }
        }
    }
}