using System;
using System.Collections.Generic;
using System.Linq;

namespace Practice2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Get passwords
            string[] strings = File.ReadAllLines("input.txt");
            char[] delimChars = {' ', ':', '-'};
            List<Combo> passwords = new List<Combo>();

            // Initialize list of password-rule combinations
            foreach(string s in strings)
            {
                string[] ss = s.Split(delimChars);
                // ss[3] is an empty space hence why it is ignored
                passwords.Add(new Combo(ss[4], new Rule(Int32.Parse(ss[0]), Int32.Parse(ss[1]), ss[2][0])));
            }

            // Part 1
            int count = 0;
            foreach(Combo pass in passwords)
            {
                if (pass.Pt1RuleCheck()) { count++; }
            }
            Console.WriteLine("Part 1 valid passwords: " + count);

            // Part 2
            count = 0;
            foreach (Combo pass in passwords)
            {
                if (pass.Pt2RuleCheck()) { count++; }
            }
            Console.WriteLine("Part 2 valid passwords: " + count);
        }
    }

    public class Rule
    {
        public int minimum { get; set; } 
        public int maximum { get; set; }
        public char letter { get; set; }

        public Rule(int min, int max, char c)
        {
            minimum = min;
            maximum = max;
            letter = c;
        }
    }
    public class Combo
    {
        public string password { get; set; }
        public Rule rule { get; set; } 

        public Combo(string password, Rule rule)
        {
            this.password = password;
            this.rule = rule;
        }

        public bool Pt1RuleCheck()
        {
            int count = 0;
            foreach(char c in password)
            {
                if (c == rule.letter)
                {
                    count++;
                }
            }

            if (rule.minimum <= count && count <= rule.maximum)
            {
                return true;
            } 
            else 
            {
                return false;
            }
        }

        public bool Pt2RuleCheck()
        {
            bool valid = false;

            if (this.password[this.rule.minimum - 1] == this.rule.letter)
            {
                valid = !valid;
            }

            if (this.password[this.rule.maximum - 1] == this.rule.letter)
            {
                valid = !valid;
            }

            return valid;
        }
    }
}