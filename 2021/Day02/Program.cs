using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day02
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Initialize Variables
            var strings = File.ReadAllLines("input.txt");
            var directions = new string[strings.Length];
            var values = new int[strings.Length];

            for (var i = 0; i < strings.Length; i++)
            {
                var splitString = strings[i].Split(' ');
                directions[i] = splitString[0];
                values[i] = int.Parse(splitString[1]);
            }

            // Part 1
            var depth = 0;
            var pos = 0;
            for (var i = 0; i < values.Length; i++)
            {
                switch (directions[i])
                {
                    case "forward":
                        pos += values[i];
                        break;
                    case "up":
                        depth -= values[i];
                        break;
                    case "down":
                        depth += values[i];
                        break;
                    default:
                        break;
                }
            }
            Console.WriteLine("Part 1: Depth = {0} Position = {1} Answer = {2}", depth, pos, depth * pos);

            // Part 2
            var aim = 0;
            depth = 0;
            pos = 0;
            for (var i = 0; i < values.Length; i++)
            {
                switch (directions[i])
                {
                    case "forward":
                        pos += values[i];
                        depth += values[i] * aim;
                        break;
                    case "up":
                        aim -= values[i];
                        break;
                    case "down":
                        aim += values[i];
                        break;
                    default:
                        break;
                }
            }
            Console.WriteLine("Part 2: Depth = {0} Position = {1} Answer = {2}", depth, pos, depth * pos);
        }
    }
}
